using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCatcher : MonoBehaviour
{
    public Transform towerAnchor; // Objeto vac�o encima de la bandeja
    public float verticalSpacing = 0.5f; // Espacio entre ingredientes

    private Transform lastIngredientTransform;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ingredient"))
        {
            // Detener f�sica
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
            }

            // Calcular la posici�n donde se apilar� el nuevo ingrediente
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

            // Guardar como �ltimo ingrediente apilado
            lastIngredientTransform = other.transform;

            // Opcional: Desactivar colisi�n
            Collider2D col = other.GetComponent<Collider2D>();
            if (col != null) col.enabled = false;
        }
    }
}
