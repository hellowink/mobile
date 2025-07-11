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
    }

    // Ll�malo cuando termina la partida
    public void AgregarPuntosPartida(int puntosGanados)
    {
        int puntosTotales = ObtenerPuntosTotales();
        puntosTotales += puntosGanados;
        PlayerPrefs.SetInt("Coins", puntosTotales);
        PlayerPrefs.Save();
        Debug.Log("Puntos acumulados: " + puntosTotales);
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
