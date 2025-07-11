using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinsManager : MonoBehaviour
{
    public static coinsManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("CoinsManager inicializado");
        }
        else if (Instance != this)
        {
            Debug.LogWarning("CoinsManager duplicado, destruyendo: " + gameObject.name);
            Destroy(gameObject);
        }

        Debug.Log("CoinsManager Instance is " + (coinsManager.Instance == null ? "NULL" : "NOT NULL"));
    }

    public void AgregarPuntosPartida(int puntosGanados)
    {
        int puntosTotales = ObtenerPuntosTotales();
        puntosTotales += puntosGanados;
        PlayerPrefs.SetInt("Coins", puntosTotales);
        PlayerPrefs.Save();
        Debug.Log("[CoinsManager] Puntos acumulados guardados: " + puntosTotales);
    }

    public int ObtenerPuntosTotales()
    {
        return PlayerPrefs.GetInt("Coins", 0);
    }

    public void ReiniciarPuntos()
    {
        PlayerPrefs.DeleteKey("Coins");
        PlayerPrefs.Save();
    }
}
