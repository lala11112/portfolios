using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyLivingEntity : LivingEntity
{
    protected GameObject target; // 플레이어의 트랜스폼
    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
    }
    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    public override void Die()
    {
        base.Die();
        StartCoroutine(shake.ShakeCamera(0.1f, 0.1f)); // 카메라 진동을 줌
        
    }
    protected virtual void Update()
    {
        Debug.Log("dddd");

        if (GameManager.instance.gameState != GameManager.GameState.Run)
        {
            return;
        }
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player");
    }
}