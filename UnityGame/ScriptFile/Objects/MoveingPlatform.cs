using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveingPlatform : MonoBehaviour
{
    public Transform startPos; // 시작
    public Transform endPos; // 끝
    public Transform desPos; // 목적지
    public float speed = 2f;
    private void Start()
    {
        transform.position = startPos.position;
        desPos = endPos;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player")) // 플레이어가 발판에 탔다면
        {
            collision.transform.SetParent(transform); // 플레이어를 발판의 자식으로 만듬
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player")) // 플레이어가 발판에 탔다면
            collision.transform.SetParent(null); // 플레이어를 발판에 자식에서 헤제
    }
    void FixedUpdate()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Run)
        {
            transform.position = Vector2.MoveTowards(transform.position, desPos.position, Time.deltaTime * speed); // 목표 지점으로 이동하기

            if(Vector2.Distance(transform.position, desPos.position) <= 0.05f) // 포지션이 목표지점과 0.05이하 떨어져 있다면
            {
                if (desPos == endPos) desPos = startPos; // desPos와 endPos이 같다면 시작으로
                else desPos = endPos; // 아니라면 
            }
        }
    }
}
