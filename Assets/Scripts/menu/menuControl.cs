using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayGame()
    {
        SceneManager.LoadScene("levelOne"); // Asegúrate de que el nombre coincida exactamente con el de tu escena
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit pressed - only works in build"); // Esto no cerrará el juego en el editor
    }
}
