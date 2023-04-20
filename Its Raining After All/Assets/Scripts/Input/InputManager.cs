using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public float groundMoveRaw;
    [HideInInspector] public bool groundJumpRaw;

    public void OnMove(InputAction.CallbackContext ctx)
    {
        groundMoveRaw = ctx.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        groundJumpRaw = ctx.ReadValueAsButton();
    }
}
