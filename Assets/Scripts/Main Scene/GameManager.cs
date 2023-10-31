using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ironCage;
    public GameObject gameOverUI;
    public GameObject dieText;
    public GameObject[] barrel;

    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;
    public float spawnInterval0;
    public float spawnInterval1;
    public float spawnInterval2;
    public float spawnInterval3;
    public float spawnInterval4;

    private float timer0;
    private float timer1;
    private float timer2;
    private float timer3;
    private float timer4;

    public GameObject player;
    public bool isEnemy3;
    public float speed;
    private UiRoadControlller uiRoadControlller;

    private int randomPoint1;
    private int randomPointRemaining;
    private int randomPointBarel;
    public int countFollowerAppear;
    void Start()
    {
        uiRoadControlller = GameObject.Find("UIRoad").GetComponent<UiRoadControlller>();
        spawnInterval1 = spawnInterval0 * 4;
        spawnInterval2 = spawnInterval0 * 7;
        spawnInterval3 = spawnInterval0 * 5;
        spawnInterval4 = spawnInterval0 * 3;
    }
    void Update()
    {
        if (player != null)
        {
            StopSpawn();
            Spawn0();
            Spawn1();
            Spawn2();
            Spawn3();
            Spawn4();

            MPlayerController mPlayerController = player.GetComponent<MPlayerController>();
            if (mPlayerController.heartPlayer <= 0)
            {
                AudioController.Ins.PlaySound(AudioController.Ins.dieSoundPlayer);
                dieText.SetActive(true);
                Destroy(player.gameObject);
                StartCoroutine(GameOver());
            }
        }
    }

    void Spawn0()
    {
        timer0 += Time.deltaTime;
        if (timer0 > spawnInterval0 && uiRoadControlller.distanceRun < uiRoadControlller.totalDistance)
        {
            SpawnEnemy(0);
            SpawnBarrel();

            timer0 = 0;
        }
    }

    void Spawn1()
    {
        if (uiRoadControlller.distanceRun >= uiRoadControlller.totalDistance * 0.1f && uiRoadControlller.distanceRun < uiRoadControlller.totalDistance)
        {
            timer1 += Time.deltaTime;
            if (timer1 > spawnInterval1)
            {
                SpawnEnemy(1);

                timer1 = 0;
            }
        }
    }

    void Spawn2()
    {
        if (uiRoadControlller.distanceRun >= uiRoadControlller.totalDistance * 0.25f && uiRoadControlller.distanceRun < uiRoadControlller.totalDistance)
        {
            timer2 += Time.deltaTime;
            if (timer2 > spawnInterval2)
            {
                SpawnEnemy(2);

                timer2 = 0;
            }
            spawnInterval1 = spawnInterval0 * 3.5f;
        }
    }

    void Spawn3()
    {
        if (uiRoadControlller.distanceRun >= uiRoadControlller.totalDistance * 0.5f && uiRoadControlller.distanceRun < uiRoadControlller.totalDistance)
        {
            timer3 += Time.deltaTime;
            if (timer3 > spawnInterval3)
            {
                SpawnEnemy(3);

                timer3 = 0;
            }
            spawnInterval1 = spawnInterval0 * 2.5f;
            spawnInterval2 = spawnInterval0 * 3.5f;
        }

    }

    void Spawn4()
    {
        if (uiRoadControlller.distanceRun >= uiRoadControlller.totalDistance * 0.8f && uiRoadControlller.distanceRun < uiRoadControlller.totalDistance)
        {
            timer4 += Time.deltaTime;
            if (timer4 > spawnInterval4)
            {
                SpawnEnemy(4);

                timer4 = 0;
            }
            spawnInterval1 = spawnInterval0 * 1.5f;
            spawnInterval2 = spawnInterval0 * 2.5f;
            spawnInterval3 = spawnInterval0 * 3.5f;
        }
    }

    void StopSpawn()
    {
        if (uiRoadControlller.distanceRun >= uiRoadControlller.totalDistance)
        {
            return;
        }
    }
    void SpawnEnemy(int numberEnemy)
    {
        if (numberEnemy == 1)
        {
            randomPoint1 = Random.Range(5, 7);

            GameObject enemy = Instantiate(enemyObjs[numberEnemy], spawnPoints[randomPoint1].position, spawnPoints[randomPoint1].rotation);

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            Enemy enemyTemp = enemy.GetComponent<Enemy>();

            enemyTemp.player = player;
        }
        else
        {
            randomPointRemaining = Random.Range(0, 5);
            GameObject enemy = Instantiate(enemyObjs[numberEnemy], spawnPoints[randomPointRemaining].position, spawnPoints[randomPointRemaining].rotation);

            if (numberEnemy == 3)
            {
                Bomb bomb = enemy.GetComponent<Bomb>();
                bomb.player = player;
            }
            else
            {
                Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
                Enemy enemyTemp = enemy.GetComponent<Enemy>();
                enemyTemp.player = player;
                rb.velocity = new Vector2(0, speed);
            }
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3f);
        gameOverUI.SetActive(true);
        gameObject.SetActive(false);
    }

    void SpawnBarrel()
    {
        randomPointBarel = Random.Range(0, 5);
        if (randomPointBarel == randomPointRemaining)
        {
            return;
        }
        int randomBarrel = Random.Range(0, 2);
        countFollowerAppear++;
        if (countFollowerAppear >= 8)
        {
            randomBarrel = 2;
            countFollowerAppear = 0;
        }
        GameObject barrelTemp = Instantiate(barrel[randomBarrel], spawnPoints[randomPointBarel].position, spawnPoints[randomPointBarel].rotation);
        Rigidbody2D rb = barrelTemp.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, speed);
    }
}
