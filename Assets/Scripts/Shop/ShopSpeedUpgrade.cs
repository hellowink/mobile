using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopSpeedUpgrade : MonoBehaviour
{
    public float increaseAmount = 0.5f;
    public float maxSpeed = 5f;

    public void UpgradeSpeed()
    {
        float currentSpeed = PlayerPrefs.GetFloat("SlideSpeed", 1.3f);
        currentSpeed += increaseAmount;
        currentSpeed = Mathf.Clamp(currentSpeed, 1.3f, maxSpeed);

        PlayerPrefs.SetFloat("SlideSpeed", currentSpeed);
        PlayerPrefs.Save();

        Debug.Log("Nueva velocidad guardada: " + currentSpeed);
    }
}
