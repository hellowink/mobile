using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidAdUnitId = "Rewarded_Android";
    //[SerializeField] private string iosAdUnitId = "Rewarded_iOS";
    private string adUnitId;

    public static AdsManager Instance;

    public System.Action OnRewardedAdCompleted;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

#if UNITY_ANDROID
        adUnitId = androidAdUnitId;
#elif UNITY_IOS
        adUnitId = iosAdUnitId;
#endif

        Advertisement.Initialize("5893628", true);
        Advertisement.Load(adUnitId, this);
    }

    public void ShowRewardedAd()
    {
        Advertisement.Show(adUnitId, this); // Ya no se chequea IsReady, se asume que lo cargaste antes
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Anuncio cargado: " + adUnitId);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("Anuncio completado");
            OnRewardedAdCompleted?.Invoke();
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.LogWarning("Error al cargar anuncio: " + message);
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.LogWarning("Error al mostrar anuncio: " + message);
    }

    public void OnUnityAdsShowStart(string adUnitId) { }

    public void OnUnityAdsShowClick(string adUnitId) { }
}

