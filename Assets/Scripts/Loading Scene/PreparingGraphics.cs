using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PreparingGraphics : MonoBehaviour
{
    public Transform loadingBarImage;
    public float targetAmount = 100f;
    public float speed = 30f;
    public Text progressText;
    private string text;

    private float currentAmount = 0f;

    private void Start()
    {
        text = "PREPARING GRAPHICS ...  ";
    }

    private void Update()
    {
        if (currentAmount < targetAmount)
        {
            currentAmount += speed * Time.deltaTime;
            loadingBarImage.GetComponent<Image>().fillAmount = currentAmount / 100f;
            int progress = (int)currentAmount;
            progressText.text = text + progress + " %";
        }

        if (currentAmount >= targetAmount)
        {
            Loader.Load(Constants.Scene.HomeScene);
        }

    }

}

