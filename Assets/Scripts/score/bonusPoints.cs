using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusPoints : MonoBehaviour
{
    public static bonusPoints Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ActivateBonus()
    {
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
