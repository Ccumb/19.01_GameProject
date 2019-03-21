using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 메인메뉴씬에서 배경의 씬을 랜덤으로 나오게 할 것. 메인메뉴 씬의 Canvas에 붙여주기만 하면 된다.
/// 랜덤으로 지정된 숫자에 게임플레이 씬이 있어야 한다. 주의바람
/// </summary>
public class LoadRandomScene : MonoBehaviour
{
    public void LoadRandomScenes()
    {
        int index = Random.Range(1, 5);
        SceneManager.LoadScene(index);
        Debug.Log("Scene Loaded");
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        LoadRandomScenes();
    }
}
