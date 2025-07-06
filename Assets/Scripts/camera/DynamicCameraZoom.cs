using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCameraZoom : MonoBehaviour
{
    public Transform towerAnchor; // Asign� el mismo TowerAnchor
    public float baseZoom = 5f; // Zoom base inicial
    public float zoomPerIngredient = 0.2f; // Cu�nto alejar por cada ingrediente
    public float maxZoom = 10f; // Zoom m�ximo permitido

    private int lastIngredientCount = 0;

    void Update()
    {
        int ingredientCount = towerAnchor.childCount;

        if (ingredientCount != lastIngredientCount)
        {
            float newZoom = baseZoom + ingredientCount * zoomPerIngredient;
            newZoom = Mathf.Min(newZoom, maxZoom);

            // Transici�n suave
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, newZoom, Time.deltaTime * 5f);

            lastIngredientCount = ingredientCount;
        }
    }
}

