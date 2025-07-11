using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class coinsUI : MonoBehaviour
{
    public TextMeshProUGUI monedasText;

    void Start()
    {
        // Si no lo asignaste en el Inspector, lo busca automáticamente en el hijo
        if (monedasText == null)
            monedasText = GetComponentInChildren<TextMeshProUGUI>();

        ActualizarTexto();
    }

    void Update()
    {
        ActualizarTexto();
    }

    void ActualizarTexto()
    {
        if (coinsManager.Instance == null || monedasText == null)
            return;

        int monedas = coinsManager.Instance.ObtenerPuntosTotales();
        monedasText.text = "Monedas: " + monedas;
    }
}
