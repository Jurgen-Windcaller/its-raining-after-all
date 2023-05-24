using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private float groundMoveRaw;

    private bool jumping, interacting, submitting;

    private Vector2 seaMoveRaw;

    #region PlayerInput Functions
    public void OnGroundMove(InputAction.CallbackContext ctx) { groundMoveRaw = ctx.ReadValue<float>(); }
    public void OnSeaMove(InputAction.CallbackContext ctx) { seaMoveRaw = ctx.ReadValue<Vector2>(); }

    public void OnJump(InputAction.CallbackContext ctx) 
    { 
        if (ctx.performed) { jumping = true; }
        else if (ctx.canceled) { jumping = false; }
    }

    public void OnInteract(InputAction.CallbackContext ctx) 
    {
        if (ctx.performed) { interacting = true; }
        else if (ctx.canceled) { interacting = false; }
    }

    public void OnSubmit(InputAction.CallbackContext ctx) 
    {
        if (ctx.performed) { submitting = true; }
        else if (ctx.canceled) { submitting = false; }
    }

    #endregion

    #region Getters
    public float GetGroundMoveRaw() { return groundMoveRaw; }
    public Vector2 GetSeaMoveRaw() { return seaMoveRaw; }
    public bool GetJumping() { bool prevJumping = jumping; jumping = false; return prevJumping; }
    public bool GetInteracting() { bool prevInteracting = interacting; interacting = false; return prevInteracting; }
    public bool GetSubmitting() { bool prevSubmitting = submitting; submitting = false; return prevSubmitting; }
    #endregion
}
