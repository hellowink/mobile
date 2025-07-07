using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public bool isBread; // Activá esta casilla en los ingredientes que sean pan (superior o inferior)

    public float GetHeight()
    {
        Collider2D col = GetComponent<Collider2D>();
        return col != null ? col.bounds.size.y : 0.5f;
    }
}
