using System.Collections;
using UnityEngine;
public class PlayerMove : PlayerHealth
{
    [SerializeField] float m_speed = 4.0f; // 걷는 속도
    [SerializeField] float m_jumpForce = 7.5f; // 점프 힘
    [SerializeField] float m_rollForce = 6.0f; // 구르기 힘
    [SerializeField] float m_WallForce = 6.0f; // 벽차기 힘

    private BoxCollider2D boxCollider; // 플레이어의 콜라이더
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;
    private bool m_grounded = false;
    public bool m_rolling = false; // 슬라이딩 중인가?
    private bool m_attack = false; // 공격중 인가?
    private int m_facingDirection = 1;
    private int m_currentAttack = 0; // 공격 모션 바꾸는 변수
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    [SerializeField] private float m_timeAttack = 0.0f; // 공격 쿨타임
    private PlayerHealth playerHealth;
    public Transform pos; // 공격 판정의 위치
    public Vector2 boxSize; // 공격 판정의 크기
    private AudioSource playerAudio; // 오디오 클립
    public AudioClip missAttackClip; // 빗나간 공격 오디오 클립
    public AudioClip attackClip; // 명중한 공격 오디오 클립
    public AudioClip rollingClip; // 구르기 오디오 클립
    public AudioClip damageClip; // 데미지 오디오 클립
    public AudioClip groundClip; // 착지 오디오 클립
    public AudioClip blockClip; // 방어 오디오 클립
    public bool m_block = false; // 방어중인가
    private float orj_TimeAttack = 0.0f; // 원래 공격 쿨타임

    private GameObject scanObj; // 대화를 가진 오브젝트
    void Start()
    {
        orj_TimeAttack = m_timeAttack;
        playerAudio = GetComponent<AudioSource>();
        playerHealth = GetComponent<PlayerHealth>();
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState != GameManager.GameState.Run) // 게임 상태가 run이 아니라면
        {
            return;
        }
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;


        if (!m_grounded && m_groundSensor.State()) // 플레이어가 땅에 착지 했다면
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
            playerAudio.clip = groundClip;
            playerAudio.Play();
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        float inputX = Input.GetAxis("Horizontal"); // 플레이어의 좌우 입력
                                                    // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false; //왼쪽 보기
            m_facingDirection = 1; // 왼쪽일때 1
            pos.position = transform.position + new Vector3(1, 0.5f, 0); // 공격 박스 왼쪽으로 이동
        }

        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true; // 오른쪽 보기
            m_facingDirection = -1; // 오른쪽일때 -1
            pos.position = transform.position + new Vector3(-1, 0.5f, 0); // 공격 박스 오른쪽으로 이동
        }
        // Move
        if (!m_rolling && inputX != 0 && !m_block) // 움직이는 조건
        {
            m_animator.SetBool("Run", true);
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
        }
        else
        {
            m_animator.SetBool("Run", false);
        }

        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y); // y축이 올라가고 있는지 애니메이션 변수 제어

        if (!m_grounded && !m_rolling && !m_block) // 땅 위에 있지 않다면 && 구르고 있지 않다면 && 방어하고있지 않다면
        {
            m_animator.SetBool("WallSlide", (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State()));
        }
        if (m_attack) // 공격중 이라면
        {
            m_body2d.velocity = Vector2.zero; // 힘 안받기
        }
        //Attack
        if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > m_timeAttack && !m_rolling) // 마우스 왼쪽 버튼을 누르고 공격 쿨타임이 돌았다면
        {
            m_attack = true;
            StartCoroutine(Attack());
        }
        // Block
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
            m_block = true;
        }

        else if (Input.GetMouseButtonUp(1))
        {
            m_animator.SetBool("IdleBlock", false);
            m_block = false;
        }

        // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
            playerAudio.clip = rollingClip;
            playerAudio.Play();
        }


        //Jump
        else if (Input.GetKeyDown("space") && !m_rolling && m_grounded) // 스페이스바를 눌렀다면
        {
            // 점프하기
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }
        else if (Input.GetKeyUp("space") && m_body2d.velocity.y > 0) // 점프키에서 손을 땠고 상승중이라면
        {
            m_body2d.velocity = m_body2d.velocity * 0.5f; // 현재 올라가는 속도 절반으로 변경
        }
        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }
        IEnumerator Attack()
        {
            m_currentAttack++; // 모션 바꾸는 변수
            if (m_currentAttack > 3) // 공격이 연속 3번을 넘었다면
            {
                m_currentAttack = 1; // 모션 1로 바꾸기
                m_timeAttack += 0.02f; // 공속 0.02초 낮추기
            }
            if (m_timeSinceAttack > 1.0f) // 공격한지 1초 지났다면
            {
                m_currentAttack = 1; // 모션 1로 바꾸기
                m_timeAttack = orj_TimeAttack; // 원래 공속으로 돌아오기
            }
            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);
            InEnemyDamage(); // 데미지 입히기
            m_timeSinceAttack = 0.0f;
            yield return new WaitForSeconds(m_timeAttack);
        }
        // 적을 공격할 곳을 정하는 함수
        void InEnemyDamage()
        {
            Collider2D[] colls = Physics2D.OverlapBoxAll(pos.position, boxSize, 0); // 공격 콜라이더 생성

            foreach (Collider2D coll in colls)
            {
                IDamageable target = coll.GetComponent<IDamageable>(); // IDamageble을 가진 오브젝트를 찾음
                if (target != null && coll.transform.tag == "Enemy") // 타겟을 찾았다면
                {
                    target.OnDamage(weaponPower); // target에게 데미지를 줌
                    StartCoroutine(shake.ShakeCamera(0.1f, 0.1f)); // 카메라 진동을 줌
                    playerAudio.clip = attackClip; // 공격 명중 오디오 재생
                    playerAudio.Play();
                }
                else
                {
                    playerAudio.clip = missAttackClip; // 공격 빗나감 오디오 재생
                    playerAudio.Play();
                }
            }
        }
    }
    void OnDrawGizmos() // 공격 판정의 기즈모
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

    // Animation Events
    // Called in end of roll animation.
    void AE_ResetRoll()
    {
        m_rolling = false;
    }
    void AE_Attack() // 애니메이션 이벤트
    {
        m_attack = false;
    }
    // Called in slide animation.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;
    }
    public override void OnDamage(float damage) // 데미지를 입는다면
    {
        if (m_block) // 막았다면
        {
            damage = damage / 2; // 데미지 절반만 받기
            playerAudio.clip = blockClip;
            playerAudio.Play();
        }
        else
        {
            playerAudio.clip = damageClip;
            playerAudio.Play();
        }

        base.OnDamage(damage);
    }
}