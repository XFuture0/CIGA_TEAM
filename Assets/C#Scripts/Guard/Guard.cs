using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Guard : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public Vector3 BasePosition;
    public float Speed;
    public bool CanElev;
    public float ElevTimer = -2f;
    [Header("GroundBoxCast")]
    public Vector2 GroundBoxPoint1;
    public Vector2 GroundBoxPoint2;
    public bool LeftWall;
    public bool RightWall;
    public LayerMask Ground;
    public AudioClip WalkClip;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        Speed = math.abs(Speed);
        anim.SetBool("Walk", true);
    }
    private void Update()
    {
        CheckIsGround();
        CheckWall();
        if (rb.velocity.x >= 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        if(rb.velocity.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        if(ElevTimer > -2f)
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
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(Speed * Time.timeScale, rb.velocity.y);
    }
    public void SetElev()
    {
        ElevTimer = 5f;
        Speed = math.abs(Speed) * -1f;
    }
    public void CheckWall()
    {
        if (LeftWall)
        {
            Speed = math.abs(Speed);
        }
        if (RightWall)
        {
            Speed = math.abs(Speed) * -1f;
        }
    }
    private void CheckIsGround()
    {
        LeftWall = Physics2D.OverlapCircle((Vector2)transform.position + GroundBoxPoint1,0.1f, Ground);
        RightWall = Physics2D.OverlapCircle((Vector2)transform.position + GroundBoxPoint2,0.1f, Ground);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere((Vector2)transform.position + GroundBoxPoint1, 0.02f);
        Gizmos.DrawSphere((Vector2)transform.position + GroundBoxPoint2, 0.02f);
    }
    private void NPCWalkAudio()
    {
        AudioManager.Instance.SetAudioClip(WalkClip, "NPC");
    }
}
