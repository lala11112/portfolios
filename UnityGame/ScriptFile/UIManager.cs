using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public GameObject itemBackGr; // 아이템 UI 뒤창
    public Image itemImage; // 아이템 UI이미지
    public Text itemText; // 아이템 UI텍스트

    public Text enemyObjectText; // 남은 적의 수를 표시할 Text
    public Button gameStartButton; // 게임 시작 버튼
    public GameObject[] enemyObjects; // 적의 수
    public GameObject MenuSet; // 메뉴 버튼들
    public GameObject player;
    public TalkManager talkManager;
    public bool isTalk = false;
    public GameObject scanObject;
    public GameObject talkPanel; // 대화창
    public int talkIndex; // 대화 번호
    public TypeEffect talk; // 대화 텍스트의 이펙트 스크립트
    public GameObject talkObject; // 대화 오브젝트
    public Image portraitImg; // 초상화 이미지
    
    public static UIManager instance //StageManage 스크립트의 싱글톤 함수
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("CameraMave");
                    Instance = instanceContainer.AddComponent<UIManager>();
                }
            }
            return Instance;
        }
    }
    private static UIManager Instance; // 싱글톤 변수
    public void EnemyObjectNumber(int enemyObjectNum) // 스테이지 메니저에서 업데이트함
    {
        if (GameManager.instance.gameState == GameManager.GameState.Run || GameManager.instance.gameState == GameManager.GameState.GameOver) // 게임오버나 게임 진행중이라면
        {
            enemyObjectText.gameObject.SetActive(true); // 적의 수 보이기
        }
        else if (GameManager.instance.gameState == GameManager.GameState.Ready) // 게임 준비상태라면
        {
            enemyObjectText.gameObject.SetActive(false); // 적의 수 숨기기
        }
        enemyObjectText.text = "남은 적의 수:" + enemyObjectNum;
    }
    private void Start()
    {
        itemBackGr.SetActive(false); // item이 처음엔 안보임
    }

    public void ItemUIImage(SpriteRenderer _itemImage) // 아이템 이미지 바꾸기
    {
        itemImage.sprite = _itemImage.sprite; // 이미지 바꾸기
        itemBackGr.SetActive(true); // item 보이기
        StartCoroutine(UIFalse(itemBackGr, new WaitForSeconds(1f))); // 1초후에 숨기기
    }
    public void ItemUIText(string explanationText)
    {
        itemText.text = explanationText;
    }
    IEnumerator UIFalse(GameObject UIObject, WaitForSeconds delay) // 몇초뒤에 UI를 안보이게 함
    {
        yield return delay;
        UIObject.SetActive(false);
    }
    public void GameStart() // 게임 시작 버튼을 누르면 실행되는 함수
    {
        gameStartButton.gameObject.SetActive(false);
        GameManager.instance.GameStart(StageManage.Instance.gameStageName);
    }
    public void Continue()
    {
        MenuSet.SetActive(false); // 숨기기
        GameManager.instance.gameState = GameManager.GameState.Run;
    }
    public void GameExit() // 게임 종료하기
    {
        Application.Quit();
    }
    public void GameSave() // 게임 저장
    {
        PlayerPrefs.SetString("Stage", StageManage.Instance.thisScene);
        PlayerPrefs.Save();

        Debug.Log(PlayerPrefs.GetString("Stage"));
    }
    public void GameLoad() // 게임 로드
    {
        if (!PlayerPrefs.HasKey("Stage")) return;

        string stageName = PlayerPrefs.GetString("Stage");
        LoadingNewScene.Instance.NewSceneLoading(stageName);

    }
    public void TalkAction(GameObject scanObj)
    {

        scanObject = scanObj;
        ObjectType objectType = scanObject.GetComponent<ObjectType>();
        Talk(objectType.id);
        talkPanel.SetActive(isTalk);
    }
    void Talk(int id)
    {
        string talkData = "";
        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        else talkData = talkManager.GetTalk(id, talkIndex);// isAnim가 꺼져있다면 (대화중이라면)
        if (talkData == null) // 대화 길이가 끝났다면 그리고 Npc가 아니라면
        {
            isTalk = false; // 대화창 끄기
            talkIndex = 0; // 대화 길이 초기화
            Destroy(talkObject); // 대화했던 오브젝트 파괴
            return;
        }
        talk.SetMsg(talkData.Split(':')[0]); // 스플릿을 통해 구분
        portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1])); // 형변환으로 int로 만든다.
        portraitImg.color = new Color32(255, 255, 255, 255);
        isTalk = true; // 대화창 켜기
        talkIndex++; // 대화 한 내용 +1
    }
    private void Update()
    {

        if (GameManager.instance.gameState != GameManager.GameState.Run) // 게임 진행중이 아니라면
        {
            enemyObjectText.gameObject.SetActive(false); // UI 안보이기
        }
        else
        {
            enemyObjectText.gameObject.SetActive(true); // UI 보이기
        }
        if (Input.GetKeyDown("escape")) // esc키를 눌렀을때
        {
            if (MenuSet.activeSelf) // 메뉴창이 켜져있다면
            {
                MenuSet.SetActive(false); // 메뉴창 숨기기
                GameManager.instance.gameState = GameManager.GameState.Run;
            }
            else
            {
                MenuSet.SetActive(true); // 보이기
                GameManager.instance.gameState = GameManager.GameState.Menu;
            }
        }
        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy"); // enemy태그의 오브젝트 모두 찾기

        UIManager.instance.EnemyObjectNumber(enemyObjects.Length); // UI메니저에서 몬스터 수 초기화
        if (GameManager.instance.gameState == GameManager.GameState.Talk) // 만약 대화상태라면
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")) // 스페이스 바, 좌클릭을 했다면
            {
                TalkAction(talkObject); // 대화 불러오기
            }
        }
    }

}