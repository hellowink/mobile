using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeAndroidButtons : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Sería la flechita para atrás en Android. Debería permitir volver atrás
        }
        if (Input.GetKeyDown(KeyCode.Menu))
        {
            //Salir de la aplicación en Android. Boron cicular, casi ni se usa.
        }
    }
}
