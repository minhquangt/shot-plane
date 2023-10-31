using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject coin;

    public string enemyName;
    public float fireInterval;
    private float timer;
    public float speed;

    public Image heartEnemyBar;
    public float heartEnemy = 100f;
    public GameObject heartEnemyBG;

    //One Bullet
    public GameObject bulletOne;

    //Many Bullets
    public float power;
    public GameObject bulletsMany;

    //Bullet Target
    public GameObject bulletTarget;
    public GameObject explosionEffect;
    public GameObject destroyEffect;
    public GameObject[] enemyBroken;
    public int numberBroken;

    private AchievementSystemController achievementSystemController;
    public float maxPosCanShoot;
    void Start()
    {
        timer = 0;
        achievementSystemController = GameObject.Find("Achivement System").GetComponent<AchievementSystemController>();
    }

    void Update()
    {
        if (player != null && transform.position.y <= maxPosCanShoot)
        {
            timer += Time.deltaTime;
            if (timer > fireInterval)
            {
                if (enemyName == "enemy0" || enemyName == "enemy1")
                {
                    OneBullet();
                }
                else if (enemyName == "enemy2")
                {
                    ManyBullets();
                }
                else if (enemyName == "enemy4")
                {
                    BulletTarget();
                }

                timer = 0;
            }
        }

        if (transform.position.y <= -5.3f)
        {
            Destroy(gameObject);
        }
    }

    void OneBullet()
    {
        GameObject bullet = Instantiate(bulletOne, transform.position, Quaternion.identity);
        AudioController.Ins.PlaySound(AudioController.Ins.gunSoundEnemy);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
    }

    void ManyBullets()
    {
        if (power % 2 == 0)
        {
            for (int i = 1; i <= power; i++)
            {
                if (i % 2 == 0)
                {
                    GameObject bulletL = Instantiate(bulletsMany, transform.position + Vector3.right * 0.13f * (i - 1), Quaternion.identity);
                    Rigidbody2D rbL = bulletL.GetComponent<Rigidbody2D>();
                    rbL.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
                }
                else
                {
                    GameObject bulletL = Instantiate(bulletsMany, transform.position + Vector3.left * 0.13f * i, Quaternion.identity);
                    Rigidbody2D rbL = bulletL.GetComponent<Rigidbody2D>();
                    rbL.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
                }
            }
        }

        else if (power % 2 != 0)
        {
            for (int i = 1; i <= power; i++)
            {
                if (i == 1)
                {
                    GameObject bullet = Instantiate(bulletsMany, transform.position, Quaternion.identity);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
                }
                else if (i > 1 && i % 2 == 0)
                {
                    GameObject bulletL = Instantiate(bulletsMany, transform.position + Vector3.right * 0.13f * i, Quaternion.identity);
                    Rigidbody2D rbL = bulletL.GetComponent<Rigidbody2D>();
                    rbL.AddForce((Vector2.down + i * new Vector2(0.1f, 0)) * 10, ForceMode2D.Impulse);
                }
                else if (i > 1 && i % 2 != 0)
                {
                    GameObject bulletL = Instantiate(bulletsMany, transform.position + Vector3.left * 0.13f * (i - 1), Quaternion.identity);
                    Rigidbody2D rbL = bulletL.GetComponent<Rigidbody2D>();
                    rbL.AddForce((Vector2.down + i * new Vector2(-0.1f, 0)) * 10, ForceMode2D.Impulse);
                }
            }
        }
        AudioController.Ins.PlaySound(AudioController.Ins.gunsSoundEnemy);
    }

    void BulletTarget()
    {
        GameObject bullet = Instantiate(bulletTarget, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        AudioController.Ins.PlaySound(AudioController.Ins.gunTargetSoundEnemy);

        Vector3 dir = player.transform.position - transform.position;
        rb.AddForce(dir.normalized * 10, ForceMode2D.Impulse);
    }

    public void OnHit(float damage)
    {
        heartEnemy -= damage;
        Instantiate(explosionEffect, transform.position, transform.rotation);
        heartEnemyBar.fillAmount = heartEnemy / 100;

        if (heartEnemy <= 0)
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
            Instantiate(enemyBroken[numberBroken], transform.position, transform.rotation);

            achievementSystemController.enemyDestroy++;

            Destroy(gameObject);
            int random = Random.Range(0, 7);
            if (random == 1)
            {
                Instantiate(coin, transform.position, Quaternion.identity);
            }
            heartEnemyBG.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Constants.Tag.BulletPlayer && transform.position.y <= maxPosCanShoot)
        {
            Bullet bulletPlayer = collision.gameObject.GetComponent<Bullet>();
            OnHit(bulletPlayer.damage);
            Destroy(collision.gameObject);
        }
    }
}
