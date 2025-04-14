using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTouch : MonoBehaviour
{
    [SerializeField] List<GameObject> _prefabs = new List<GameObject>();
    [SerializeField] LayerMask _floorMask;

    void Update()
    {
        //Input.touchCount; //Cuenta la cantidad de touches en pantalla.
        //Input.touches[i]; //Lista de touches.
        //Input.touchSupported; // Si el dispositivo soporta touches.
        //Input.multiTouchEnabled = false; // Habilita o deshabilita multiples touches;

        if (Input.touchCount == 0) return;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch currentTouch = Input.touches[i];

            if (currentTouch.phase == TouchPhase.Began)
            {
                Ray touchRay = Camera.main.ScreenPointToRay(currentTouch.position);
                RaycastHit hit;
                if (Physics.Raycast(touchRay, out hit, 10000, _floorMask, QueryTriggerInteraction.Ignore))
                {
                    int random = Random.Range(0, _prefabs.Count);
                    GameObject spawned = Instantiate(_prefabs[random].gameObject);
                    spawned.transform.position = new Vector3(hit.point.x, hit.point.y + 2, hit.point.z);
                }
            }
        }
    }
}
