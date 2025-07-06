using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCatcher : MonoBehaviour
{
    public Transform towerAnchor; // Objeto vacío encima de la bandeja
    public float verticalSpacing = 0.5f; // Espacio entre ingredientes

    private Transform lastIngredientTransform;

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
                // Primer ingrediente se apila sobre el TowerAnchor
                stackPosition = towerAnchor.position;
            }
            else
            {
                // Siguiente ingrediente se apila sobre el anterior
                float height = lastIngredientTransform.GetComponent<SpriteRenderer>().bounds.size.y;
                stackPosition = lastIngredientTransform.position + new Vector3(0f, height, 0f);
            }

            // Posicionar y hacer hijo del towerAnchor
            other.transform.position = stackPosition;
            other.transform.SetParent(towerAnchor);

            // Guardar como último ingrediente apilado
            lastIngredientTransform = other.transform;

            // Opcional: Desactivar colisión
            Collider2D col = other.GetComponent<Collider2D>();
            if (col != null) col.enabled = false;
        }
    }
}
