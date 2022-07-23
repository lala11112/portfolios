using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TypeEffect : MonoBehaviour
{
    string targetMsg; // 대화 내용
    public int CharPerSeconds; // 대화 지연 시간
    public Text msgText; // 자신
    int index = 0; // 타이핑되는 텍스트의 수
    public GameObject talkCursor; // 끝났을때 나오는 오브젝트
    float interval; // 텍스트 지연 시간
    AudioSource audioSource;
    public bool isAnim = true;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void SetMsg(string msg)
    {
        if (isAnim) // 애니메이션이 작동중이라면
        {
            CancelInvoke();
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart()
    {
        msgText.text = ""; // 원래 문자 초기화
        index = 0;
        talkCursor.SetActive(false); // 커서 없에기
        isAnim = true;
        Invoke("Effecting", interval); // 시간차 반복 호출 1/CPS = 1글자가 나오는 딜레이
    }
    void Effecting()
    {
        if(msgText.text == targetMsg) // 문장이 끝났다면
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index]; // index의 문자 가져오기
        index++; // index 1증가
        audioSource.Play();
        interval = 1f / CharPerSeconds;
        Invoke("Effecting", interval); // 자신을 호출
    }
    void EffectEnd()
    {
        isAnim = false;
        talkCursor.SetActive(true); // 커서 보이기
    }
}
