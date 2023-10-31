using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchievementSystemController : MonoBehaviour
{
    public Text goldCollectedText;
    public Text distanceText;
    public Text enemiesDestroyedText;
    public Text alliesFreedText;
    public Text alliesGatheredText;

    public Text coinInt;
    public Text distanceInt;
    public Text enemiesDestroyedInt;
    public Text alliesFreedInt;
    public Text alliesGatheredInt;

    private float coinCurrent;
    private float distanceCurrent;
    private float alliesFreedCurrent;
    private float alliesGatheredCurrent;

    public int coinCollect;
    public float distanceRun;
    public int enemyDestroy;
    public int alliesFreed;
    public void Update()
    {
        GoldCollected();

        CountingCoin(coinCollect);

        RunDistance();

        CountingDistance(distanceRun);

        EnemiesDestroyed();
        
        CountingEnemiesDestroyed();

        AlliesFreed();

        CountingAlliesFreed(alliesFreed);

        AlliesGathered();

        CountingAlliesGathered(alliesFreed + 1);
    }
    public void GoldCollected()
    {
        goldCollectedText.text = "GOLD COLLECTED ";
    }
    public void RunDistance()
    {
        distanceText.text = "RUN DISTANCE ";
    }
    public void EnemiesDestroyed()
    {
        enemiesDestroyedText.text = "ENEMIES DESTROYED ";
    }
    public void AlliesFreed()
    {
        alliesFreedText.text = "ALLIES FREED ";

    }
    public void AlliesGathered()
    {
        alliesGatheredText.text = "ALLIES GATHERED ";
    }

    public void OKButton()
    {
        Loader.Load(Constants.Scene.HomeScene);
    }

    public void CountingCoin(int desiredNumber)
    {
        coinCurrent += 1;
        if (coinCurrent >= desiredNumber)
        {
            coinCurrent = desiredNumber;
            coinInt.text = coinCurrent.ToString("0");
            return;
        }
        coinInt.text = coinCurrent.ToString("0");
    }

    public void CountingDistance(float desiredNumber)
    {
        distanceCurrent += 1;
        if (distanceCurrent >= desiredNumber)
        {
            distanceCurrent = desiredNumber;
            distanceInt.text = distanceCurrent.ToString("0") + " M";
            return;
        }
        distanceInt.text = distanceCurrent.ToString("0") + " M";
    }
    public void CountingEnemiesDestroyed()
    {
        enemiesDestroyedInt.text = enemyDestroy.ToString("0");
    }
    public void CountingAlliesFreed(int desiredNumber)
    {
        alliesFreedCurrent += 0.005f;
        if (alliesFreedCurrent >= desiredNumber)
        {
            alliesFreedCurrent = desiredNumber;
            alliesFreedInt.text = alliesFreedCurrent.ToString("0");
            return;
        }
        alliesFreedInt.text = alliesFreedCurrent.ToString("0");
    }

    public void CountingAlliesGathered(int desiredNumber)
    {
        alliesGatheredCurrent += 0.005f;
        if (alliesGatheredCurrent >= desiredNumber)
        {
            alliesGatheredCurrent = desiredNumber;
            alliesGatheredInt.text = alliesGatheredCurrent.ToString("0");
            return;
        }
        alliesGatheredInt.text = alliesGatheredCurrent.ToString("0");
    }

}

