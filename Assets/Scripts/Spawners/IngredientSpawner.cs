using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [Header("Configuración de Spawn")]
    public GameObject[] ingredientPrefabs; // Array para distintos ingredientes
    public float spawnInterval = 1.5f;

    [Header("Altura de Spawn")]
    public float spawnYOffset = 1f;

    [Header("Velocidad de Caída")]
    public float minFallSpeed = 1f;
    public float maxFallSpeed = 5f;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        InvokeRepeating(nameof(SpawnIngredient), 1f, spawnInterval);
    }

    void SpawnIngredient()
    {
        if (ingredientPrefabs.Length == 0) return;

        // Obtener ancho visible en unidades del mundo
        float screenHalfWidth = cam.orthographicSize * cam.aspect;

        // Posición aleatoria en X
        float randomX = Random.Range(-screenHalfWidth, screenHalfWidth);

        // Posición Y justo fuera de la pantalla
        float spawnY = cam.transform.position.y + cam.orthographicSize + spawnYOffset;

        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

        // Seleccionar un prefab aleatorio
        GameObject prefab = ingredientPrefabs[Random.Range(0, ingredientPrefabs.Length)];

        // Instanciar
        GameObject ingredient = Instantiate(prefab, spawnPosition, Quaternion.identity);

        // Asignar velocidad aleatoria
        Rigidbody2D rb = ingredient.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(0f, -Random.Range(minFallSpeed, maxFallSpeed));
        }
    }
}
