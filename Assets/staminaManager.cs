using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class staminaManager : MonoBehaviour
{
    public static staminaManager Instance;

    [Header("Stamina UI")]
    public Image staminaFill;
    public TextMeshProUGUI staminaText;
    public GameObject noEnergyPanel;

    private int maxStamina;
    private int currentStamina;
    private float regenTimeMinutes;

    private float regenTimer;
    private DateTime lastPlayedTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Config.OnConfigReady += InitializeStamina;
    }

    private void InitializeStamina()
    {
        maxStamina = Config.maxStamina;
        regenTimeMinutes = Config.staminaRechargeTimeMinutes;

        currentStamina = PlayerPrefs.GetInt("CurrentStamina", maxStamina);

        string lastTimeStr = PlayerPrefs.GetString("LastPlayedTime", DateTime.Now.ToString());
        lastPlayedTime = DateTime.Parse(lastTimeStr);

        TimeSpan timePassed = DateTime.Now - lastPlayedTime;
        int regenUnits = Mathf.FloorToInt((float)timePassed.TotalMinutes / regenTimeMinutes);
        if (regenUnits > 0)
        {
            currentStamina = Mathf.Min(currentStamina + regenUnits, maxStamina);
        }

        PlayerPrefs.SetInt("CurrentStamina", currentStamina);
        PlayerPrefs.SetString("LastPlayedTime", DateTime.Now.ToString());
        PlayerPrefs.Save();

        UpdateStaminaUI();

        if (noEnergyPanel != null)
            noEnergyPanel.SetActive(false);
    }

    private void Update()
    {
        if (!Config.IsReady) return;

        if (currentStamina < maxStamina)
        {
            regenTimer += Time.deltaTime;
            if (regenTimer >= regenTimeMinutes * 60f)
            {
                currentStamina++;
                regenTimer = 0f;
                SaveStamina();
                UpdateStaminaUI();
            }
        }
    }

    public bool TryUseStamina()
    {
        if (currentStamina <= 0)
        {
            noEnergyPanel?.SetActive(true);
            return false;
        }

        currentStamina--;
        SaveStamina();
        UpdateStaminaUI();

        if (currentStamina <= 0)
        {
            noEnergyPanel?.SetActive(true);
        }

        return true;
    }

    void SaveStamina()
    {
        PlayerPrefs.SetInt("CurrentStamina", currentStamina);
        PlayerPrefs.SetString("LastPlayedTime", DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    void UpdateStaminaUI()
    {
        if (staminaFill == null || staminaText == null) return;

        float fillAmount = (float)currentStamina / maxStamina;
        staminaFill.fillAmount = fillAmount;
        staminaText.text = $"Energía: {currentStamina}/{maxStamina}";
    }
}
