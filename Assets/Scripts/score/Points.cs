using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public int pointsActually = 0;
    public int pointsSave = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            pointsActually = 20;
            pointsSave += pointsActually;

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Portal")) 
        {
            GuardarPuntosAlFinal();
        }
    }

    public void GuardarPuntosAlFinal()
    {
        int puntosAnteriores = PlayerPrefs.GetInt("pointsSave", 0);
        puntosAnteriores += pointsSave;
        PlayerPrefs.SetInt("pointsSave", puntosAnteriores);
        PlayerPrefs.Save();

        Debug.Log("Puntos guardados: " + puntosAnteriores);
    }
}
