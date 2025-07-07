using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusItem : MonoBehaviour
{
        private bool alreadyCollected = false;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (alreadyCollected) return;

        // Verifica que tocó el catcher
        if (other.CompareTag("IngredientCatcher"))
        {
            bonusPoints.Instance.ActivateBonus();
            gameObject.SetActive(false);
        }
    }

        IEnumerator ActivateBonusTemporarily()
        {
        Debug.Log("BONUS ACTIVADO");
        Config.bonusEventActive = true;

        yield return new WaitForSeconds(10f);

        Config.bonusEventActive = false;
        Debug.Log("BONUS FINALIZADO");

        gameObject.SetActive(false); // Desactivar al final, no antes
    }
}
