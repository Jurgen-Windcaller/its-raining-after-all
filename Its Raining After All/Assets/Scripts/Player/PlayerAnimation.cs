using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [HideInInspector] public int facingGround;
    [HideInInspector] public int facingSea;
    [HideInInspector] public int yVel;

    private PlayerMovementGround groundMove;
    private PlayerAttacks attacks;
    private WaterDetector waterDetector;
    private Animator animator;

    private int prevFaceDir = 1;

    // Start is called before the first frame update
    void Awake()
    {
        groundMove = GetComponent<PlayerMovementGround>();
        attacks = GetComponent<PlayerAttacks>();
        waterDetector = GetComponent<WaterDetector>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        facingGround = GetFacingDir(InputManager.Instance.GetGroundMoveRaw());
        facingSea = GetFacingDir(InputManager.Instance.GetSeaMoveRaw().x);

        yVel = GetYVelocity(groundMove.rb.velocity.y);

        animator.SetBool("In Water", waterDetector.inWater);
        animator.SetBool("Airborne", !groundMove.grounded);
        animator.SetFloat("YVelocity", yVel);

        animator.SetFloat("Facing", facingGround);
        animator.SetFloat("Water Facing", facingSea);

        // So that falling animations will still be active and will stop themselves when the player touches the ground
        //if (DialougeManager.Instance.dialougePlaying) { return; }

        animator.SetFloat("Direction", InputManager.Instance.GetGroundMoveRaw());
        animator.SetFloat("Water Direction", InputManager.Instance.GetSeaMoveRaw().x);


        //Debug.Log("In Water: " + animator.GetBool("In Water"));
        //Debug.Log("Airborne: " + animator.GetBool("Airborne"));
        //Debug.Log("YVelocity: " + animator.GetFloat("YVelocity"));
        //Debug.Log("Facing: " + animator.GetFloat("Facing"));
        //Debug.Log("Water Facing: " + animator.GetFloat("In Water"));
        //Debug.Log("Direction: " + animator.GetFloat("Direction"));
        //Debug.Log("Water Direction: " + animator.GetFloat("In Water"));
    }

    private int GetFacingDir(float moveDir)
    {
        switch (moveDir)
        {
            case -1f:
                prevFaceDir = -1;
                return -1;

            case 1f:
                prevFaceDir = 1;
                return 1;

            default:
                return prevFaceDir;
        }
    }

    private int GetYVelocity(float rawVelocity)
    {
        if (rawVelocity > 0) { return 1; }

        // if it is less than or equal to 0
        return -1;
    }
}
