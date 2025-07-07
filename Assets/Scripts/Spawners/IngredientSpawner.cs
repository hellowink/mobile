using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [Header("Probabilidades de ingredientes")]
    public List<IngredientProbability> ingredientProbabilities;

    [Header("Spawning")]
    public float spawnInterval = 1.5f;
    public float spawnY = 10f;

    private float minX;
    private float maxX;

    void Start()
    {
        // Calcular bordes de la pantalla en coordenadas del mundo
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0));

        minX = leftEdge.x;
        maxX = rightEdge.x;

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

        // 🔹 RESETEAR el ingrediente al sacarlo del pool
        IngredientPool.Instance.ResetIngredient(ingredient);  // 👈 AQUÍ LLAMÁS EL MÉTODO NUEVO

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