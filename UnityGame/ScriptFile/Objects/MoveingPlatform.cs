using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveingPlatform : MonoBehaviour
{
    public Transform startPos; // ����
    public Transform endPos; // ��
    public Transform desPos; // ������
    public float speed = 2f;
    private void Start()
    {
        transform.position = startPos.position;
        desPos = endPos;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player")) // �÷��̾ ���ǿ� ���ٸ�
        {
            collision.transform.SetParent(transform); // �÷��̾ ������ �ڽ����� ����
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player")) // �÷��̾ ���ǿ� ���ٸ�
            collision.transform.SetParent(null); // �÷��̾ ���ǿ� �ڽĿ��� ����
    }
    void FixedUpdate()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Run)
        {
            transform.position = Vector2.MoveTowards(transform.position, desPos.position, Time.deltaTime * speed); // ��ǥ �������� �̵��ϱ�

            if(Vector2.Distance(transform.position, desPos.position) <= 0.05f) // �������� ��ǥ������ 0.05���� ������ �ִٸ�
            {
                if (desPos == endPos) desPos = startPos; // desPos�� endPos�� ���ٸ� ��������
                else desPos = endPos; // �ƴ϶�� 
            }
        }
    }
}
