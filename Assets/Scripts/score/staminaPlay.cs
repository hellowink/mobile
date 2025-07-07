using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staminaPlay : MonoBehaviour
{
    //public stamina staminaController;

    //void Update()
    //{
    //    // Detectar clic con mouse (para PC)
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        DetectClickOrTouch(Input.mousePosition);
    //    }

    //    // Detectar toque en móvil (tactil)
    //    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    //    {
    //        DetectClickOrTouch(Input.GetTouch(0).position);
    //    }
    //}

    //void DetectClickOrTouch(Vector3 screenPos)
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(screenPos);
    //    RaycastHit hit;

    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        if (hit.transform == transform)
    //        {
    //            // El objeto fue tocado/clickeado
    //            if (staminaController != null)
    //            {
    //                staminaController.UseStamina(1);
    //            }
    //            else
    //            {
    //                Debug.LogWarning("StaminaController no asignado en ButtonStamina");
    //            }
    //        }
    //    }
    //}

    public stamina staminaController;

    void OnMouseDown()
    {
        if (staminaController != null)
        {
            staminaController.UseStamina(1);
        }
    }
}
