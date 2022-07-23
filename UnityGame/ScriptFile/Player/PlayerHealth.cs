using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.ParticleSystemJobs;
public class PlayerHealth : LivingEntity
{
    public int weaponPower; // 기본 공격력
    private bool isDamageTime = false;
    private Animator playerAnimator;
    public int hp = 30;
    private PlayerMove playerMove;
    public Image bloodScreen; // 데미지를 입었을 때 나오는 이미지
    SpriteRenderer playerRenderer;
    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        startingHp = hp;
        health = startingHp;
    }
    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
    }
    public override void OnDamage(float damage) // 데미지를 입는다면
    {
        if(dead == true && !isDamageTime) // 죽어있거나 무적시간이라면
        {
            return; // 데미지를 입지 않음
        }

        StartCoroutine(shake.ShakeCamera(0.2f, 0.2f)); // 카메라 진동을 줌
        if(!playerMove.m_rolling && !playerMove.m_block && !playerAnimator.GetBool("WallSlide")) // 행동불가 상태에선 애니메이션 실행불가
            playerAnimator.SetTrigger("Hurt");
        base.OnDamage(damage);
        healthSlider.value = health;
        StartCoroutine(ShowBloodScreen()); // 피격 UI 재생
        isDamageTime = true;
        StartCoroutine(UnDamageTime()); // 무적시간 활성화

    }
    IEnumerator ShowBloodScreen()
    {
        bloodScreen.color = new Color(1, 0, 0, Random.Range(0.2f, 0.3f)); // 이미지의 투명도를 불규칙하게 변경
        yield return new WaitForSeconds(0.3f);
        bloodScreen.color = Color.clear;
    }
    public override void Die()
    {
        base.Die();
        playerAnimator.SetTrigger("Death");
        //Collider2D[] playerColliders = GetComponents<Collider2D>();
        //for (int i = 0; i < playerColliders.Length; i++)
            //playerColliders[i].enabled = false;B
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IItem item = collision.GetComponent<IItem>(); // 아이템과 닿았는지 검사

        if (item != null) // 아이템과 닿았다면
        {

            SpriteRenderer itemSprite = collision.GetComponent<SpriteRenderer>(); // 아이템의  SpriteRenderer컴포넌트 가져오기
            if (itemSprite != null)
                UIManager.instance.ItemUIImage(itemSprite); // UI 이미지를 item 오브젝트로 바꾸기

            string itemExplanation = item.Explanation(); // 아이템 설명 가져오기
            if (itemExplanation != null) // 아이템 설명이 있다면
                UIManager.instance.ItemUIText(itemExplanation); // 아이템 설명 UI에 보내기
            else // 없다면
                UIManager.instance.ItemUIText(null); // 아이템 설명에 null 보내기

            item.Use(gameObject); // 아이템 사용하기
        }
        if(collision.gameObject.tag == "Dead") // 낭떨어지등에 닿았다면
        {
            Die();
        }
        if (collision.gameObject.tag == "TalkObject")
        {
            ObjectType target = collision.gameObject.GetComponent<ObjectType>();
            if (target != null) // 타겟을 찾았다면
            {
                GameManager.instance.gameState = GameManager.GameState.Talk; // 대화 상태로 전환
                UIManager.instance.talkObject = collision.gameObject; // 대화 이벤트 오브젝트를 변경
            }
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        DamageObject damageObject = other.GetComponent<DamageObject>();
        if (damageObject != null && !isDamageTime)
        {
            OnDamage(damageObject.damage);
            isDamageTime = true;
            StartCoroutine(UnDamageTime());
        }
    }
    IEnumerator UnDamageTime() // 무적시간
    {
        Time.timeScale = 0.5f; // 시간 감속하기

        // 무적시간 활성화 하기
        int countTime = 0;
        while(countTime < 5)
        {
            if (countTime % 2 == 0)
                playerRenderer.color = new Color32(255, 255, 255, 90);
            else
                playerRenderer.color = new Color32(255, 255, 255, 180);
            Time.timeScale = Time.timeScale + 0.1f;
            yield return new WaitForSeconds(0.2f);
            countTime++;
        }

        Time.timeScale = 1; // 원래 시간으로 되돌리기
        playerRenderer.color = new Color32(255, 255, 255, 255); // 원래 투명도로 돌아오기
        isDamageTime = false; // 데미지 다시 받게 만들기
        yield return null;
    }
}
