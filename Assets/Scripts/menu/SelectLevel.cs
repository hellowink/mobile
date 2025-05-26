using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Portal"))
        {
            SceneManager.LoadScene("LevelSelection");  // Cambia de escena
        }
       
        
    }

    public void PlayLevelOne()
    {
        SceneManager.LoadScene("levelOne"); // Aseg�rate de que el nombre coincida exactamente con el de tu escena
    }

    public void PlayLevelTwo()
    {
        SceneManager.LoadScene("levelTwo"); // Aseg�rate de que el nombre coincida exactamente con el de tu escena
    }

    public void PlayLevelThree()
    {
        SceneManager.LoadScene("levelThree"); // Aseg�rate de que el nombre coincida exactamente con el de tu escena
    }
}
