using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class staminaManager : MonoBehaviour
{
    public static staminaManager Instance;

    [Header("Stamina")]
    public int maxStamina = 3;
    public int currentStamina;
    public Image staminaFill;
    public TextMeshProUGUI staminaText;
    public GameObject noEnergyPanel;

    public bool isOutOfStamina = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentStamina = maxStamina;
        UpdateStaminaUI();
        noEnergyPanel.SetActive(false);
    }

    public bool TryUseStamina()
    {
        if (currentStamina <= 0)
        {
            isOutOfStamina = true;
            noEnergyPanel.SetActive(true);
            return false;
        }

        currentStamina--;
        UpdateStaminaUI();

        if (currentStamina <= 0)
        {
            isOutOfStamina = true;
            noEnergyPanel.SetActive(true);
        }

        return true;
    }

    void UpdateStaminaUI()
    {
        if (staminaFill == null || staminaText == null) return;

        float fillAmount = (float)currentStamina / maxStamina;
        staminaFill.fillAmount = fillAmount;
        staminaText.text = "Energía: " + currentStamina + "/" + maxStamina;
    }
}