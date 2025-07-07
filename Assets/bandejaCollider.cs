using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Tray : MonoBehaviour
{
    private bool canCollect = false;

    private void Start()
    {
        // Pequeño delay para evitar sumar puntos al inicio
        Invoke("EnableCollection", 0.2f);
    }

    void EnableCollection()
    {
        canCollect = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canCollect) return;

        if (other.CompareTag("Ingredient"))
        {
            GameManager.Instance.AddPoints(10);
            
        }
    }
}