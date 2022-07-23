using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartBtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    public ButtonType currentType; // 버튼의 타입을 담는 변수
    private Transform buttonScale;
    Vector3 defaultScale; // 버튼의 초기 크기
    bool isSound = true; // 사운드가 켜져있는가
    public CanvasGroup mainGroup; // 메인 버튼들의 Group
    public CanvasGroup optonGroup; // 옵션 버튼들의 Group
    public Text SoundText; // 사운드 on, off 텍스트
    public Text loadText; // 세이브로드 텍스트
    private void Start()
    {
        buttonScale = GetComponent<Transform>(); // 버튼의 트랜스폼 초기와
        defaultScale = buttonScale.localScale; // 버튼의 초기 크기 설정
    }
    public void OnBtnClick()
    {
        switch (currentType)
        {
            case ButtonType.New: // 버튼 타입이 New라면 (새로 시작 버튼이라면)
                SceneManager.LoadScene("LoadingScene");
                LoadingNewScene.Instance.NewSceneLoading("Stage1");
                break;
            case ButtonType.Continue: // 다시 시작 버튼이라면
                if (PlayerPrefs.HasKey("Stage")) // 세이브 파일이 존재한다면
                {
                    SceneManager.LoadScene("LoadingScene");
                    string stageName = PlayerPrefs.GetString("Stage");
                    LoadingNewScene.Instance.NewSceneLoading(stageName);
                }
                else if(!PlayerPrefs.HasKey("Stage"))
                {
                    loadText.text = "세이브 파일이 존재하지 않습니다.";
                }
                break;
            case ButtonType.Quit: // 게임 끄기 버튼이라면
                Application.Quit();
                break;
            case ButtonType.Option: // 옵션 버튼이라면
                CanvasOn(optonGroup);
                CanvasOff(mainGroup);
                break;
            case ButtonType.Sound: // 사운드 버튼이라면
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
            case ButtonType.Back: // 옵션의 뒤로가기 버튼이라면
                CanvasOn(mainGroup);
                CanvasOff(optonGroup);
                break;
        }
    }
    void CanvasOn(CanvasGroup cg) //cg 캔버스를 킴
    {
        cg.alpha = 1f;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    void CanvasOff(CanvasGroup cg) // cg 캔버스를 끔
    {
        cg.alpha = 0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f; // 마우스에 닿았다면 크기 1.2배
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale; // 크기 원상복귀
    }
}
