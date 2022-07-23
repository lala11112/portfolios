using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mushroom : LivingEntity
{
    // 이동 속도
    public float moveSpeed = 1.5f;
    // 돌진 속도
    public float dashSpeed = 4f;
    // 발견 범위
    [SerializeField] float findDistance = 8f;
    // 공격 범위
    [SerializeField] float attackDistance = 0.9f;
    // 돌진 범위
    [SerializeField] float dashDistance = 4f;
    // 누적 시간
    float currentTime = 0;
    // 공격력
    [SerializeField] float damage = 5f;
    // 공격 딜레이
    [SerializeField] float attackDelay = 1f;
    // 대쉬 딜레이
    float dashDelay = 4f;
    // 플레이어 트렌스폼
    private GameObject target;
    Animator animator;
    SpriteRenderer Mushroomrenderer;
    Rigidbody2D m_body2d;
    private bool targetDead = false; // 플레이어가 죽었는가
    enum MushroomState
    {
        Idle,
        Move,
        Attack,
        Dash,
        Damage,
        Die
    }
    MushroomState m_State;
    void Start()
    {
        animator = GetComponent<Animator>();
        Mushroomrenderer = GetComponent<SpriteRenderer>();
        m_State = MushroomState.Idle;
        m_body2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player");

        switch (m_State)
        {
            case MushroomState.Idle:
                Idle();
                break;
            case MushroomState.Move:
                Move();
                break;
            case MushroomState.Dash:
                Dash();
                break;
            case MushroomState.Attack:
                Attack();
                break;
            case MushroomState.Damage:
                break;
        }
    }
    void Idle()
    {
        animator.SetBool("Walk", false);
        if (Vector3.Distance(transform.position, target.transform.position) < findDistance && !targetDead && Vector3.Distance(transform.position, target.transform.position) > attackDistance)
        {
            m_State = MushroomState.Move;
        }
    }
    void Move()
    {

        if (Vector3.Distance(transform.position, target.transform.position) > attackDistance && currentTime < dashDelay)
        {
            currentTime += Time.deltaTime;
            Vector3 dir = (target.transform.position - transform.position).normalized; // 방향
            if (dir.x >= 0)
                Mushroomrenderer.flipX = false;
            else if (dir.x < 0)
                Mushroomrenderer.flipX = true;

            transform.position += dir * moveSpeed * Time.deltaTime; // 이동
            animator.SetBool("Walk", true); // 이동 애니메이션
        }
        else if (Vector3.Distance(transform.position, target.transform.position) < attackDistance) // 공격 사정거리에 들어오면
        {
            m_State = MushroomState.Attack;
        }
        else if (Vector3.Distance(transform.position, target.transform.position) > attackDistance && Vector3.Distance(transform.position, target.transform.position) < dashDistance && currentTime > dashDelay)
        {
            m_State = MushroomState.Dash;
        }
        else
        {
            m_State = MushroomState.Idle;
        }
    }
    void Attack()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < attackDistance) // 계속 공격 사정거리라면
        {
            currentTime += Time.deltaTime;
            LivingEntity attackTarget = target.GetComponent<LivingEntity>();
            if (currentTime > attackDelay)
            {
                attackTarget.OnDamage(damage);
                animator.SetTrigger("Attack");
                currentTime = 0;
            }
            if (attackTarget.dead == true)
            {
                targetDead = true;
                m_State = MushroomState.Idle;
            }
        }
        else
            m_State = MushroomState.Idle;
    }
    void Dash()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        float m_facingDirection = dir.x;
        currentTime += Time.deltaTime;
        animator.SetTrigger("Dash");
        m_body2d.velocity = new Vector2(m_facingDirection * dashSpeed, m_body2d.velocity.y);
        currentTime = 0;
        m_State = MushroomState.Move;
    }
    public override void OnDamage(float damage)
    {
        if (!dead)
        {
            StartCoroutine(DamageProcess());
            base.OnDamage(damage);
        }
    }
    IEnumerator DamageProcess()
    {
        m_State = MushroomState.Idle;
        yield return new WaitForSeconds(1f);
        m_State = MushroomState.Move;
    }
    public override void Die()
    {
        base.Die();
        StopAllCoroutines();
        
        Destroy(gameObject);
    }
}
