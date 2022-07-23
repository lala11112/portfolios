using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartBtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    public ButtonType currentType; // ��ư�� Ÿ���� ��� ����
    private Transform buttonScale;
    Vector3 defaultScale; // ��ư�� �ʱ� ũ��
    bool isSound = true; // ���尡 �����ִ°�
    public CanvasGroup mainGroup; // ���� ��ư���� Group
    public CanvasGroup optonGroup; // �ɼ� ��ư���� Group
    public Text SoundText; // ���� on, off �ؽ�Ʈ
    public Text loadText; // ���̺�ε� �ؽ�Ʈ
    private void Start()
    {
        buttonScale = GetComponent<Transform>(); // ��ư�� Ʈ������ �ʱ��
        defaultScale = buttonScale.localScale; // ��ư�� �ʱ� ũ�� ����
    }
    public void OnBtnClick()
    {
        switch (currentType)
        {
            case ButtonType.New: // ��ư Ÿ���� New��� (���� ���� ��ư�̶��)
                SceneManager.LoadScene("LoadingScene");
                LoadingNewScene.Instance.NewSceneLoading("Stage1");
                break;
            case ButtonType.Continue: // �ٽ� ���� ��ư�̶��
                if (PlayerPrefs.HasKey("Stage")) // ���̺� ������ �����Ѵٸ�
                {
                    SceneManager.LoadScene("LoadingScene");
                    string stageName = PlayerPrefs.GetString("Stage");
                    LoadingNewScene.Instance.NewSceneLoading(stageName);
                }
                else if(!PlayerPrefs.HasKey("Stage"))
                {
                    loadText.text = "���̺� ������ �������� �ʽ��ϴ�.";
                }
                break;
            case ButtonType.Quit: // ���� ���� ��ư�̶��
                Application.Quit();
                break;
            case ButtonType.Option: // �ɼ� ��ư�̶��
                CanvasOn(optonGroup);
                CanvasOff(mainGroup);
                break;
            case ButtonType.Sound: // ���� ��ư�̶��
                isSound = !isSound;
                if (isSound)
                {
                    SoundText.text = "Sound On";
                }
                else
                {
                    SoundText.text = "Sound Off";
                }
                break;
            case ButtonType.Back: // �ɼ��� �ڷΰ��� ��ư�̶��
                CanvasOn(mainGroup);
                CanvasOff(optonGroup);
                break;
        }
    }
    void CanvasOn(CanvasGroup cg) //cg ĵ������ Ŵ
    {
        cg.alpha = 1f;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    void CanvasOff(CanvasGroup cg) // cg ĵ������ ��
    {
        cg.alpha = 0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f; // ���콺�� ��Ҵٸ� ũ�� 1.2��
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale; // ũ�� ���󺹱�
    }
}
