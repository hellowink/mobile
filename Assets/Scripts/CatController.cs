using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CatController : MonoBehaviour
{
 public float moveSpeed = 5f;
private Vector2 targetPosition;

void Start()
{
    targetPosition = transform.position;
}

void Update()
{
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);
        Vector2 worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

        // Movimiento vertical simulado en pantalla horizontal
        if (worldTouch.y < 0)
            targetPosition = new Vector2(transform.position.x, -3.5f); // abajo
        else
            targetPosition = new Vector2(transform.position.x, 3.5f);  // arriba
    }

    // Movimiento suave hacia la posición
    transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
}
}
