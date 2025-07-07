using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCatcher : MonoBehaviour
{
    public static IngredientCatcher Instance;

    
    public Transform towerAnchor; // Objeto vacío encima de la bandeja
    public float verticalSpacing = 0.1f; // Espacio entre ingredientes (positivo = más juntos)

    private Transform lastIngredientTransform;
    private int totalCoins;

    void Start()
    {
        // Cargar monedas guardadas o iniciar en 0
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        Debug.Log("Monedas iniciales: " + totalCoins);
    }

    void Awake()
    {
        // Singleton para acceder desde otros scripts
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ingredient"))
        {
            // Detener física
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
            }

            // Calcular la posición donde se apilará el nuevo ingrediente
            Vector3 stackPosition;

            if (lastIngredientTransform == null)
            {
                stackPosition = towerAnchor.position;
            }
            else
            {
                float height = lastIngredientTransform.GetComponent<SpriteRenderer>().bounds.size.y;
                stackPosition = lastIngredientTransform.position + new Vector3(0f, height - verticalSpacing, 0f);
            }

            // Posicionar y hacer hijo del towerAnchor
            other.transform.position = stackPosition;
            other.transform.SetParent(towerAnchor);

            // Guardar como último ingrediente apilado
            lastIngredientTransform = other.transform;

            // Desactivar colisión
            Collider2D col = other.GetComponent<Collider2D>();
            if (col != null) col.enabled = false;

            AddCoins(Config.coinPerItem > 0 ? Config.coinPerItem : 100);
        }
    }

    void AddCoins(int amount)
    {
        totalCoins += amount;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();
        Debug.Log("Monedas ganadas: +" + amount + " | Total: " + totalCoins);
    }

    // 🔻 Remueve ingredientes desde arriba hacia abajo
    public void RemoveFromTower(int amount)
    {
        int toRemove = Mathf.Min(amount, towerAnchor.childCount);

        List<Transform> toDestroy = new List<Transform>();

        // Guardamos primero los ingredientes a destruir
        for (int i = 0; i < toRemove; i++)
        {
            int lastIndex = towerAnchor.childCount - 1 - i;
            if (lastIndex >= 0)
            {
                Transform ingredient = towerAnchor.GetChild(lastIndex);
                toDestroy.Add(ingredient);
            }
        }
    
        // Luego destruimos fuera del loop principal
        foreach (Transform ingredient in toDestroy)
        {
            Destroy(ingredient.gameObject);
        }

        // Actualizamos la referencia correctamente
        if (towerAnchor.childCount > 0)
        {
            lastIngredientTransform = towerAnchor.GetChild(towerAnchor.childCount - 1);
        }
        else
        {
            lastIngredientTransform = null;
        }

    }
}