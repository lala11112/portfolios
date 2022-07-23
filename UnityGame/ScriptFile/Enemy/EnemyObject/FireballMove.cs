using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMove : MonoBehaviour
{
    [SerializeField] private int buttonDamage = 7; // 데미지
    [SerializeField] private float speed = 3f; // 속도
    Vector3 dir; // 방향
    GameObject target; // 플레이어
    [SerializeField] private float fireballDistance; // 파이어볼 거리
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        dir = target.transform.position - transform.position; // 방향 구하기
        dir.Normalize(); // 방향 크기 1로 초기화
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime; // 파이어볼 움직이기
        if(Vector3.Distance(transform.position, target.transform.position) > fireballDistance) // 플레이어와 너무 많이 떨어져 있다면
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == target) // 플레이어가 맞았다면
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.OnDamage(buttonDamage);
            Destroy(this.gameObject);
        }
    }
}
