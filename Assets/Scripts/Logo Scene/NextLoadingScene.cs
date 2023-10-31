using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLoadingScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(6);
        Loader.Load(Constants.Scene.LoadingScene);
    }
}
