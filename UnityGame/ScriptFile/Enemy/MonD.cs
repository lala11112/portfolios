using System.Collections;
using UnityEngine;

public class MonD : LivingEntity
{
    // 공격 범위
    [SerializeField] float attackDistance = 1f;
    // 파이어볼 범위
    [SerializeField] float fireDistance = 8f;
    // 누적 시간
    float currentTime = 0;
    // 공격력
    [SerializeField] float damage = 3f;
    // 공격 딜레이
    [SerializeField] float attackDelay = 5f;
    protected GameObject target;
    // 파이퍼 볼 딜레이
    float fireDelay = 4f;
    Animator animator;
    public GameObject fireball; // 파이어볼 프리팹
    public Transform fireballPos; // 파이어볼 생성 위치
    public AudioClip attackClip; // 공격 오디오
    public AudioClip fireballClip; // 파이어볼 오디오
    private AudioSource enemyAudio;
    enum MonDState
    {
        Idle,
        Attack,
        Fire,
        Damage,
        Die
    }
    MonDState m_State;
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    void Start()
    {
        enemyAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        m_State = MonDState.Idle;
    }
    void Update()
    {
        if (GameManager.instance.gameState != GameManager.GameState.Run)
        {
            return;
        }
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player");
        currentTime += Time.deltaTime;
        switch (m_State)
        {
            case MonDState.Idle:
                Idle();
                break;
            case MonDState.Fire:
                Fire();
                break;
            case MonDState.Attack:
                Attack();
                break;
            case MonDState.Damage:
                break;
        }
    }
    void Idle()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < attackDistance) // 공격 사정거리에 들어오면
        {
            m_State = MonDState.Attack;
        }
        else if (Vector3.Distance(transform.position, target.transform.position) > attackDistance && Vector3.Distance(transform.position, target.transform.position) < fireDistance && currentTime > fireDelay)
        {
            m_State = MonDState.Fire;
        }
    }
    void Attack()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < attackDistance) // 계속 공격 사정거리라면
        {
            LivingEntity attackTarget = target.GetComponent<LivingEntity>();
            if (currentTime > attackDelay)
            {
                animator.SetTrigger("attack");

                currentTime = 0;
            }
            if (attackTarget.dead == true)
            {
                m_State = MonDState.Idle;
            }
        }
        else
            m_State = MonDState.Idle;
    }
    void AttackDamage()
    {
        PlayerHealth attackTarget = target.GetComponent<PlayerHealth>(); // 플레이어 찾기

        attackTarget.OnDamage(damage); // 데미지 입히기
        enemyAudio.clip = attackClip;
        enemyAudio.Play(); // 오디오 실행

    }
    void Fire()
    {
        animator.SetTrigger("attack02");
        currentTime = 0;
        m_State = MonDState.Idle;

    }
    void FireAttack()
    {
        if (target != null)
        {
            Vector3 myPos = fireballPos.position;
            Vector3 targetPos = target.transform.position;
            targetPos.z = myPos.z;

            Vector3 vectorToTarget = targetPos - myPos;
            Vector3 quaternionToTarget = Quaternion.Euler(0, 0, 0) * vectorToTarget;

            Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: quaternionToTarget);
            fireballPos.rotation = Quaternion.RotateTowards(fireballPos.rotation, targetRotation, 500 * Time.deltaTime);
        }
        Instantiate(fireball, fireballPos.position, fireballPos.rotation); // 파이어볼 생성
        enemyAudio.clip = fireballClip; // 오디오 클립 바꾸기
        enemyAudio.Play(); // 오디오 실행
    }
    public override void OnDamage(float damage)
    {
        if (!dead)
        {
            animator.SetTrigger("damage");
            base.OnDamage(damage);
        }

    }
    public override void Die()
    {
        base.Die();
        StopAllCoroutines();

        Destroy(gameObject);
    }
}
