using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CutSceneManager : MonoBehaviour
{

    public Dictionary<string, UnityEngine.SceneManagement.LoadSceneMode> loadScenes = new Dictionary<string, UnityEngine.SceneManagement.LoadSceneMode>();
    void InitSceneInfo() // 호출할 씬 모음
    {
        loadScenes.Add("MainScene1", LoadSceneMode.Additive);
        loadScenes.Add("MainScene2", LoadSceneMode.Additive);
    }
    public void Start()
    {
        InitSceneInfo();

        foreach (var _loadScene in loadScenes)
        {
            if (_loadScene.Key == StageManage.Instance.nextScene)
            {
                //SceneManager.LoadScene(_loadScene.Key, _loadScene.Value);
            }
        }
    }
}
