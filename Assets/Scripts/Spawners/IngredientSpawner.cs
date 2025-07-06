using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
   public string[] ingredientNames = { "Tomato" };
    public float spawnInterval = 1.5f;
    public float minX = -3f;
    public float maxX = 3f;
    public float spawnY = 10f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnIngredient), 1f, spawnInterval);
    }

    void SpawnIngredient()
    {
        string randomType = ingredientNames[Random.Range(0, ingredientNames.Length)];

        GameObject ingredient = IngredientPool.Instance.GetFromPool(randomType);
        if (ingredient == null) return;

        float randomX = Random.Range(minX, maxX);
        ingredient.transform.position = new Vector2(randomX, spawnY);
        ingredient.SetActive(true);

        var rb = ingredient.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * Random.Range(2f, 6f);
        }
    }
}
