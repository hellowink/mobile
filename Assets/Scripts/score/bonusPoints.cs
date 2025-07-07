using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusPoints : MonoBehaviour
{
    void Start()
    {
        // Aseguramos que empiece en false
        Config.bonusEventActive = false;

        // Activamos el bonus por 10 segundos
        StartCoroutine(ActivateBonusTemporarily());
    }

    IEnumerator ActivateBonusTemporarily()
    {
        Debug.Log("BONUS ACTIVADO");
        Config.bonusEventActive = true;

        yield return new WaitForSeconds(10f);

        Config.bonusEventActive = false;
        Debug.Log("BONUS FINALIZADO");
    }
}
