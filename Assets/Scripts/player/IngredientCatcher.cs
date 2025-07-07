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

    // Se calcula automáticamente según la cámara
    private float maxYLimit;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Obtenemos el borde superior de la cámara principal en coordenadas del mundo
        maxYLimit = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0)).y;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Entró {other.name}");

        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient == null) return;

        Debug.Log($"Es ingrediente válido: {ingredient.name}, pan: {ingredient.isBread}");

        /*if (!ingredient.isBread)
        {
            GameState.AddPoints(Config.coinPerItem);
        }
        */

        // Frenar caída
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        // Calcular posición de apilado
Vector3 stackPosition;

float newHeight = GetHeight(other.transform);

if (lastIngredientTransform == null)
{
    stackPosition = new Vector3(
        towerAnchor.position.x,
        towerAnchor.position.y + newHeight / 2f,
        0f
    );
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

        // Posicionar y parentar
        other.transform.position = stackPosition;
        other.transform.SetParent(towerAnchor);
        lastIngredientTransform = other.transform;

        // Desactivar collider
        Collider2D col = other.GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // Si es pan, completar sándwich
        if (ingredient.isBread)
        {
            Debug.Log("Llamando a CompleteSandwich");
            CompleteSandwich();
        }

        // Verificar si se pasó de altura
        CheckLoseCondition();
    }

    void CheckLoseCondition()
    {
        if (lastIngredientTransform != null && lastIngredientTransform.position.y > maxYLimit)
        {
            Debug.Log("¡Perdiste! El sándwich es demasiado alto.");
            GameManager.Instance.LoseAllLives();
        }
    }

    void CompleteSandwich()
    {
        Debug.Log("¡Sándwich completo!");

        foreach (Transform child in towerAnchor)
        {
            IngredientPool.Instance.ReturnToPool(child.gameObject);
        }

        towerAnchor.DetachChildren();
        lastIngredientTransform = null;

        /*int coinsEarned = Config.coinPerItem;

        if (Config.bonusEventActive)
        {
            coinsEarned *= 2;
        }
        */

        //GameState.CompleteSandwich();

        // Sumar monedas del Remote Config
        //GameState.Coins += Config.coinPerItem;
        //Debug.Log("Monedas actuales: " + GameState.Coins);
    }

    float GetHeight(Transform t)
    {
        Collider2D col = t.GetComponent<Collider2D>();
        return col != null ? col.bounds.size.y : 0.5f;
    }
}