using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsCoins : MonoBehaviour
{
    public void OnClick_DuplicateCoins()

    {
        Debug.Log("Botón de duplicar monedas presionado");
        AdsManager.Instance.rewardedAds.OnRewardedAdCompleted = () =>
        {
            int amount = Config.coinPerItem * 2;
            Config.AddCoins(amount);
            Debug.Log($" Monedas duplicadas: +{amount}");
        };

        AdsManager.Instance.ShowRewardedAd();
    }
}
