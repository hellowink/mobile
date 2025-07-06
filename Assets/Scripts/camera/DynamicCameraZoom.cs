using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCameraZoom : MonoBehaviour
{
   public Transform towerAnchor;   // Donde se apilan los ingredientes
    public float baseZoom = 5f;     // Zoom inicial
    public float zoomPerIngredient = 0.2f; // Cu�nto aleja por cada ingrediente
    public float maxZoom = 12f;

    public float verticalOffset = 3f;  // Cu�nto m�s arriba de la torre mostrar
    public float centerX = 0f;         // X fija de la c�mara
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        int ingredientCount = towerAnchor.childCount;

        // Calcular nuevo zoom
        float newZoom = baseZoom + ingredientCount * zoomPerIngredient;
        newZoom = Mathf.Min(newZoom, maxZoom);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, newZoom, Time.deltaTime * smoothSpeed);

        // Calcular nueva posici�n Y
        float newY = towerAnchor.position.y + verticalOffset + ingredientCount * zoomPerIngredient;

        // Aplicar nueva posici�n (solo cambia en Y, X es fija)
        Vector3 targetPos = new Vector3(centerX, newY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothSpeed);
    }
}