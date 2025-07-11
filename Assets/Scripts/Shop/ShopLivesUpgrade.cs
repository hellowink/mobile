using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLivesUpgrade : MonoBehaviour
{
    public int increaseAmount = 2;
    public int maxUpgrades = 5;
    public int maxLivesCap = 15;

    public void UpgradeLives()
    {
        int currentMaxLives = PlayerPrefs.GetInt("MaxLives", 5);
        int upgradesUsed = PlayerPrefs.GetInt("LivesUpgrades", 0);

        if (upgradesUsed >= maxUpgrades)
        {
            Debug.Log("No se pueden hacer más mejoras de vidas.");
            return;
        }

        if (currentMaxLives + increaseAmount > maxLivesCap)
        {
            Debug.Log("Se alcanzó el máximo de vidas: " + maxLivesCap);
            return;
        }

        currentMaxLives += increaseAmount;
        upgradesUsed++;

        PlayerPrefs.SetInt("MaxLives", currentMaxLives);
        PlayerPrefs.SetInt("LivesUpgrades", upgradesUsed);
        PlayerPrefs.Save();

        Debug.Log("Nuevo máximo de vidas guardado: " + currentMaxLives);
    }
}
