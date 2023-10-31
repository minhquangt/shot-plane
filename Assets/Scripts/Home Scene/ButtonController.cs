using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class ButtonController : MonoBehaviour
{
    public GameObject homeCanvas;
    public GameObject upgradesCanvas;

    public void PlayButton()
    {
        Loader.Load(Constants.Scene.MainScene);
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OKButton()
    {
        homeCanvas.SetActive(true);
        upgradesCanvas.SetActive(false);
    }

    public void UpgradesButton()
    {
        homeCanvas.SetActive(false);
        upgradesCanvas.SetActive(true);
    }
}