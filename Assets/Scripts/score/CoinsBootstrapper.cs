using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinsBootstrapper : MonoBehaviour
{
    public GameObject coinsManagerPrefab;

    void Awake()
    {
        Debug.Log("Bootstrapper activo en escena: " + SceneManager.GetActiveScene().name);

        if (coinsManager.Instance == null)
        {
            Debug.Log("CoinsManager.Instance == null → se instancia");
            GameObject cm = Instantiate(coinsManagerPrefab);
            DontDestroyOnLoad(cm);
        }
        else
        {
            Debug.Log("CoinsManager ya existe → NO se instancia");
        }
    }
}
