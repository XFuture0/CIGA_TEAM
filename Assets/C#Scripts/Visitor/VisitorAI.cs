using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VisitorAI : MonoBehaviour
{
    public float Score;// ����
    private SpriteRenderer spriteRenderer;
    public Vector3 BasePosition;
    private Animator anim;
    public AudioClip Escape;
    [Header("�־�ֵ����")]
    public float maxFear = 100f;       // ���־�ֵ
    public float fearIncreaseRate = 15f; // ÿ��־�ֵ������
    public float fearDecreaseRate = 20f; // ���ܺ�ÿ��־�ֵ������

    [Header("�ƶ�����")]
    public float normalSpeed = 1.5f;   // �����ƶ��ٶ�
    public float panicSpeed = 3f;      // �����ٶ�
    public float avoidanceDistance = 7.5f; // �߽�رܾ���
    public float avoidanceDuration = 2f; // �߽�رܳ���ʱ��
    private bool isAvoiding = false;
    public float currentFear = 0f;    // ��ǰ�־�ֵ
    public bool isPanicking = false;  // �Ƿ���������

    public float panicTimer = 0f;     // ���ܼ�ʱ��
    private float directionTimer = 0f; // �����л���ʱ��
    private float avoidanceTimer = 0f; // �رܼ�ʱ��
    private float obstacleAvoidanceTimer = 0f; // �ϰ��رܼ�ʱ��
    private float exhibitWatchTimer = 0f; // չƷ�ۿ���ʱ��
    private float watchCooldownTimer = 0f; // �ۿ���ȴ��ʱ��
    public float ElevTimer = -2f;

    private int currentDirection = 1;  // ��ǰ�ƶ����� (1=��, -1=��)
    private bool isAvoidingObstacle = false; // �Ƿ��ڻر��ϰ���
    private bool isWatchingExhibit = false; // �Ƿ��ڹۿ�չƷ
    private bool canWatchExhibit = true; // �Ƿ�����ۿ�չƷ
    public bool CanElev;
    private Rigidbody2D rb;
    public Text text;
    [Header("���߼������")]
    public float obstacleRayDistance = 1f; // �ϰ��������
    public float exhibitRayDistance = 1f; // չƷ������
    public float exhibitWatchProbability = 0.5f; // �ۿ�չƷ����
    public LayerMask Exhabit;
    // �־�ֵ�ӳ�
    private float fearMultiplier = 1f; // �־�ֵ���ʣ��ۿ�չƷʱ������
    public float ChangeAlphaSpeed;
    public AudioClip WalkClip;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        spriteRenderer.color = Color.white;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // ��ʼ�������
        SetRandomDirection();
    }

    void Update()
    {
        if (isPanicking)
        {
            ChangeAlpha();
        }
        UpdateCooldowns();
        PerformRaycasts();
        HandleState();
        CanSetElev();
        if(rb.velocity.x >= 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        if(rb.velocity.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    void UpdateCooldowns()
    {
        // ���¹ۿ���ȴ��ʱ��
        if (watchCooldownTimer > 0)
        {
            watchCooldownTimer -= Time.deltaTime;
            if (watchCooldownTimer <= 0)
            {
                canWatchExhibit = true;
            }
        }
    }
    void PerformRaycasts()
    {
        // ֻ�ڷ����ܡ��ǹۿ�״̬�½������߼��
        if (isPanicking || isWatchingExhibit) return;

        // ����Ҳ��ϰ���
        RaycastHit2D obstacleHit = Physics2D.Raycast(
            transform.position,
            Vector2.right * currentDirection,
            obstacleRayDistance
        );

        // ����Ϸ�չƷ
        RaycastHit2D exhibitHit = Physics2D.Raycast(
            transform.position,
            Vector2.up,
            exhibitRayDistance,
            Exhabit
        );
        // �����ϰ�����
        if(exhibitHit.collider != null)
        {
            if (obstacleHit.collider != null && !isAvoidingObstacle && exhibitHit.collider.CompareTag("obstacle"))
            {
                StartObstacleAvoidance();
            }
            // ����չƷ���
            if (exhibitHit.collider.gameObject != null && !isWatchingExhibit && canWatchExhibit)
            {
                TryStartWatchingExhibit();
            }
        }
    }
    void StartObstacleAvoidance()
    {
        isAvoidingObstacle = true;
        obstacleAvoidanceTimer = 1f; // ������1��
        currentDirection = -1; // ǿ�������ƶ�

        Debug.Log("��⵽�ϰ������ر�");
    }

    void TryStartWatchingExhibit()
    {
        // ��������Ƿ�ۿ�չƷ
        if (Random.value <= exhibitWatchProbability)
        {
            StartWatchingExhibit();
        }
    }

    void StartWatchingExhibit()
    {

        isWatchingExhibit = true;
        exhibitWatchTimer = 1f; // �ۿ�1��
        rb.velocity = Vector2.zero; // ֹͣ�ƶ�
        SetAnim("Idle");
        // �־�ֵ��������
        fearMultiplier = 2f;
        Debug.Log("��ʼ�ۿ�չƷ���־�ֵ��������");
    }

    void HandleState()
    {
        if (isPanicking)
        {
            HandlePanicState();
        }
        else if (isAvoidingObstacle)
        {
            HandleObstacleAvoidance();
        }
        else if (isWatchingExhibit)
        {
            HandleExhibitWatching();
        }
        else if (isAvoiding) // ���ȴ���ر�״̬
        {
            HandleAvoidance();
        }
        else
        {
            HandleNormalState();
        }
    }
    void HandleObstacleAvoidance()
    {
        // �ϰ��ر��߼�
        obstacleAvoidanceTimer -= Time.deltaTime;
        rb.velocity = new Vector2(currentDirection * normalSpeed, 0);

        // �ر�ʱ�����
        if (obstacleAvoidanceTimer <= 0)
        {
            isAvoidingObstacle = false;
            SetRandomDirection();
        }
    }

    void HandleExhibitWatching()
    {
        // չƷ�ۿ��߼�
        exhibitWatchTimer -= Time.deltaTime;
        // �ۿ�ʱ�����
        if (exhibitWatchTimer <= 0)
        {
            isWatchingExhibit = false;
            fearMultiplier = 1f; // ���ÿ־屶��
            SetRandomDirection();
            Debug.Log("�����ۿ�չƷ");
        }
    }
    void HandlePanicState()
    {
        // �����߼�
        panicTimer -= Time.deltaTime;
        OnEscape();

        // ���ܽ������
        if (panicTimer <= 0)
        {
            isPanicking = false;
            SetRandomDirection(); // ����Ϊ����ƶ�
        }
    }

    void HandleNormalState()
    {
        // �߽��⣨����������߽磩
        if (transform.position.x < avoidanceDistance && !isAvoiding)
        {
            StartAvoidance();
        }
        else
        {
            HandleRandomMovement();
        }
    }
    void StartAvoidance()
    {
        isAvoiding = true;
        avoidanceTimer = avoidanceDuration;
        currentDirection = 1; // ǿ�������ƶ�
        rb.velocity = new Vector2(normalSpeed, 0);
    }
    void HandleAvoidance()
    {
        // �߽�ر��߼�
        avoidanceTimer -= Time.deltaTime;

        // ���������ƶ�
        rb.velocity = new Vector2(normalSpeed, 0);

        // �ر�ʱ�����
        if (avoidanceTimer <= 0)
        {
            isAvoiding = false;
            SetRandomDirection();
        }
    }

    void HandleRandomMovement()
    {
        // ��������ƶ��߼�
        directionTimer -= Time.deltaTime;

        // ��ʱ�л�����
        if (directionTimer <= 0)
        {
            SetRandomDirection();
        }

        // Ӧ���ƶ�
        rb.velocity = new Vector2(currentDirection * normalSpeed, 0);
    }
    // �жϹۿ�չƷ��������ʱ���ã�
    public void InterruptExhibitWatching()
    {
        if (!isWatchingExhibit) return;

        Debug.Log("�ۿ�չƷʱ�����ţ����жϹۿ�");
        SetAnim("On");
        // ����1�������ۿ�״̬
        exhibitWatchTimer = 1f;

        // ����3��ۿ���ȴ
        canWatchExhibit = false;
        watchCooldownTimer = 3f;
    }
    void SetRandomDirection()
    {
        // ���ѡ���·���
        currentDirection = (Random.Range(0, 2) == 0 ? -1 : 1);
        SetAnim("Walk");
        directionTimer = Random.Range(1f, 4f); // �������ʱ�� 1-4��
    }

    // �����龪��ʱ�����������
    public void Scare(float intensity)
    {
        // ���ӿ־�ֵ
        currentFear += fearIncreaseRate * intensity * fearMultiplier * Time.deltaTime;
        // ������ڹۿ�չƷ���жϹۿ�
        if (isWatchingExhibit)
        {
            InterruptExhibitWatching();
        }

        // �־�ֵ�ﵽ����ʱ��ʼ����
        if (currentFear >= maxFear && !isPanicking)
        {
            StartPanic();
        }
    }

    void StartPanic()
    {
        isPanicking = true;
        panicTimer = 5f; // �������ܳ���ʱ��
    }
    private void OnDisable()
    {
        Scoremanager.Instance.SetScore(Score);
    }
    private void OnEscape()
    {
        normalSpeed = 1.5f;
        panicSpeed = 3f;
        SetAnim("Run");
        AudioManager.Instance.SetAudioClip(Escape, "NPC");
        if(gameObject.transform.position.y > 0)
        {
            rb.velocity = new Vector2(currentDirection * panicSpeed, 0);
        }
        if(gameObject.transform.position.y < 0)
        {
            rb.velocity = new Vector2(-currentDirection * panicSpeed, 0);
        }
    }
    private void CanSetElev()
    {
        if(ElevTimer > -2)
        {
            ElevTimer -= Time.deltaTime;
        }
        if(ElevTimer < 0)
        {
            CanElev = true;
        }
        if(ElevTimer >= 0)
        { 
            CanElev = false;
        }
    }
    public void ChangeSpeed()
    {
        if (isPanicking)
        {
            panicSpeed = -panicSpeed;
        }
        else
        {
            normalSpeed = -normalSpeed;
        }
    }
    private void SetAnim(string animName)
    {
        if (animName == "Idle" && rb.velocity != Vector2.zero)
        {
            return;
        }
        if(animName != "On")
        {
            anim.SetBool("On", false);
        }
        if(animName != "Run")
        {
            anim.SetBool("Run", false);
        }
        if(animName != "Walk")
        {
            anim.SetBool("Walk", false);
        }
        if(animName == "Idle")
        {
            return;
        }
        anim.SetBool(animName, true);
    }
    private void ChangeAlpha()
    {
        var NowSpeed = ChangeAlphaSpeed;
        if (spriteRenderer.color.a < 0.5f)
        {
            NowSpeed = ChangeAlphaSpeed * 4;
        }
        var alpha = Mathf.Lerp(spriteRenderer.color.a, 0, NowSpeed);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
        if(alpha < 0.1f)
        {
            gameObject.SetActive(false);
        }
    }
    private void NPCWalkAudio()
    {
        AudioManager.Instance.SetAudioClip(WalkClip,"NPC");
    }
}