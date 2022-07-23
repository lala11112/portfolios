using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndDoor : MonoBehaviour
{
    Animator animator;
    public bool isEnd = false;
    public static EndDoor instance //StageManage 스크립트의 싱글톤 함수
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<EndDoor>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("CameraMave");
                    Instance = instanceContainer.AddComponent<EndDoor>();
                }
            }
            return Instance;
        }
    }
    private static EndDoor Instance; // 싱글톤 변수
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void EndDoorOpen()
    {
        Debug.Log("ddddddda");
        animator.SetTrigger("EndDoorOpen");
        isEnd = true;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && isEnd)
        {
            SceneManager.LoadScene("LoadingScene");
            LoadingNewScene.Instance.NewSceneLoading(StageManage.Instance.nextScene);
            isEnd = false;
        }
    }
}
