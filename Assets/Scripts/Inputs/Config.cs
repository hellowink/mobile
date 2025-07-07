using System.Collections;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using System.Threading.Tasks;

public class Config : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    public static float itemDropRate;
    public static int coinPerItem;
    public static int staminaRechargeTimeMinutes;
    public static int maxStamina;
    public static bool bonusEventActive;

    public static bool IsReady { get; private set; } = false;

    async void Start()
    {
        await UnityServices.InitializeAsync();

        try
        {
            await RemoteConfigService.Instance.FetchConfigsAsync(new userAttributes(), new appAttributes());
            ApplyRemoteSettings();
            IsReady = true;
            OnConfigReady?.Invoke();
            Debug.Log("Remote Config aplicado correctamente.");
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Error al obtener Remote Config: " + e.Message);
        }
    }

    public delegate void ConfigReadyDelegate();
    public static event ConfigReadyDelegate OnConfigReady;

    void ApplyRemoteSettings()
    {
        var appConfig = RemoteConfigService.Instance.appConfig;

        itemDropRate = appConfig.GetFloat("itemsDrop", 0.5f);
        coinPerItem = appConfig.GetInt("Coins", 10);
        staminaRechargeTimeMinutes = appConfig.GetInt("Stamina", 5);
        maxStamina = appConfig.GetInt("MaxStamina", 10);
        bonusEventActive = appConfig.GetBool("Bonus", false);
    }
}
