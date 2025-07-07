using System.Collections.Generic;
using UnityEngine;

public class IngredientCatcher : MonoBehaviour
{
    public Transform stackPoint;
    public float ingredientHeight = 0.5f;

    private List<GameObject> tower = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            CatchIngredient(other.gameObject);
        }
        else if (other.CompareTag("Hazard"))
        {
            RemoveFromTower(5);
        }
    }

    void CatchIngredient(GameObject ingredient)
    {
        ingredient.transform.SetParent(stackPoint);

        float yOffset = tower.Count * ingredientHeight;
        ingredient.transform.localPosition = new Vector3(0, yOffset, 0);
        ingredient.transform.localRotation = Quaternion.identity;

        tower.Add(ingredient);
    }

    public void RemoveFromTower(int count)
    {
        int removeCount = Mathf.Min(count, tower.Count);

        for (int i = 0; i < removeCount; i++)
        {
            int index = tower.Count - 1;
            GameObject ing = tower[index];
            tower.RemoveAt(index);
            IngredientPool.Instance.ReturnToPool(ing);
        }

        RepositionTower();
    }

    void RepositionTower()
    {
        for (int i = 0; i < tower.Count; i++)
        {
            GameObject ing = tower[i];
            if (ing != null)
            {
                ing.transform.localPosition = new Vector3(0, i * ingredientHeight, 0);
            }
        }
    }
}