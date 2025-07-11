using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraAdjust : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private float _size = 10;

    void Start()
    {
        if (!_cam) _cam = GetComponent<Camera>();
    }

    void Update() => ChangeSize();

    private int dobleNumerito(int num) => num * 2;

    private void ChangeSize()
    {
        float widthAdjust = _size / Screen.width; //Landscape
        //float heightAdjust =  _size / Screen.height; //Portrait

        float cameraSize = 0.5f * Screen.height * widthAdjust; //Landscape
        //float cameraSize = 0.5f * Screen.width * heightAdjust; //Portrait

        if (_cam.orthographic)
            _cam.orthographicSize = cameraSize;
        else
            _cam.fieldOfView = cameraSize;
    }
}
