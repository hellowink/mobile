using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RemoteConfig;
using Unity.Services.RemoteConfig;

public class coins : MonoBehaviour
{
    ////// Start is called before the first frame update
    ////void Start()
    ////{
    ////    DuplicarMonedas();

    ////}
    ////public void DuplicarMonedas()
    ////{
    ////    AdsManager.Instance.OnRewardedAdCompleted = () =>
    ////    {
    ////        Debug.Log("¡Recompensa activada!");
    ////        // acá sumás monedas, activás efectos, etc.
    ////    };

    ////    AdsManager.Instance.ShowRewardedAd();
    ////}

    //public static void AddCoins(int amount)
    //{
    //    int currentCoins = GetCoins();
    //    int newTotal = currentCoins + amount;

    //    PlayerPrefs.SetInt("RemoteCoins", newTotal); // opcional, para caché local
    //    PlayerPrefs.Save();

    //    // Si querés ver el cambio local
    //    Debug.Log($"Monedas remotas actualizadas: {currentCoins} ? {newTotal}");
    //}

    //public static int GetCoins()
    //{
    //    // Obtener de Remote Config
    //    if (RemoteConfigService.Instance.appConfig.HasKey("Coins"))
    //        return RemoteConfigService.Instance.appConfig.GetInt("Coins");

    //    return PlayerPrefs.GetInt("RemoteCoins", 0); // Fallback local si no está disponible aún
    //}
}

