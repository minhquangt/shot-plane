using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinPlayer : MonoBehaviour
{
    public Text coinText;
    public int coin;

    private void Awake()
    {
        coin = PlayerPrefs.GetInt("Coin");
    }
    void Start()
    { 
        coinText.text = coin.ToString();
    }
}
