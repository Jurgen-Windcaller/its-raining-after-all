using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public float groundMoveRaw;
    [HideInInspector] public bool jumping;

    [HideInInspector] public Vector2 seaMoveRaw;

    public void OnGroundMove(InputAction.CallbackContext ctx)
    {
        groundMoveRaw = ctx.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        jumping = ctx.ReadValueAsButton();
    }

    public void OnSeaMove(InputAction.CallbackContext ctx)
    {
        seaMoveRaw = ctx.ReadValue<Vector2>();
    }
}
