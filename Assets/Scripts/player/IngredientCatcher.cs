using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngredientCatcher : MonoBehaviour
{
    public static IngredientCatcher Instance;

    public Transform towerAnchor;
    public float verticalSpacing = 0.05f;

    private Transform lastIngredientTransform;
    private float maxYLimit;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        maxYLimit = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0)).y;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient == null) return;

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        Vector3 stackPosition;
        float newHeight = GetHeight(other.transform);

        if (lastIngredientTransform == null)
        {
            stackPosition = new Vector3(towerAnchor.position.x, towerAnchor.position.y + newHeight / 2f, 0f);
        }
        else
        {
            float topOfLast = lastIngredientTransform.GetComponent<Collider2D>().bounds.max.y;
            stackPosition = new Vector3(
                towerAnchor.position.x,
                topOfLast + newHeight / 2f - verticalSpacing,
                0f
            );
        }

        other.transform.position = stackPosition;
        other.transform.SetParent(towerAnchor);
        lastIngredientTransform = other.transform;

        Collider2D col = other.GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // Aquí sumás puntos cuando el ingrediente es atrapado
        if (GameManager.Instance != null)
        {
            int puntosPorIngrediente = 10; // o lo que quieras asignar
            GameManager.Instance.AddPoints(puntosPorIngrediente);
            Debug.Log($"Ingrediente atrapado, sumando {puntosPorIngrediente} puntos.");
        }

        if (ingredient.isBread)
        {
            CompleteSandwich();
        }

        CheckLoseCondition();

    }

    void CheckLoseCondition()
    {
        if (lastIngredientTransform != null && lastIngredientTransform.position.y > maxYLimit)
        {
            GameManager.Instance.LoseAllLives();
        }
    }

    public void CompleteSandwich()
    {
        Debug.Log("¡Sándwich completo!");

        foreach (Transform child in towerAnchor)
        {
            IngredientPool.Instance.ReturnToPool(child.gameObject);
        }

        towerAnchor.DetachChildren();
        lastIngredientTransform = null;
    }

    float GetHeight(Transform t)
    {
        Collider2D col = t.GetComponent<Collider2D>();
        return col != null ? col.bounds.size.y : 0.5f;
    }
}