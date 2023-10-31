using UnityEngine;
using UnityEngine.UI;

public class UiRoadControlller : MonoBehaviour
{
    public float distanceRun;
    public float totalDistance;
    private float distanceRunMax;
    public GameObject player;
    public GameObject iconPlayer;
    public GameObject barMax;
    public Vector3 iconPlayerPos;
    public Text distanceText;
    public Text bestDistanceText;
    public float y;

    private AchievementSystemController achievementSystemController;

    public GameObject winText;
    public GameObject winEffect;
    private void Start()
    {
        achievementSystemController = GameObject.Find("Achivement System").GetComponent<AchievementSystemController>();
        totalDistance = 70000;
        bestDistanceText.text = "BEST:  " + PlayerPrefs.GetFloat("DistanceRunMax").ToString("0") + " M";
        if (PlayerPrefs.GetFloat("BarMax") == 0)
        {
            barMax.transform.position = new Vector3(barMax.transform.position.x, barMax.transform.position.y, barMax.transform.position.z);
            y = -15f;
            PlayerPrefs.SetFloat("BarMax", y);
        }

        else
        {
            barMax.transform.position = new Vector3(barMax.transform.position.x, PlayerPrefs.GetFloat("BarMax"), barMax.transform.position.z);
        }
        iconPlayerPos = iconPlayer.transform.position;
    
    }
    private void Update()
    {
        if (player != null && SetPause.isPause == false)
        {
            distanceRun++;
            distanceText.text = (distanceRun / 1000).ToString("0") + " M"; 

            distanceRunMax = distanceRun / 1000;

            achievementSystemController.distanceRun = distanceRun / 1000;
            iconPlayerPos.y += 1 / 10000f;

            if (iconPlayerPos.y > PlayerPrefs.GetFloat("BarMax"))
            {
                PlayerPrefs.SetFloat("BarMax", iconPlayerPos.y);
                PlayerPrefs.SetFloat("DistanceRunMax", distanceRunMax);
            }

            if (distanceRun >= (totalDistance + 1500))
            {
                winText.SetActive(true);
                Instantiate(winEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }

            iconPlayer.transform.position = iconPlayerPos;
        }
    }
}
