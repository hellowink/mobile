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
    public GameObject player; // El GameObject del jugador
    public Sprite windSuitSprite; // Sprite del traje viento
    public Sprite iceSuitSprite; // Sprite del traje hielo
    

    private SpriteRenderer playerSpriteRenderer;

    void Start()
    {
        // Obtener el SpriteRenderer del hijo del jugador (asumimos que es un hijo con SpriteRenderer)
        playerSpriteRenderer = player.GetComponentInChildren<SpriteRenderer>();

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

        // Eliminar los scripts existentes de forma segura
        if (player.GetComponent<DefaultSuit>() != null)
            Destroy(player.GetComponent<DefaultSuit>());

        if (player.GetComponent<SuitWind>() != null)
            Destroy(player.GetComponent<SuitWind>());

        if (player.GetComponent<SuitIce>() != null)
            Destroy(player.GetComponent<SuitIce>());

        // Cambiar el sprite dependiendo del traje
        switch (suitName)
        {
            case "SuitWind":
                player.AddComponent<SuitWind>();
                playerSpriteRenderer.sprite = windSuitSprite; // Cambiar el sprite
                break;
            case "SuitIce":
                player.AddComponent<SuitIce>();
                playerSpriteRenderer.sprite = iceSuitSprite; // Cambiar el sprite
                
                break;
        }
    }
}