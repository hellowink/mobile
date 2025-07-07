using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staminaPlay : MonoBehaviour
{
    public stamina staminaController;

    void OnMouseDown()
    {
        if (staminaController != null)
        {
            staminaController.UseStamina(1);
        }
    }
}
