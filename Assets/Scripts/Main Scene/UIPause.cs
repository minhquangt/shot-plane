using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPause : MonoBehaviour
{
    public GameObject panelPause;
    public void Pause()
    {
        Time.timeScale = 0;
        panelPause.SetActive(true);
        SetPause.isPause = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        panelPause.SetActive(false);
        SetPause.isPause = false;
    }

    public void Replay()
    {
        Time.timeScale = 1;
        Loader.Load(Constants.Scene.MainScene);
        SetPause.isPause = false;
    }

    public void Home()
    {
        Time.timeScale = 1;
        Loader.Load(Constants.Scene.HomeScene);
        SetPause.isPause = false;
    }
}
