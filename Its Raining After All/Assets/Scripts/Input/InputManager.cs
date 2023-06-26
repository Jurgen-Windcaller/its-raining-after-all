using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private float groundMoveRaw;

    private bool jumping, interacting, submitting, smacking, waving;

    private Vector2 seaMoveRaw;

    #region PlayerInput Functions
    private void OnGroundMove(InputAction.CallbackContext ctx) { groundMoveRaw = ctx.ReadValue<float>(); }
    private void OnSeaMove(InputAction.CallbackContext ctx) { seaMoveRaw = ctx.ReadValue<Vector2>(); }

    private void OnJump(InputAction.CallbackContext ctx) 
    { 
        if (ctx.performed) { jumping = true; }
        else if (ctx.canceled) { jumping = false; }
    }

    private void OnInteract(InputAction.CallbackContext ctx) 
    {
        if (ctx.performed) { interacting = true; }
        else if (ctx.canceled) { interacting = false; }
    }

    private void OnSubmit(InputAction.CallbackContext ctx) 
    {
        if (ctx.performed) { submitting = true; }
        else if (ctx.canceled) { submitting = false; }
    }

    private void OnSmack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) { smacking = true; }
        else if (ctx.canceled) { smacking = false; }
    }

    private void OnWave(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) { waving = true; }
        else if (ctx.canceled) { waving = false; }
    }

    #endregion

    #region Getters
    public float GetGroundMoveRaw() { return groundMoveRaw; }
    public Vector2 GetSeaMoveRaw() { return seaMoveRaw; }
    public bool GetJumping() { bool prevJumping = jumping; jumping = false; return prevJumping; }
    public bool GetInteracting() { bool prevInteracting = interacting; interacting = false; return prevInteracting; }
    public bool GetSubmitting() { bool prevSubmitting = submitting; submitting = false; return prevSubmitting; }
    public bool GetSmack() { bool prevSmack = smacking; smacking = false; return prevSmack; }
    public bool GetWave() { bool prevWave = waving; waving = false; return prevWave; }
    #endregion
}
