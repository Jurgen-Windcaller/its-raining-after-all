using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [HideInInspector] public int facingGround;
    [HideInInspector] public int facingSea;
    [HideInInspector] public int yVel;

    private PlayerMovementGround groundMove;
    private PlayerMovementSea seaMove;

    private WaterDetector waterDetector;

    private Animator animator;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        groundMove = GetComponent<PlayerMovementGround>();
        seaMove = GetComponent<PlayerMovementSea>();
        waterDetector = GetComponent<WaterDetector>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        facingGround = groundMove.facing;
        facingSea = seaMove.facing;

        yVel = GetYVelocity(rb.velocity.y);

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

    private int GetYVelocity(float rawVelocity)
    {
        if (rawVelocity > 0) { return 1; }

        // if it is less than or equal to 0
        return -1;
    }
}
