using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stamina : MonoBehaviour
{
    public int currentStamina;
    private int maxStamina;
    private int rechargeTimeMinutes;

    private Coroutine rechargeCoroutine;

    void Start()
    {
        maxStamina = Config.maxStamina;
        rechargeTimeMinutes = Config.staminaRechargeTimeMinutes;
        currentStamina = maxStamina;
        UseStamina(1);
        rechargeCoroutine = StartCoroutine(RechargeStamina());
    }

    public bool UseStamina(int amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            Debug.Log($"Stamina usada: {amount}. Stamina actual: {currentStamina}");
            return true;
        }
        else
        {
            Debug.Log("No hay suficiente stamina");
            return false;
        }
    }
    IEnumerator RechargeStamina()
    {
        while (true)
        {
            yield return new WaitForSeconds(rechargeTimeMinutes * 60f); 

            if (currentStamina < maxStamina)
            {
                currentStamina++;
                Debug.Log($"Stamina recargada +1. Actual: {currentStamina}");
            }
        }
    }
    void OnEnable()
    {
        Config.OnConfigReady += InitializeStamina;
    }

    void OnDisable()
    {
        Config.OnConfigReady -= InitializeStamina;
    }

    void InitializeStamina()
    {
        maxStamina = Config.maxStamina;
        rechargeTimeMinutes = Config.staminaRechargeTimeMinutes;
        currentStamina = maxStamina;
        UseStamina(1);
        rechargeCoroutine = StartCoroutine(RechargeStamina());
    }
}
