using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttons : Controller
{
    public override Vector3 GetMovement()
    {
        return _moveDir;
    }

    public void MoveUp() { _moveDir = Vector3.forward; }
    public void MoveDown() { _moveDir = Vector3.back; }
    public void MoveLeft() { _moveDir = Vector3.left; }
    public void MoveRight() { _moveDir = Vector3.right; }
    public void StopMove() { _moveDir = Vector3.zero; }
}
