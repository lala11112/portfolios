using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TypeEffect : MonoBehaviour
{
    string targetMsg; // ��ȭ ����
    public int CharPerSeconds; // ��ȭ ���� �ð�
    public Text msgText; // �ڽ�
    int index = 0; // Ÿ���εǴ� �ؽ�Ʈ�� ��
    public GameObject talkCursor; // �������� ������ ������Ʈ
    float interval; // �ؽ�Ʈ ���� �ð�
    AudioSource audioSource;
    public bool isAnim = true;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void SetMsg(string msg)
    {
        if (isAnim) // �ִϸ��̼��� �۵����̶��
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
        msgText.text = ""; // ���� ���� �ʱ�ȭ
        index = 0;
        talkCursor.SetActive(false); // Ŀ�� ������
        isAnim = true;
        Invoke("Effecting", interval); // �ð��� �ݺ� ȣ�� 1/CPS = 1���ڰ� ������ ������
    }
    void Effecting()
    {
        if(msgText.text == targetMsg) // ������ �����ٸ�
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index]; // index�� ���� ��������
        index++; // index 1����
        audioSource.Play();
        interval = 1f / CharPerSeconds;
        Invoke("Effecting", interval); // �ڽ��� ȣ��
    }
    void EffectEnd()
    {
        isAnim = false;
        talkCursor.SetActive(true); // Ŀ�� ���̱�
    }
}
