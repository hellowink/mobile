using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public Tray Tray;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!Tray.canCollects) return;

        if (other.CompareTag("Hazard"))
        {
            GameManager.Instance.AddPoints(-50);

        }
    }
    
}
