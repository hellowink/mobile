using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuControl : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("game"); 
    }
    public void GoShop()
    {
        SceneManager.LoadScene("Shop"); 
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit pressed - only works in build"); 
    }
}
