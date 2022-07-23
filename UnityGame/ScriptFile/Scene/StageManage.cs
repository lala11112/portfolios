using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StageManage : MonoBehaviour
{
    private bool isMain = false;
    public AudioClip backgroundClip; // 배경음 클립
    public static StageManage Instance //StageManage 스크립트의 싱글톤 함수
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StageManage>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("CameraMave");
                    instance = instanceContainer.AddComponent<StageManage>();
                }
            }
            return instance;
        }
    }
    private static StageManage instance; // 싱글톤 변수
    public string gameStageName; // 게임 시작 문구
    public string nextScene; // 다음 스테이지의 이름
    public string thisScene; // 지금 씬 이름
    private void Start()
    {
        StartCoroutine(GetMain());
    }
    private void Update()
    {
        if (UIManager.instance.enemyObjects.Length == 0 && !EndDoor.instance.isEnd)
        {
            EndDoor.instance.EndDoorOpen();
        }
    }
    IEnumerator GetMain()
    {
        while(!SceneManager.GetSceneByName("Main").isLoaded)
        {
            yield return null;
        }
        isMain = true;
    }
}
