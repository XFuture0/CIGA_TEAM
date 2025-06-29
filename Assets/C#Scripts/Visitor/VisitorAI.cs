using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VisitorAI : MonoBehaviour
{
    public float Score;// 分数
    private SpriteRenderer spriteRenderer;
    public Vector3 BasePosition;
    private Animator anim;
    public AudioClip Escape;
    [Header("恐惧值设置")]
    public float maxFear = 100f;       // 最大恐惧值
    public float fearIncreaseRate = 15f; // 每秒恐惧值增加量
    public float fearDecreaseRate = 20f; // 逃跑后每秒恐惧值减少量

    [Header("移动设置")]
    public float normalSpeed = 1.5f;   // 正常移动速度
    public float panicSpeed = 3f;      // 逃跑速度
    public float avoidanceDistance = 7.5f; // 边界回避距离
    public float avoidanceDuration = 2f; // 边界回避持续时间
    private bool isAvoiding = false;
    public float currentFear = 0f;    // 当前恐惧值
    public bool isPanicking = false;  // 是否正在逃跑

    public float panicTimer = 0f;     // 逃跑计时器
    private float directionTimer = 0f; // 方向切换计时器
    private float avoidanceTimer = 0f; // 回避计时器
    private float obstacleAvoidanceTimer = 0f; // 障碍回避计时器
    private float exhibitWatchTimer = 0f; // 展品观看计时器
    private float watchCooldownTimer = 0f; // 观看冷却计时器
    public float ElevTimer = -2f;

    private int currentDirection = 1;  // 当前移动方向 (1=右, -1=左)
    private bool isAvoidingObstacle = false; // 是否在回避障碍物
    private bool isWatchingExhibit = false; // 是否在观看展品
    private bool canWatchExhibit = true; // 是否允许观看展品
    public bool CanElev;
    private Rigidbody2D rb;
    public Text text;
    [Header("射线检测设置")]
    public float obstacleRayDistance = 1f; // 障碍物检测距离
    public float exhibitRayDistance = 1f; // 展品检测距离
    public float exhibitWatchProbability = 0.5f; // 观看展品概率
    public LayerMask Exhabit;
    // 恐惧值加成
    private float fearMultiplier = 1f; // 恐惧值倍率（观看展品时翻倍）
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
        // 初始随机方向
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
        // 更新观看冷却计时器
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
        // 只在非逃跑、非观看状态下进行射线检测
        if (isPanicking || isWatchingExhibit) return;

        // 检测右侧障碍物
        RaycastHit2D obstacleHit = Physics2D.Raycast(
            transform.position,
            Vector2.right * currentDirection,
            obstacleRayDistance
        );

        // 检测上方展品
        RaycastHit2D exhibitHit = Physics2D.Raycast(
            transform.position,
            Vector2.up,
            exhibitRayDistance,
            Exhabit
        );
        // 处理障碍物检测
        if(exhibitHit.collider != null)
        {
            if (obstacleHit.collider != null && !isAvoidingObstacle && exhibitHit.collider.CompareTag("obstacle"))
            {
                StartObstacleAvoidance();
            }
            // 处理展品检测
            if (exhibitHit.collider.gameObject != null && !isWatchingExhibit && canWatchExhibit)
            {
                TryStartWatchingExhibit();
            }
        }
    }
    void StartObstacleAvoidance()
    {
        isAvoidingObstacle = true;
        obstacleAvoidanceTimer = 1f; // 向左走1秒
        currentDirection = -1; // 强制向左移动

        Debug.Log("检测到障碍物，向左回避");
    }

    void TryStartWatchingExhibit()
    {
        // 随机决定是否观看展品
        if (Random.value <= exhibitWatchProbability)
        {
            StartWatchingExhibit();
        }
    }

    void StartWatchingExhibit()
    {

        isWatchingExhibit = true;
        exhibitWatchTimer = 1f; // 观看1秒
        rb.velocity = Vector2.zero; // 停止移动
        SetAnim("Idle");
        // 恐惧值增长翻倍
        fearMultiplier = 2f;
        Debug.Log("开始观看展品，恐惧值增长翻倍");
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
        else if (isAvoiding) // 优先处理回避状态
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
        // 障碍回避逻辑
        obstacleAvoidanceTimer -= Time.deltaTime;
        rb.velocity = new Vector2(currentDirection * normalSpeed, 0);

        // 回避时间结束
        if (obstacleAvoidanceTimer <= 0)
        {
            isAvoidingObstacle = false;
            SetRandomDirection();
        }
    }

    void HandleExhibitWatching()
    {
        // 展品观看逻辑
        exhibitWatchTimer -= Time.deltaTime;
        // 观看时间结束
        if (exhibitWatchTimer <= 0)
        {
            isWatchingExhibit = false;
            fearMultiplier = 1f; // 重置恐惧倍率
            SetRandomDirection();
            Debug.Log("结束观看展品");
        }
    }
    void HandlePanicState()
    {
        // 逃跑逻辑
        panicTimer -= Time.deltaTime;
        OnEscape();

        // 逃跑结束检测
        if (panicTimer <= 0)
        {
            isPanicking = false;
            SetRandomDirection(); // 重置为随机移动
        }
    }

    void HandleNormalState()
    {
        // 边界检测（即将超出左边界）
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
        currentDirection = 1; // 强制向右移动
        rb.velocity = new Vector2(normalSpeed, 0);
    }
    void HandleAvoidance()
    {
        // 边界回避逻辑
        avoidanceTimer -= Time.deltaTime;

        // 保持向右移动
        rb.velocity = new Vector2(normalSpeed, 0);

        // 回避时间结束
        if (avoidanceTimer <= 0)
        {
            isAvoiding = false;
            SetRandomDirection();
        }
    }

    void HandleRandomMovement()
    {
        // 随机方向移动逻辑
        directionTimer -= Time.deltaTime;

        // 定时切换方向
        if (directionTimer <= 0)
        {
            SetRandomDirection();
        }

        // 应用移动
        rb.velocity = new Vector2(currentDirection * normalSpeed, 0);
    }
    // 中断观看展品（被惊吓时调用）
    public void InterruptExhibitWatching()
    {
        if (!isWatchingExhibit) return;

        Debug.Log("观看展品时被惊吓，将中断观看");
        SetAnim("On");
        // 设置1秒后结束观看状态
        exhibitWatchTimer = 1f;

        // 设置3秒观看冷却
        canWatchExhibit = false;
        watchCooldownTimer = 3f;
    }
    void SetRandomDirection()
    {
        // 随机选择新方向
        currentDirection = (Random.Range(0, 2) == 0 ? -1 : 1);
        SetAnim("Walk");
        directionTimer = Random.Range(1f, 4f); // 随机持续时间 1-4秒
    }

    // 被幽灵惊吓时调用这个方法
    public void Scare(float intensity)
    {
        // 增加恐惧值
        currentFear += fearIncreaseRate * intensity * fearMultiplier * Time.deltaTime;
        // 如果正在观看展品，中断观看
        if (isWatchingExhibit)
        {
            InterruptExhibitWatching();
        }

        // 恐惧值达到上限时开始逃跑
        if (currentFear >= maxFear && !isPanicking)
        {
            StartPanic();
        }
    }

    void StartPanic()
    {
        isPanicking = true;
        panicTimer = 5f; // 设置逃跑持续时间
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