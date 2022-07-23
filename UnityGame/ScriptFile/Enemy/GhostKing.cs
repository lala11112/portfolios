using System.Collections;
using UnityEngine;

public class GhostKing : LivingEntity
{
    [SerializeField] float m_jumpForce = 20f; // 점프 높이
    private BoxCollider2D coll;
    private AudioSource audioSource;
    private Animator animator;
    private Rigidbody2D rigid;
    public ParticleSystem TrornReadyParticle; // 가시 준비 파티클
    public ParticleSystem TrornParticle; // 가시 파티클
    public ParticleSystem TeleportyParticle; // 텔레포트 파티클
    GameObject target; // 플레이어
    SpriteRenderer ghostrenderer;
    public int damage; // 기본으로 플레이어와 닿으면 줄 데미지
    public GameObject button; // 투사체 오브젝트
    public AudioClip attack1ReadyClip; // 가시 솟기 준비 클립
    public AudioClip attack1Clip; // 가시 솟기 클립
    public AudioClip attack3Clip; // 투사체 발사 클립
    public AudioClip TeleportyClip; // 텔레포트 클립
    public AudioClip dashReadyClip; // 대쉬 준비 클립
    public AudioClip jumpAttackClip; // 점프 공격 클립
    public AudioClip dashClip; // 대쉬 클립
    [SerializeField] private float speed = 5f; // 대쉬 속도
    bool isLook = false; // 대쉬중이라면
    public GameObject dashPrefab; // 대쉬 프리팹
    private bool isJumpAttack = false; // 점프어택 중인가
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        ghostrenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Think());
    }

    void Update()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player");
        if (shake == null)
            shake = GameObject.FindGameObjectWithTag("CameraRig").GetComponent<Shake>();
        if (!isLook)
        {
            Vector3 dir = (target.transform.position - transform.position).normalized; // 플레이어를 바라보게 만들기
            if (dir.x >= 0) // 플레이어가 오른쪽에 있다면
            {
                ghostrenderer.flipX = false;
                dashPrefab.transform.position = transform.position + new Vector3(3, -1, 0); // 방향 표시도 오른쪽으로 옮기기
                dashPrefab.transform.rotation = Quaternion.Euler(0,0,0);
            }
            else if (dir.x < 0)// 플레이어가 왼쪽에 있다면
            {
                ghostrenderer.flipX = true;
                dashPrefab.transform.position = transform.position + new Vector3(-3, -1, 0); // 방향 표시도 왼쪽으로 옮기기
                dashPrefab.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    IEnumerator Think()
    {
        while (GameManager.instance.gameState != GameManager.GameState.Run) // 게임 진행중이 아니라면
        {
            yield return new WaitForSeconds(0.3f);
        }
        animator.SetTrigger("m_Idle");
        yield return new WaitForSeconds(1f);
        int ranAction = Random.Range(0, 10);
        switch (ranAction)
        {
            // 땅에서 가시가 솟아나는 패턴
            case 0:
            case 6:
                StartCoroutine(UpThorn());
                break;
            // 순간이동 후 가시 솟기
            case 1:
            case 2:
            case 3:
                StartCoroutine(TeleportUpThorn());
                break;
            // 투사체 패턴
            case 4:
            case 5:
                StartCoroutine(Shot());
                break;
            // 대쉬 패턴
            case 7:
            case 8:
                StartCoroutine(Dash());
                break;
            // 점프 공격 패턴
            case 9:
            case 10:
                StartCoroutine(Jump());
                break;
        }
    }
    IEnumerator TeleportUpThorn() // 순간이동후 가시 솟기 패턴
    {
        bool randBool = (Random.value > 0.5f); // 순간이동하는 위치 랜덤
        int teleportCount = Random.Range(2, 4); // 텔레포트 횟수
        for (int i = 0; i < teleportCount; i++) // 텔레포트 횟수만큼 텔레포트하기
        {
            StartCoroutine(Teleport(randBool, 4, 1.6f)); // 텔레포트하기
            yield return new WaitForSeconds(0.3f); // 1초 기다리기
            randBool = !randBool; // 다른 방향으로 텔레포트 
        }
        yield return new WaitForSeconds(0.5f); // 1초 기다리기

        StartCoroutine(UpThorn()); // 가시 솟기
    }
    IEnumerator Teleport(bool dir, float x, float y) // 방향과 사이에 거리를 입력 받음
    {
        animator.SetTrigger("Walk"); // 이동 애니메이션으로 바꾼후
        yield return new WaitForSeconds(0.3f); // 0.3초 기다리기
        if (dir) // 방향이 트루라면
        {
            transform.position = target.transform.position + new Vector3(x, y, 0); // 오른쪽으로 순간이동
        }
        else
        {
            transform.position = target.transform.position + new Vector3(-x, y, 0); // 왼쪽으로 순간이동
        }
        StartCoroutine(shake.ShakeCamera(0.1f, 0.1f)); // 카메라 진동을 줌
        Instantiate(TeleportyParticle, transform.position, Quaternion.identity); // 파티클 생성
        audioSource.clip = TeleportyClip; // 오디오 출력
        audioSource.Play();
    }
    IEnumerator Dash() // 순간이동 후 돌진
    {
        bool randBool = (Random.value > 0.5f); // 순간이동하는 위치 랜덤
        StartCoroutine(Teleport(randBool, 4, 1.6f)); // 텔레포트하기
        audioSource.clip = dashReadyClip;
        audioSource.Play(); // 대쉬 준비 사운드 실행
        yield return new WaitForSeconds(0.5f);
        dashPrefab.SetActive(true); // 대쉬 경로 보이기
        yield return new WaitForSeconds(1f);

        dashPrefab.SetActive(false); // 대쉬 경로 숨기기
        isLook = true; // 대쉬중으로 변경
        animator.SetTrigger("Walk"); // 이동 애니메이션으로 바꾸기
        audioSource.clip = dashReadyClip;
        audioSource.Play(); // 대쉬 사운드 실행
        for (int i = 0; i <= 80; i++)
        {
            if (ghostrenderer.flipX) // 왼쪽을 보고있다면
                transform.localPosition += -Vector3.right * speed * Time.deltaTime; // 왼쪽으로 움직이기
            else if (!ghostrenderer.flipX) // 오른쪽을 보고있다면
                transform.localPosition += Vector3.right * speed * Time.deltaTime; // 오른쪽으로 움직이기
            yield return new WaitForSeconds(0.001f); ;
        }
        StartCoroutine(Teleport(!randBool, 4, 1.6f)); // 텔레포트하기
        yield return new WaitForSeconds(0.5f);
        isLook = false; // 대쉬중이 아닌걸로 변경
        StartCoroutine(Think());
    }
    IEnumerator Jump() // 점프
    {
        animator.SetTrigger("Jump");
        rigid.velocity = new Vector2(rigid.velocity.x, m_jumpForce);
        isLook = true;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Teleport(true, 0, 5)); // 플레이어 위의 위치로 떨어지기
        rigid.gravityScale = 0f; // 중력값 0으로 만들기
        rigid.velocity = Vector3.zero; // 더이상 올라가지 않기

        animator.SetTrigger("Attack1"); // attack2 애니메이션 호출
        yield return new WaitForSeconds(0.5f);
        isJumpAttack = true; // 점프어택 true로 만들기
        for (float i = 0; i < 360; i += 40) // 360도로 불꽃 발사 
        {
            Instantiate(button, transform.position + new Vector3(0, -0.4f, 0), Quaternion.Euler(0, 0, i));
            audioSource.clip = attack3Clip; // 오디오 출력
            audioSource.Play();
        }
        yield return new WaitForSeconds(1f);
        rigid.gravityScale = 5f; // 중력값 5으로 만들기
    }

    IEnumerator Shot() // 투사체 발사
    {
        bool randBool = (Random.value > 0.5f); // 순간이동하는 위치 랜덤
        StartCoroutine(Teleport(randBool, 5, 1.6f)); // 텔레포트하기
        yield return new WaitForSeconds(0.3f);

        animator.SetTrigger("Attack3Ready"); // 발사 준비
        yield return new WaitForSeconds(1f);

        animator.SetTrigger("Attack3"); // 발사
        for (float i = -60; i <= 60; i += 17)
        {
            Instantiate(button, transform.position, Quaternion.Euler(0, 0, i));
            audioSource.clip = attack3Clip; // 오디오 출력
            audioSource.Play();
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Think());
    }
    IEnumerator UpThorn() // 땅에서 솟는 가시
    {
        float trornDir = 1f;
        WaitForSeconds endWait; // 끝날때 기다리는 초
        if (Vector3.Distance(transform.position, target.transform.position) < 5)
        {
            trornDir = Random.Range(1, 2); // 플레이어가 가깝다면 가시 사이의 간격 좁게하기
            endWait = new WaitForSeconds(4f); // 패턴 끝날때 4초 기다리기
        }
        else
        {
            trornDir = Random.Range(3, 5); // 플레이어가 멀다면 가시 사이의 간격 멀리하기
            endWait = new WaitForSeconds(2f); // 패턴 끝날때 2초 기다리기
        }

        animator.SetTrigger("Attack1Ready"); // 가시 솟기 준비 자세
        audioSource.clip = attack1ReadyClip; // 오디오 출력
        audioSource.Play();
        for (float i = trornDir; i < trornDir * 5; i += trornDir)
        {
            Instantiate(TrornReadyParticle, transform.position + new Vector3(i, -1.4f, 0), Quaternion.Euler(90, 0, 0));
            Instantiate(TrornReadyParticle, transform.position + new Vector3(-i, -1.4f, 0), Quaternion.Euler(90, 0, 0));
        }
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Attack1"); // 가시 솟기 자세
        audioSource.clip = attack1Clip; // 오디오 출력
        audioSource.Play();
        StartCoroutine(shake.ShakeCamera(0.05f, 0.1f)); // 카메라 진동을 줌
        for (float i = trornDir; i < trornDir * 5; i += trornDir)
        {
            Instantiate(TrornParticle, transform.position + new Vector3(i, -1.4f, 0), Quaternion.Euler(-90, 0, 0));
            Instantiate(TrornParticle, transform.position + new Vector3(-i, -1.4f, 0), Quaternion.Euler(-90, 0, 0));
        }
        yield return endWait; // end Wait초 기다리기
        StartCoroutine(Think()); // 다음 패턴 만들기
    }
    public override void OnDamage(float damage)
    {
        if (!dead)
        {
            base.OnDamage(damage);
        }

    }
    public override void Die()
    {
        base.Die();
        StopAllCoroutines();

        StartCoroutine(Dead());
    }
    IEnumerator Dead()
    {
        animator.SetTrigger("Dead");
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == target) // 플레이어와 닿았다면
        {
            LivingEntity attackTarget = target.GetComponent<LivingEntity>(); // 플레이어의 LivingEntity 컴포넌트 찾기
            attackTarget.OnDamage(damage); // 플레이어에게 damage 값만큼 데미지 입히기
        }

        if (collision.contacts[0].normal.y > 0.7f && isJumpAttack) // 점프 어택중이고 착지했다면
        {

            shake.ShakeCamera(0.2f, 0.1f); // 화면흔들기
            isLook = false;
            rigid.gravityScale = 2f; // 중력값 복구
            for (float i = 0; i <= 180; i += 180) // 양옆에 불꽃 발사 
            {
                Instantiate(button, transform.position + new Vector3(0, -0.4f, 0), Quaternion.Euler(0, 0, i));
                audioSource.clip = jumpAttackClip; // 오디오 출력
                audioSource.Play();
                audioSource.clip = attack3Clip; // 오디오 출력
                audioSource.Play();
            }
            StartCoroutine(Think());
            isJumpAttack = false;
        }
    }
}
