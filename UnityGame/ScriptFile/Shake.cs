using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 카메라의 흔들림을 제어하는 컴포넌트
public class Shake : MonoBehaviour
{
    public Transform shakeCamera; // 셰이크 효과를 줄 카메라의 트랜스폼
    public bool shakeRotate = false; // 셰이크 효과를 줄 것인지 판단할 변수

    // 카메라의 초기 좌표와 회전값을 저장할 변수
    private Vector3 originPos;
    private Quaternion originRot;
    void Start()
    {
        originPos = shakeCamera.localPosition;
        originRot = shakeCamera.localRotation;
    }
    public static Shake Instance //Shake 스크립트의 싱글톤 함수
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Shake>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("CameraMave");
                    instance = instanceContainer.AddComponent<Shake>();
                }
            }
            return instance;
        }
    }
    private static Shake instance; // 싱글톤 변수
    public IEnumerator ShakeCamera(float duration = 0.05f, // 진동 시간
                                   float magnitudePos = 0.03f // 진동의 위치 크기
                                   ) // 진동의 회전 크기
    {
        float passTime = 0.0f;
        while(passTime < duration)
        {
            Vector3 shakePos = Random.insideUnitSphere;
            shakeCamera.localPosition = shakePos * magnitudePos; // 카메라 위치 변경

            passTime += Time.deltaTime; // 진동 시간 누적

            yield return null;
        }
        shakeCamera.localPosition = originPos;
        shakeCamera.localRotation = originRot;
    }
}
