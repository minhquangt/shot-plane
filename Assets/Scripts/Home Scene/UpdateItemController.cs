using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateItemController : MonoBehaviour
{
    public bool isMobile;
    public bool isPC;
    public GameObject dialogBuy;
    public GameObject dialogPoor;
    public int temp;
    public Text[] priceText;
    public Text[] titleText;
    public Text[] descriptionText;

    public Button[] button;

    public int[] unlock;
    public int[] bought;
    public CoinPlayer coinPlayer;

    private Data data;
    void Awake()
    {
        data = GameObject.Find("Data").GetComponent<Data>();
    }

    void Start()
    {
        if (isMobile)
        {
            AddClickEventsMobile();
        }
        else if (isPC)
        {
            AddClickEventsPC();
        }
        for (int i = 0; i < titleText.Length; i++)
        {
            titleText[i].text = data.title[i];
            descriptionText[i].text = data.descriptions[i];
            priceText[i].text = data.price[i].ToString();
            if (PlayerPrefs.GetInt("ChangeImage" + (i + 1)) ==  (i + 1))
            {
                bought[i] = i;
                button[i].GetComponent<Image>().sprite = data.spriteBought[i];
            }
            else
            {
                bought[i] = titleText.Length + 1;
                button[i].GetComponent<Image>().sprite = data.sprite[i];
            }

        }
    }

    void AddClickEventsPC()
    {
        int indexTemp = 0;
        foreach (Button item in button)
        {
            int index = indexTemp;
            item.onClick.AddListener(() => BuyPC(index));
            indexTemp++;
        }
    }

    void AddClickEventsMobile()
    {
        int indexTemp = 0;
        foreach (Button item in button)
        {
            int index = indexTemp;
            item.onClick.AddListener(() => BuyMobile(index));
            indexTemp++;
        }
    }

    private void Update()
    {
        for (int i = 0; i < titleText.Length; i++)
        {
            if (coinPlayer.coin >= data.price[i] && bought[i] == 31 && data.price[i] != 0)
            {
                button[i].GetComponent<Image>().color = new Color(0, 255, 255, 180);
                unlock[i] = i;
            }

            else
            {
                button[i].GetComponent<Image>().color = Color.white;
                unlock[i] = 31;
            }
        }
    }

    void BuyPC(int i)
    {
        if (unlock[i] != i)
        {
            return;
        }

        else if (unlock[i] == i)
        {
            bought[i] = i;
            coinPlayer.coin -= data.price[i];
            PlayerPrefs.SetInt("Coin", coinPlayer.coin);
            coinPlayer.coinText.text = coinPlayer.coin.ToString();
            button[i].GetComponent<Image>().sprite = data.spriteBought[i];
            PlayerPrefs.SetInt("ChangeImage" + (i + 1), (i + 1));
            if (i > 0 && i < 10)
            {
                PlayerPrefs.SetInt("UpdateSkillPlayer", i);
            }
            else if (i >= 10 && i < 20)
            {
                PlayerPrefs.SetInt("UpdateSkillFollower", (i - 10));
            }
        }
    }

    void BuyMobile(int i)
    {
        if (bought[i] == i && data.price[i] != 0)
        {
            return;
        }
        else if (unlock[i] != i && data.price[i] != 0)
        {
            dialogPoor.SetActive(true);
        }
        else if (unlock[i] == i)
        {
            dialogBuy.SetActive(true);
            temp = i;
        }
    }

    void Bought(int i)
    {
        bought[i] = i;
        coinPlayer.coin -= data.price[i];
        PlayerPrefs.SetInt("Coin", coinPlayer.coin);
        coinPlayer.coinText.text = coinPlayer.coin.ToString();
        button[i].GetComponent<Image>().sprite = data.spriteBought[i];
        button[i].GetComponent<Button>().enabled = false;
        PlayerPrefs.SetInt("ChangeImage" + (i + 1), (i + 1));
        if (i > 0 && i < 10)
        {
            PlayerPrefs.SetInt("UpdateSkillPlayer", i);
        }
        else if (i >= 10 && i < 20)
        {
            PlayerPrefs.SetInt("UpdateSkillFollower", (i - 10));
        }
    }

    public void BuyButton()
    {
        dialogBuy.SetActive(false);
        Bought(temp);
    }

    public void CancelButton()
    {
        dialogBuy.SetActive(false);
    }

    public void OKButton()
    {
        dialogPoor.SetActive(false);
    }
}
