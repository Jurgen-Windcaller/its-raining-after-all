using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [HideInInspector] public int facing;
    [HideInInspector] public int yVel;

    private PlayerMovement playerMovement;
    private Animator animator;

    private int prevFaceDir = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        facing = GetFacingDir(playerMovement.input.groundMoveRaw);
        yVel = GetYVelocity(playerMovement.rb.velocity.y);

        animator.SetBool("Airborne", !playerMovement.grounded);

        animator.SetFloat("Direction", playerMovement.input.groundMoveRaw);
        animator.SetFloat("YVelocity", yVel);
        animator.SetFloat("Facing", facing);
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
