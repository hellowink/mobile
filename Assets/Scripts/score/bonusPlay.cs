using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusPlay : MonoBehaviour
{
    private bool isBonusActive = false;

    void OnMouseDown()
    {
        // Evita reactivar el bonus si ya está activo
        if (!isBonusActive)
        {
            StartCoroutine(ActivateBonusTemporarily());
        }
    }

    IEnumerator ActivateBonusTemporarily()
    {
        isBonusActive = true;
        Config.bonusEventActive = true;
        Debug.Log("BONUS ACTIVADO");

        yield return new WaitForSeconds(10f);

        Config.bonusEventActive = false;
        isBonusActive = false;
        Debug.Log("BONUS FINALIZADO");
    }
}
