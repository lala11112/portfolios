using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlimeMove : EnemyLivingEntity
{
    // 이동속도
    public float moveSpeed = 1.5f;
    // 발견 범위
    [SerializeField] float findDistance = 8f;
    // 공격 범위
    [SerializeField] float attackDistance = 0.5f;
    // 누적 시간
    float currentTime = 0;
    // 공격 딜레이
    [SerializeField] float attackDelay = 1f;
    // 플레이어 트렌스폼
    private Transform player;
    Animator animator;
    SpriteRenderer renderer;

    private bool targetDead = false; // 플레이어가 죽었는가
    public float damage = 2f; // 공격력
    enum SlimeState
    {
        Idle,
        Move,
        Attack,
        Die
    }
    SlimeState m_State;
    void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        m_State = SlimeState.Idle;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        switch(m_State)
        {
            case SlimeState.Idle:
                Idle();
                break;
            case SlimeState.Move:
                Move();
                break;
            case SlimeState.Attack:
                Attack();
                break;
        }
    }
    void Idle()
    {
        animator.SetBool("Walk", false);

        if (Vector3.Distance(transform.position, player.transform.position) < findDistance && !targetDead)
        {
            m_State = SlimeState.Move;
        }
    }
    void Move()
    {
        if(Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            if(dir.x >= 0)
                renderer.flipX = true;
            else if (dir.x < 0)
                renderer.flipX = false;

            transform.position += dir * moveSpeed * Time.deltaTime;
            animator.SetBool("Walk", true);
        }
        else if(Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            m_State = SlimeState.Attack;
        }
        else
        {
            m_State = SlimeState.Idle;
        }
    }
    void Attack()
    {
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            LivingEntity attackTarget = player.GetComponent<LivingEntity>();
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                attackTarget.OnDamage(damage);
                animator.SetTrigger("Attack");
                currentTime = 0;
            }
            if (attackTarget.dead == true)
            {
                targetDead = true;
                m_State = SlimeState.Idle;
            }
        }
        else
            m_State = SlimeState.Move;
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
        yield return new WaitForSeconds(0.5f);
        m_State = SlimeState.Move;
    }
    public override void Die()
    {
        base.Die();
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
