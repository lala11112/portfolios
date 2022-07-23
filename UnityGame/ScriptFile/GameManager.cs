using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance //StageManage 스크립트의 싱글톤 함수
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("CameraMave");
                    Instance = instanceContainer.AddComponent<GameManager>();
                }
            }
            return Instance;
        }
    }
    private static GameManager Instance; // 싱글톤 변수
    public bool isGameover = false; // 게임 오버인가?

    public GameObject gameLable;    // 게임 상태 UI 오브젝트 변수
    Text gameText; // 게임 상태 텍스트
    PlayerHealth player; // 플레이어의 체력

    Vector3 startingPos; // 시작하는 위치 
    Quaternion startingRotate; // 시작하는 방향

    public enum GameState
    {
        Ready, // 게임 준비 상태
        Talk, // 대화중
        Run, // 게임 중
        Menu, // 메뉴창
        GameOver // 게임 오버
    }
    public GameState gameState; // 리스트 변수화
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        // Ready 상태가 된다
        gameState = GameState.Ready;
        startingPos = GameObject.FindGameObjectWithTag("Start").transform.position; // 시작 위치
        // gameLable의 Text를 변수에 넣는다
        gameText = gameLable.GetComponent<Text>();
        // Player 스크립트를 가져온다
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        player.gameObject.transform.position = startingPos;
    }
    public void GameStart(string stageName)
    {
        gameText.text = stageName; // 스테이지 이름
        gameText.color = new Color32(255, 100, 100, 255); // 텍스트 빨간색으로 변경

        gameLable.SetActive(true);
        // ReadyToStart 코루틴을 시작한다
        StartCoroutine(ReadyToStart());
    }
    IEnumerator ReadyToStart()
    {
        yield return new WaitForSeconds(1f);
        gameText.text = "Start!";
        yield return new WaitForSeconds(1f);
        gameLable.SetActive(false);
        gameState = GameState.Run;
    }
    // Update is called once per frame
    void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        if (player.dead == true) //플레이어가 죽었다면
        {
            gameText.text = "GameOver"; // 게임 오버 출력하기
            gameText.color = new Color32(255, 0 , 0, 255); // 빨간색으로 만들기
            gameLable.SetActive(true); // Text 보이게 하기
            Transform buttons = gameText.transform.GetChild(0); // 버튼 모으기
            buttons.gameObject.SetActive(true); // 버튼들 보이게 하기
            gameState = GameState.GameOver; // 게임 오버 상태로 바꾸기
        }
        if (gameState == GameState.Menu) Time.timeScale = 0.01f; // 만약 옵션이라면 게임 속도 0.01
        else Time.timeScale = 1; // 만약 옵션이 아니라면 시간 원래대로
    }
    public void RestartGame() // 재시작 버튼을 누르면 출력되는 함수
    {
        SceneManager.LoadScene("LoadingScene");
        LoadingNewScene.Instance.NewSceneLoading(StageManage.Instance.thisScene);
    }
}