using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeAndroidButtons : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Ser�a la flechita para atr�s en Android. Deber�a permitir volver atr�s
        }
        if (Input.GetKeyDown(KeyCode.Menu))
        {
            //Salir de la aplicaci�n en Android. Boron cicular, casi ni se usa.
        }
    }
}
