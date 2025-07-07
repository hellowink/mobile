using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IngredientCatcher catcher = other.GetComponent<IngredientCatcher>();
            if (catcher != null)
            {
                catcher.RemoveFromTower(5);
            }

            // Volver el hazard al pool (si usás pooling)
            gameObject.SetActive(false);
        }
    }
}