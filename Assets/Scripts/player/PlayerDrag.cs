using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrag : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    private bool isDragging = false;
    private float halfColliderWidth;

    void Start()
    {
        cam = Camera.main;

        // Obtener mitad del ancho del collider en unidades del mundo
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if (col != null)
        {
            halfColliderWidth = col.bounds.extents.x;
        }
        else
        {
            Debug.LogWarning("BoxCollider2D no encontrado en el Player.");
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosWorld = cam.ScreenToWorldPoint(touch.position);
            touchPosWorld.z = 0f;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    RaycastHit2D hit = Physics2D.Raycast(touchPosWorld, Vector2.zero);
                    if (hit.collider != null && hit.collider.gameObject == gameObject)
                    {
                        isDragging = true;
                        offset = transform.position - touchPosWorld;
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        Vector3 newPos = touchPosWorld + offset;

                        // Limitar usando el tamaño del collider
                        float screenHalfWidth = cam.orthographicSize * cam.aspect;
                        float minX = -screenHalfWidth + halfColliderWidth;
                        float maxX = screenHalfWidth - halfColliderWidth;

                        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);

                        // Solo mueve en X
                        transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isDragging = false;
                    break;
            }
        }
    }
}