using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class MPlayerController : MonoBehaviour
{
    public Vector2 vectorAdd;
    public float radius;

    public GameObject bulletObjA;

    public GameObject[] allies;

    public float speed;
    public float fireInterval;
    private float timer;

    public float heartPlayer;
    private int coin;
    public int allyCount;

    private Vector3 targetPosition;

    public Text coinUI;
    public Image heartPlayerBar;

    public Transform[] allyPositionTransforms;

    public GameObject plusParticle;
    public GameObject eatCoinText;
    public GameObject riflemanText;

    private AchievementSystemController achievementSystemController;
    public float leftPos;
    public float rightPos;
    public float topPos;
    public float bottomPos;
    public bool isReplace;

    void Start()
    {
        achievementSystemController = GameObject.Find("Achivement System").GetComponent<AchievementSystemController>();
        heartPlayer = 100;
        coin = 0;
        timer = 0;
        coin = PlayerPrefs.GetInt("Coin");
        coinUI.text = " " + coin;
        allies = new GameObject[allyPositionTransforms.Length];
    }

    void Update()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (worldPosition.x > leftPos && worldPosition.x < rightPos && worldPosition.y > bottomPos && worldPosition.y < topPos)
        {
            targetPosition = new Vector2(worldPosition.x, worldPosition.y) + vectorAdd;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
        }

        timer += Time.deltaTime;
        if (timer > fireInterval)
        {
            SetUpgradePlayer(PlayerPrefs.GetInt("UpdateSkillPlayer"));
            AudioController.Ins.PlaySound(AudioController.Ins.gunSoundPlayer);
            timer = 0;
        }

        if (allyCount >= 12)
        {
            allyCount = 0;
            isReplace = true;
        }

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(Constants.Tag.PlayerFollow))
            {
                PlayerFollow playerFollow = hitCollider.GetComponent<PlayerFollow>();
                if (allyCount >= 6)
                {
                    Destroy(allies[allyCount - 6].gameObject);
                    allies[allyCount - 6] = hitCollider.gameObject;
                    playerFollow.Activate(allyPositionTransforms[allyCount - 6], allyCount - 6);
                }
                else
                {
                    if (isReplace)
                    {
                        Destroy(allies[allyCount].gameObject);
                    }
                    allies[allyCount] = hitCollider.gameObject;
                    playerFollow.Activate(allyPositionTransforms[allyCount], allyCount);
                }
                allyCount++;
                Instantiate(riflemanText, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);

                achievementSystemController.alliesFreed++;
            }
        }
    }

    public void OnHit(float damage)
    {
        heartPlayer -= damage;
        heartPlayerBar.fillAmount = heartPlayer / 100;
    }

    void SetUpgradePlayer(int upgrade)
    {
        int power;
        switch (upgrade)
        {
            case 0:
                power = 1;
                Fire(power);
                break;
            case 1:
                power = 2;
                Fire(power);
                break;
            case 2:
                power = 3;
                Fire(power);
                break;
            case 3:
                power = 4;
                Fire(power);
                break;
            case 4:
                power = 5;
                Fire(power);
                break;
            case 5:
                power = 6;
                Fire(power);
                break;
            case 6:
                power = 7;
                Fire(power);
                break;
            case 7:
                power = 8;
                Fire(power);
                break;
            case 8:
                power = 9;
                Fire(power);
                break;
            case 9:
                power = 10;
                Fire(power);
                break;
        }
    }

    void Fire(int power)
    {
        if (power % 2 == 0)
        {
            for (int i = 1; i <= power; i++)
            {
                if (i % 2 == 0)
                {
                    GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.right * 0.13f * (i - 1), Quaternion.identity);
                    Rigidbody2D rbL = bulletL.GetComponent<Rigidbody2D>();
                    rbL.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
                }
                else
                {
                    GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.13f * i, Quaternion.identity);
                    Rigidbody2D rbL = bulletL.GetComponent<Rigidbody2D>();
                    rbL.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
                }
            }
        }

        else if (power % 2 != 0)
        {
            for (int i = 1; i <= power; i++)
            {
                if (i == 1)
                {
                    GameObject bullet = Instantiate(bulletObjA, transform.position, Quaternion.identity);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
                }
                else if (i > 1 && i % 2 == 0)
                {
                    GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.right * 0.13f * i, Quaternion.identity);
                    Rigidbody2D rbL = bulletL.GetComponent<Rigidbody2D>();
                    rbL.AddForce((Vector2.up + i * new Vector2(0.1f, 0)) * 8, ForceMode2D.Impulse);
                }
                else if (i > 1 && i % 2 != 0)
                {
                    GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.13f * (i - 1), Quaternion.identity);
                    Rigidbody2D rbL = bulletL.GetComponent<Rigidbody2D>();
                    rbL.AddForce((Vector2.up + i * new Vector2(-0.1f, 0)) * 8, ForceMode2D.Impulse);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Constants.Tag.BulletEnemy)
        {
            Bullet bulletEnemy = collision.gameObject.GetComponent<Bullet>();
            OnHit(bulletEnemy.damage);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag(Constants.Tag.Coin))
        {
            AudioController.Ins.PlaySound(AudioController.Ins.coinSound);
            coin += 10;
            achievementSystemController.coinCollect += 10;

            coinUI.text = " " + coin;
            Instantiate(eatCoinText, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            Destroy(collision.gameObject);
            PlayerPrefs.SetInt("Coin", coin);
            PlayerPrefs.Save();
        }
        if (collision.CompareTag(Constants.Tag.MedicalBox))
        {
            heartPlayer += 10;
            if (heartPlayer >= 100)
            {
                heartPlayer = 100;
            }
            heartPlayerBar.fillAmount = heartPlayer / 100;
            Destroy(collision.gameObject);
            Instantiate(plusParticle, transform.position, transform.rotation);
        }
    }

    public void UpdateAlliesPosition(int deathAllyIndex)
    {
        // Filter remained allies
        List<GameObject> remainedAllies = new List<GameObject>(allyPositionTransforms.Length);
        for (int i = 0; i < allyPositionTransforms.Length; i++)
        {
            if (allies[i] != null && i != deathAllyIndex)
            {
                remainedAllies.Add(allies[i]);
                allies[i] = null;
            }
        }
        // Re-assign allies and allies target transform
        GameObject[] alliesTemp = remainedAllies.ToArray();

        for (int i = 0; i < alliesTemp.Length; i++)
        {
            allies[i] = alliesTemp[i];
        }

        for (int i = 0; i < allyPositionTransforms.Length; i++)
        {
            if (allies[i] != null)
            {
                allies[i].GetComponent<PlayerFollow>().SetTarget(allyPositionTransforms[i], i);
            }
        }

        allyCount = alliesTemp.Length;
    }
}



