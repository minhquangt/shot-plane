using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioSource bombAudio;
    public AudioClip explosionSound;
    public GameObject explosion;
    public float explosionForce;
    public float radius;
    void Start()
    { 
        bombAudio = GetComponent<AudioSource>();
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(explosion, transform.position, transform.rotation);
        bombAudio.PlayOneShot(explosionSound, 0.9f);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D collider in colliders)
        {
            MPlayerController bloodPlayer = collider.GetComponent<MPlayerController>();
            if (bloodPlayer != null)
            {
                float distancePlayer = Mathf.Sqrt((Mathf.Pow((transform.position.x - bloodPlayer.transform.position.x), 2) +
                    Mathf.Pow((transform.position.y - bloodPlayer.transform.position.y), 2)));
                bloodPlayer.OnHit(explosionForce / distancePlayer);
            }

            PlayerFollow bloodPlayerFollow = collider.GetComponent<PlayerFollow>();
            if (bloodPlayerFollow != null)
            {
                bloodPlayerFollow.OnHit(5f);
            }
        }
        Destroy(gameObject);
    }

}
