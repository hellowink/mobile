using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.ActivateBonus();
            Destroy(gameObject);
        }
    }
}
