using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : Controller, IDragHandler, IEndDragHandler
{
    private Vector3 initialPosition;
    [SerializeField] float maxMagnitude = 75;

    private void Start()
    {
        initialPosition = transform.position;
    }
    public override Vector3 GetMovement()
    {
        Vector3 changedDir = new Vector3(_moveDir.x, 0, _moveDir.y);
        changedDir /= maxMagnitude;
        return changedDir;
    }
    public void OnDrag(PointerEventData eventData)
    {
        _moveDir = Vector3.ClampMagnitude((Vector3)eventData.position - initialPosition, maxMagnitude);
        transform.position = initialPosition + _moveDir;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = initialPosition;
        _moveDir = Vector3.zero;
    }
}
