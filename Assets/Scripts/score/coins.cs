using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coins : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DuplicarMonedas();
        
    }
    public void DuplicarMonedas()
    {
        AdsManager.Instance.OnRewardedAdCompleted = () =>
        {
            Debug.Log("�Recompensa activada!");
            // ac� sum�s monedas, activ�s efectos, etc.
        };

        AdsManager.Instance.ShowRewardedAd();
    }
}
