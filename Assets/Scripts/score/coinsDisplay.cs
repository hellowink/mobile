using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class coinDisplay : MonoBehaviour
{
    public TMP_Text coinsText;

    void Start()
    {
        int coins = PlayerPrefs.GetInt("pointsSave", 0);
        coinsText.text = "Coins: " + coins.ToString();
    }
}
