using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttom : MonoBehaviour
{
    Vector3 dir; // ����
    GameObject target; // �÷��̾�
    [SerializeField] private int fireballDamage = 7; // ������
    [SerializeField] private float speed = 3f; // �ӵ�
    [SerializeField] private float buttonDistance; // �ִ� �Ÿ�
    SpriteRenderer buttonRenderer;

    Vector3 buttonTransform;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        buttonRenderer = GetComponent<SpriteRenderer>();
        Vector3 dir = (target.transform.position - transform.position).normalized; // ����
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

        transform.localPosition += buttonTransform * speed * Time.deltaTime; // �����̱�
        if (Vector3.Distance(transform.position, target.transform.position) > buttonDistance) // �÷��̾�� �ʹ� ���� ������ �ִٸ�
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == target) // �÷��̾ �¾Ҵٸ�
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.OnDamage(fireballDamage);
            Destroy(this.gameObject);
        }
    }
}
