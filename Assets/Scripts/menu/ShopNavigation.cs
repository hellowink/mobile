using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopNavigation : MonoBehaviour
{
    public void GoBackToMenu()
    {
        SceneManager.LoadScene("Menu"); // Asegurate de que el nombre coincida exactamente
    }
}
