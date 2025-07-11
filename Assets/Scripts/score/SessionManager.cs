using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance;
    public string currentUser;
    public int savedPoints;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // pewrsiste entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartSession(string username)
    {
        currentUser = username;
        savedPoints = PlayerPrefs.GetInt(GetPointsKey(), 0);
        GameManager.Instance.points = savedPoints;
        GameManager.Instance.UpdatePointsUI();
        Debug.Log("Sesión iniciada: " + currentUser + " con " + savedPoints + " puntos");
    }

    public void SavePoints(int points)
    {
        PlayerPrefs.SetInt(GetPointsKey(), points);
        PlayerPrefs.Save();
    }

    private string GetPointsKey()
    {
        return "UserPoints_" + currentUser;
    }
}