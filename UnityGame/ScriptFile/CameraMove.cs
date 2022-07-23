using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public static CameraMove Instance //CameraMove 스크립트의 싱글톤 함수
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<CameraMove>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("CameraMave");
                    instance = instanceContainer.AddComponent<CameraMove>();
                }
            }
            return instance;
        }
    }
    private static CameraMove instance; // 싱글톤 변수
    
    public float offsetX = 0f;
    public float offsetY = 0f;
    public float offsetZ = -35f;
    Vector3 cameraPos; // 카메라의 위치
    public float followSpeed = 10f; // 플레이어를 쫒는 카메라의 속도
    public bool cameraSmootMoving; // true 라면 플레이어를 천천히 쫒음, false 라면 플레이어에게 순간이동
    void LateUpdate()
    {

        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        cameraPos.x = playerPos.x + offsetX;
        cameraPos.y = playerPos.y + offsetY;
        cameraPos.z = playerPos.z + offsetZ;

        if (cameraSmootMoving)
        {
            transform.position = Vector3.Lerp(transform.position, cameraPos, followSpeed * Time.deltaTime); // 플레이어 쪽으로 천천히 쫒아가기
        }
        else
        {
            transform.position = playerPos; // 플레이어에게 순간이동하기
            cameraSmootMoving = true;
        }

    }
}