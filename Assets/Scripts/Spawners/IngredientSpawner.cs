using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
   [Header("Probabilidades de ingredientes")]
    public List<IngredientProbability> ingredientProbabilities;

    [Header("Spawning")]
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
        string chosenType = GetRandomIngredient();
        GameObject ingredient = IngredientPool.Instance.GetFromPool(chosenType);
        if (ingredient == null)
        {
            Debug.LogWarning("No se encontró el prefab del tipo: " + chosenType);
            return;
        }

        float randomX = Random.Range(minX, maxX);
        ingredient.transform.position = new Vector2(randomX, spawnY);
        ingredient.SetActive(true);

        var rb = ingredient.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * Random.Range(2f, 6f);
        }
    }

    string GetRandomIngredient()
    {
        float totalWeight = 0f;
        foreach (var ip in ingredientProbabilities)
        {
            totalWeight += ip.weight;
        }

        float randomValue = Random.Range(0, totalWeight);
        float current = 0f;

        foreach (var ip in ingredientProbabilities)
        {
            current += ip.weight;
            if (randomValue <= current)
                return ip.name;
        }

        return ingredientProbabilities[0].name; // Fallback
    }
}