using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HazardType
{
    Boot,
    Can,
    Bomb
}

public class Hazard : MonoBehaviour
{
    public HazardType hazardType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TowerAnchor"))
        {
            switch (hazardType)
            {
                case HazardType.Boot:
                case HazardType.Can:
                    // Quita 5 ingredientes de la torre
                    IngredientCatcher.Instance.RemoveFromTower(5);
                    break;

                case HazardType.Bomb:
                    // Hace perder todas las vidas instantáneamente
                    GameManager.Instance.LoseAllLives();
                    break;
            }

            // Destruye el objeto una vez aplicada la penalización
            Destroy(gameObject);
        }
    }
}