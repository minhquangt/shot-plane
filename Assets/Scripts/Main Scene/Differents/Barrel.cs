using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField]
    int health;
    [SerializeField]
    GameObject destructibleRef;

    public GameObject particle;
    public GameObject[] explosion;
    public GameObject[] reward;
    public GameObject playerFollow;
    public int radius;
    public float explosionForce;

    public string barrelName;
    public int countIronCageCollider;
    public float maxPosCanShoot;
    private void Update()
    {
        if (transform.position.y <= -5.3f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.Tag.BulletPlayer) && transform.position.y <= maxPosCanShoot)
        {
            Destroy(collision.gameObject);
            health--;
            if (barrelName == "barrel" || barrelName == "box")
            {
                Instantiate(particle, transform.position, transform.rotation);
                if (health <= 0 && barrelName == "barrel")
                {
                    ExplosionBarrel();
                }
                else if (health <= 0 && barrelName == "box")
                {
                    ExplosionBox();
                }
            }
               
            else if ( barrelName == "iron cage")
            {
                countIronCageCollider++;
                if (health <= 0)
                {
                    ExplosionIronCage();
                }
            }
        }

        else if (collision.CompareTag(Constants.Tag.Player))
        {
            if (barrelName == "barrel")
            {
                ExplosionBarrel();
            }
        }
    }

    void ExlodeThisGameObject()
    {
        Destroy(gameObject);
        Instantiate(destructibleRef, transform.position, transform.rotation);
    }

    public void ExplosionBarrel()
    {
        ExlodeThisGameObject();
        Instantiate(explosion[0], transform.position, Quaternion.identity);

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

            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                float distanceEnemy = Mathf.Sqrt((Mathf.Pow((transform.position.x - enemy.transform.position.x), 2) +
                    Mathf.Pow((transform.position.y - enemy.transform.position.y), 2)));
                enemy.OnHit(explosionForce / distanceEnemy);
            }

            PlayerFollow playerFollow = collider.GetComponent<PlayerFollow>();
            if (playerFollow != null)
            {
                float distancePlayerFollow = Mathf.Sqrt((Mathf.Pow((transform.position.x - playerFollow.transform.position.x), 2) +
                    Mathf.Pow((transform.position.y - playerFollow.transform.position.y), 2)));
                playerFollow.OnHit(explosionForce / distancePlayerFollow);
            }
        }
    }
    public void ExplosionBox()
    {
        ExlodeThisGameObject();

        Instantiate(explosion[1], transform.position, Quaternion.identity);
        int randomReward = Random.Range(0, 2);
        Instantiate(reward[randomReward], transform.position, Quaternion.identity);
    }

    public void ExplosionIronCage()
    {
        ExlodeThisGameObject();
        Instantiate(particle, new Vector3(transform.position.x, transform.position.y - 0.7f, 0), transform.rotation);
        if (countIronCageCollider == 1)
        {
            GameObject playerFollowTemp = Instantiate(playerFollow, transform.position, transform.rotation);
            Rigidbody2D rb = playerFollowTemp.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, -0.5f);
        }
    }
}

