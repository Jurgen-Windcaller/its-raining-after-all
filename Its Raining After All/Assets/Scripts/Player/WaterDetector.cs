using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterDetector : MonoBehaviour
{
    public bool inWater;

    [SerializeField] int waterLayer;

    private PlayerInput inputActions;
    private PlayerMovementGround groundMove;
    private PlayerMovementSea seaMove;

    // Start is called before the first frame update
    void Start()
    {
        inputActions = GetComponent<PlayerInput>();
        groundMove = GetComponent<PlayerMovementGround>();
        seaMove = GetComponent<PlayerMovementSea>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inWater && inputActions.currentActionMap.name != "Water") { inputActions.SwitchCurrentActionMap("Water"); }
        else if (!inWater && inputActions.currentActionMap.name != "Ground") { inputActions.SwitchCurrentActionMap("Ground"); }

        SwitchMovementScript(inputActions.currentActionMap);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer != waterLayer) { return; }

        inWater = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != waterLayer) { return; }

        inWater = false;
    }

    private void SwitchMovementScript(InputActionMap actionMap)
    {
        switch (actionMap.name)
        {
            case "Water":
                if (!seaMove.enabled) 
                { 
                    seaMove.enabled = true; 
                    groundMove.enabled = false; 
                }

                break;
            case "Ground":
                if (!groundMove.enabled) 
                { 
                    groundMove.enabled = true; 
                    seaMove.enabled = false; 
                }

                break;
        }
    }
}
