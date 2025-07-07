using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCatcher : MonoBehaviour
{
    public static IngredientCatcher Instance;

    public Transform towerAnchor;
    public float verticalSpacing = 0.1f;

    private Transform lastIngredientTransform;

    void Awake()
    {
        Instance = this;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ingredient"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
            }

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

            other.transform.position = stackPosition;
            other.transform.SetParent(towerAnchor);

            lastIngredientTransform = other.transform;

            Collider2D col = other.GetComponent<Collider2D>();
            if (col != null) col.enabled = false;
        }
    }
}