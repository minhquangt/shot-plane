using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerFollow : MonoBehaviour
{
    public GameObject ironCage;
    public float fireInterval;
    private float timer;
    public bool isActivated;

    public Image heartBarPlayerFollow;
    public float heartPlayerFollow;
    public Slider slider;
    public Gradient gradient;

    public Transform targetTransform;
    public int targetTransformIndex;
    private bool hasTarget;
    public GameObject player;
    public GameObject arrowdown;

    //One Bullet
    public GameObject bulletOne;

    //Boom
    public GameObject boom;
    public GameObject explosion;
    public float explosionForce;
    public float radius;

    //Bullet Target
    public GameObject bulletTarget;

    public int randomPlayerFollow;
    void Start()
    {
        if (PlayerPrefs.GetInt("UpdateSkillFollower") > 0)
        {
            randomPlayerFollow = Random.Range(0, PlayerPrefs.GetInt("UpdateSkillFollower") + 1);
        }
        timer = 0;
        heartPlayerFollow = 30f;

        isActivated = false;
        player = GameObject.FindGameObjectWithTag(Constants.Tag.Player);
    }

    void Update()
    {
        if (player != null && hasTarget)
        {
            transform.position = targetTransform.position;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            timer += Time.deltaTime;
            if (timer > fireInterval)
            {
                SetUpgradePlayer(randomPlayerFollow);
                timer = 0;
            }
        }
    }

    void SetUpgradePlayer(int upgrade)
    {
        switch (upgrade)
        {
            case 0:
                Fire();
                break;
            case 1:
                StartCoroutine(Bomb());
                break;
            case 2:
                FireTarget();
                break;
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletOne, transform.position, Quaternion.identity);
        AudioController.Ins.PlaySound(AudioController.Ins.gunSoundPlayer);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }

    IEnumerator Bomb()
    {
        GameObject boomTemp = Instantiate(boom, transform.position, Quaternion.identity);
        AudioController.Ins.PlaySound(AudioController.Ins.boomSoundFollower);

        Rigidbody2D rb = boomTemp.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        Destroy(boomTemp, 1.2f);
        yield return new WaitForSeconds(1.15f);
        if (boomTemp != null)
        {
            Instantiate(explosion, boomTemp.transform.position, Quaternion.identity);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(boomTemp.transform.position, radius);

            foreach (Collider2D collider in colliders)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                Barrel explosiveBarrel = collider.GetComponent<Barrel>();
                if (enemy != null && enemy.transform.position.y < 4.7f)
                {
                    float distance = Mathf.Sqrt((Mathf.Pow((boomTemp.transform.position.x - enemy.transform.position.x), 2) +
                        Mathf.Pow((boomTemp.transform.position.y - enemy.transform.position.y), 2)));
                    enemy.OnHit(explosionForce / distance);
                }

                if (explosiveBarrel != null && explosiveBarrel.transform.position.y < 4.7f)
                {
                    if(explosiveBarrel.barrelName == "barrel")
                    {
                        explosiveBarrel.ExplosionBarrel();
                    }
                    else if (explosiveBarrel.barrelName == "box")
                    {
                        explosiveBarrel.ExplosionBox();
                    }
                }

            }
        }
    }

    void FireTarget()
    {
        GameObject bulletTargetTemp = Instantiate(bulletTarget, transform.position, Quaternion.identity);
        if (bulletTargetTemp.GetComponent<FireTarget>().enemy != null)
        {
            AudioController.Ins.PlaySound(AudioController.Ins.gunTargetSoundFollower);
        }
    }

    public void Activate(Transform target, int index)
    {
        targetTransform = target;
        targetTransformIndex = index;
        isActivated = true;
        hasTarget = true;
        arrowdown.SetActive(false);
    }

    public void SetTarget(Transform target, int index)
    {
        targetTransform = target;
        targetTransformIndex = index;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Constants.Tag.BulletEnemy && hasTarget)
        {
            Bullet bulletEnemy = collision.gameObject.GetComponent<Bullet>();
            OnHit(bulletEnemy.damage);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == Constants.Tag.BorderBullet)
        {
            Destroy(gameObject);
        }
    }

    public void OnHit(float damage)
    {
        heartPlayerFollow -= damage;
        slider.value = heartPlayerFollow;
        heartBarPlayerFollow.color = gradient.Evaluate(slider.normalizedValue);
        if (heartPlayerFollow <= 0f && player != null)
        {
            player.GetComponent<MPlayerController>().UpdateAlliesPosition(targetTransformIndex);
            Destroy(gameObject);
        }
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}


