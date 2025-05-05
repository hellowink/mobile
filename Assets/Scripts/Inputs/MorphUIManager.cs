using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MorphUIManager : MonoBehaviour
{
    public GameObject suitSelectionPanel;
    public Button morphButton;
    public Button suit1Button;
    public Button suit2Button;
    public GameObject player;
    public Sprite windSuitSprite;
    public Sprite iceSuitSprite;
    

    private SpriteRenderer playerSpriteRenderer;
    private PlayerJump playerJump;

    void Start()
    {
        playerSpriteRenderer = player.GetComponentInChildren<SpriteRenderer>();
        playerJump = player.GetComponent<PlayerJump>(); // Accede al script de salto

        suitSelectionPanel.SetActive(false);

        morphButton.onClick.AddListener(OnMorphButtonClick);
        suit1Button.onClick.AddListener(() => SelectSuit("SuitWind"));
        suit2Button.onClick.AddListener(() => SelectSuit("SuitIce"));
    }

    void OnMorphButtonClick()
    {
        suitSelectionPanel.SetActive(true);
    }

    void SelectSuit(string suitName)
    {
        suitSelectionPanel.SetActive(false);
        Debug.Log($"Has seleccionado el traje: {suitName}");

        // Eliminar los scripts existentes
        Destroy(player.GetComponent<DefaultSuit>());
        Destroy(player.GetComponent<SuitWind>());
        Destroy(player.GetComponent<SuitIce>());

        switch (suitName)
        {
            case "SuitWind":
                player.AddComponent<SuitWind>();
                playerSpriteRenderer.sprite = windSuitSprite;
               

                playerJump.jumpForce = 15f; // 💨 Aumenta fuerza de salto
                break;

            case "SuitIce":
                player.AddComponent<SuitIce>();
                playerSpriteRenderer.sprite = iceSuitSprite;
               

                playerJump.jumpForce = 8f; // ❄️ Valor por defecto
                break;
        }
    }
}