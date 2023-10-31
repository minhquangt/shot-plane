using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonSoundController : MonoBehaviour
{
    public Sprite[] spriteSound;
    public Sprite[] spriteMusic;
    public Image soundImageButton;
    public Image musicImageButton;
    public GameObject soundController;
    public AudioSource musicController;

    private void Start()
    {
        if (PlayerPrefs.GetInt("TurnMusic") == 0)
        {
            musicController.Play();
            musicImageButton.sprite = spriteMusic[0];
        }
        else if (PlayerPrefs.GetInt("TurnMusic") == 1)
        {
            musicController.Pause();
            musicImageButton.sprite = spriteMusic[1];
        }

        if (PlayerPrefs.GetInt("TurnSound") == 0)
        {
            soundController.SetActive(true);
            soundImageButton.sprite = spriteSound[0];
        }
        else if (PlayerPrefs.GetInt("TurnSound") == 1)
        {
            soundController.SetActive(false);
            soundImageButton.sprite = spriteSound[1];
        }
    }

    public void TurnOffOrOnSound()
    {
        if (PlayerPrefs.GetInt("TurnSound") == 0)
        {
            soundController.SetActive(false);
            soundImageButton.sprite = spriteSound[1];
            PlayerPrefs.SetInt("TurnSound", 1);
        }
        else if (PlayerPrefs.GetInt("TurnSound") == 1)
        {
            soundController.SetActive(true);
            soundImageButton.sprite = spriteSound[0];
            PlayerPrefs.SetInt("TurnSound", 0);
        }
    }

    public void TurnOffOrOnMusic()
    {
        if (PlayerPrefs.GetInt("TurnMusic") == 0)
        {
            musicController.Pause();
            musicImageButton.sprite = spriteMusic[1];
            PlayerPrefs.SetInt("TurnMusic", 1);
        }
        else if(PlayerPrefs.GetInt("TurnMusic") == 1)
        {
            musicController.Play();
            musicImageButton.sprite = spriteMusic[0];
            PlayerPrefs.SetInt("TurnMusic", 0);
        }
    }


}
