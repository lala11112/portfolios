using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttom : MonoBehaviour
{
    Vector3 dir; // 방향
    GameObject target; // 플레이어
    [SerializeField] private int fireballDamage = 7; // 데미지
    [SerializeField] private float speed = 3f; // 속도
    [SerializeField] private float buttonDistance; // 최대 거리
    SpriteRenderer buttonRenderer;

    Vector3 buttonTransform;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        buttonRenderer = GetComponent<SpriteRenderer>();
        Vector3 dir = (target.transform.position - transform.position).normalized; // 방향
        if (dir.x >= 0)
        {
            buttonRenderer.flipX = false;
            buttonTransform = transform.right;
        }
        else if (dir.x < 0)
        {
            buttonRenderer.flipX = true;
            buttonTransform = -transform.right;
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.localPosition += buttonTransform * speed * Time.deltaTime; // 움직이기
        if (Vector3.Distance(transform.position, target.transform.position) > buttonDistance) // 플레이어와 너무 많이 떨어져 있다면
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == target) // 플레이어가 맞았다면
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.OnDamage(fireballDamage);
            Destroy(this.gameObject);
        }
    }
}
