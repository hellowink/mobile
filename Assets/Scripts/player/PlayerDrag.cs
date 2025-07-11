using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrag : MonoBehaviour
{
     private Vector3 offset;
    private Camera cam;
    private bool isDragging = false;
    private float halfColliderWidth;

    [Header("Configuración de Deslizamiento")]
    [Range(1f, 20f)]
    public float slideSpeed = 1.3f; // Velocidad de seguimiento (ajustable)

    private Vector3 targetPosition;

    void Start()
    {
        cam = Camera.main;

        // Cargar slideSpeed desde PlayerPrefs
        slideSpeed = PlayerPrefs.GetFloat("SlideSpeed", 1.3f); // 1.3f es el valor por defecto

        // Calcular ancho del collider
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if (col != null)
        {
            halfColliderWidth = col.bounds.extents.x;
        }

        targetPosition = transform.position;
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
                        targetPosition = touchPosWorld + offset;

                        // Limitar movimiento según el ancho del collider
                        float screenHalfWidth = cam.orthographicSize * cam.aspect;
                        float minX = -screenHalfWidth + halfColliderWidth;
                        float maxX = screenHalfWidth - halfColliderWidth;

                        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
                        targetPosition.y = transform.position.y;
                        targetPosition.z = transform.position.z;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isDragging = false;
                    break;
            }
        }

        // Movimiento suave hacia el objetivo
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * slideSpeed);
    }
}