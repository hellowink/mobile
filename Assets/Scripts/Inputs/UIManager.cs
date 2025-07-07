using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Elements")]
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI coinsText; // NUEVO

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdatePointsUI(int points)
    {
        if (pointsText != null)
            pointsText.text = "POINTS: " + points;
    }

    public void UpdateCoinsUI(int coins)
    {
        if (coinsText != null)
            coinsText.text = "COINS: " + coins;
    }
}
