using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingNewScene : MonoBehaviour, ILoadingScene
{
    public static LoadingNewScene Instance //LoadingNewScene 스크립트의 싱글톤 함수
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LoadingNewScene>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("CameraMave");
                    instance = instanceContainer.AddComponent<LoadingNewScene>();
                }
            }
            return instance;
        }
    }
    private static LoadingNewScene instance; // 싱글톤 변수
    public Slider loadingSlider; // 로딩 슬라이더 바
    public Text loadingText; // 로딩 진행 텍스트
    public Text isloadingText; // 로딩중 텍스트
    public void NewSceneLoading(string sceneName) // 스테이지를 생성하는 함수
    {
        StartCoroutine(TransitionNextScene(sceneName)); // 씬 비동기 로딩
    }
    IEnumerator TransitionNextScene(string str) //씬을 로드하는 코루틴
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(str, LoadSceneMode.Additive); // 씬 비동기 형식으로 로드하기
        AsyncOperation ao_main = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
        ao.allowSceneActivation = false; // 로드되는 씬 안보이게 하기
        ao_main.allowSceneActivation = false; // 로드되는 씬 안보이게 하기
        while (!ao.isDone && !ao_main.isDone) // 씬 로드가 안되었다면
        {
            loadingSlider.value = ao.progress;
            loadingText.text = (ao.progress * 100f).ToString() + "%";
            if (ao.progress >= 0.9f && ao_main.progress >= 0.9f)
            {
                SceneManager.UnloadSceneAsync("LoadingScene");
                ao.allowSceneActivation = true; // 로드 되는 씬 보이기
                ao_main.allowSceneActivation = true; // 로드 되는 씬 보이기
                SceneManager.UnloadSceneAsync("LoadingScene");
            }
            yield return null;
        }
    }
    
}