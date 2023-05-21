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
    public void OnJump(InputAction.CallbackContext ctx) { jumping = ctx.ReadValueAsButton(); }
    public void OnSeaMove(InputAction.CallbackContext ctx) { seaMoveRaw = ctx.ReadValue<Vector2>(); }
    public void OnInteract(InputAction.CallbackContext ctx) { interacting = ctx.ReadValueAsButton(); }
    public void OnSubmit(InputAction.CallbackContext ctx) { submitting = ctx.ReadValueAsButton(); }
    #endregion

    #region Getters
    public float GetGroundMoveRaw() { return groundMoveRaw; }
    public bool GetJumping() { return jumping; }
    public bool GetInteracting() {  return interacting; }
    public bool GetSubmitting() {  return submitting; }
    public Vector2 GetSeaMoveRaw() { return seaMoveRaw; }
    #endregion
}
