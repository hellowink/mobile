using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaUI : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI staminaText;
    public Image staminaFill;

    [Header("Referencia al sistema de stamina")]
    public stamina staminaSystem;

    private void Update()
    {
        if (staminaSystem == null) return;

        int current = staminaSystem.currentStamina;
        int max = Config.maxStamina;

        staminaText.text = $"{current}/{max}";
        staminaFill.fillAmount = (float)current / max;
    }
}

