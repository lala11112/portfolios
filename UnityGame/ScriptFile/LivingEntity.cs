using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class LivingEntity : MonoBehaviour, IDamageable
{
    public Slider healthSlider; // 체력바
    public int startingHp = 100; // 시작 체력
    public float health { get; protected set; } //  체력
    public bool dead { get; protected set; } // 죽음 여부
    public int ID; // 임시 ID
    public event Action onDeath;

    public Shake shake; // 카메라 진동 컴포넌트
    protected virtual void OnEnable() // 시작시 발동
    {
        dead = false;
        health = startingHp;
        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = startingHp;
        healthSlider.value = health;
    }
    public virtual void OnDamage(float damage) // 데미지 입힘
    {
        health -= damage;
        healthSlider.value = health;
        if (health <= 0 && !dead)
        {
            Die();
        }
    }
    public virtual void RestoreHealth(float newHealth) // 체력 채우기
    {
        if (dead)
        {
            return;
        }

        health += newHealth;
        if (health > startingHp)
        {
            Debug.Log("ssssssss");
            health = startingHp;
        }

        healthSlider.value = health;
    }
    public virtual void Die() // 죽었을때 발동
    {
        if (onDeath != null)
        {
            onDeath();
        }
        dead = true; // dead를 트루로
        healthSlider.gameObject.SetActive(false); // 체력바 숨기기
        ItemDp itemDp = GetComponent<ItemDp>(); // ItemDp 클래스 가져오기
        if (itemDp != null)
            itemDp.ItemDrop(); // 아이템 드롭 실행하기
    }
}
