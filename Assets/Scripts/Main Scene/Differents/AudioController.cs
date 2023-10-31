using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Ins;
    [Range(0, 1)]
    public float soundVolume;

    public AudioSource soundAus;

    public AudioClip coinSound;
    public AudioClip dieSoundPlayer;
    public AudioClip gunSoundPlayer;
    public AudioClip explosionSoundEnemy;
    public AudioClip gunSoundEnemy;
    public AudioClip gunsSoundEnemy;
    public AudioClip gunTargetSoundEnemy;
    public AudioClip gunTargetSoundFollower;
    public AudioClip boomSoundFollower;

    private void Awake()
    {
        MakeSingleton();
    }
    void Start()
    {
        
    }

    public void PlaySound(AudioClip sound)
    {
        if (soundAus && sound)
        {
            soundAus.volume = soundVolume;
            soundAus.PlayOneShot(sound);
        }
    }
    void Update()
    {
        
    }

    void MakeSingleton()
    {
        if (Ins == null)
        {
            Ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
