using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementGround : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;

    [HideInInspector] public int facing = 1;

    [HideInInspector] public bool grounded;

    [SerializeField] private LayerMask groundedMask;

    [SerializeField] private PhysicsMaterial2D fullFriction;
    [SerializeField] private PhysicsMaterial2D noFriction;

    [SerializeField] private float groundSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float floorCheckRad = 0.07f;
    [SerializeField] private float slopeCheckRad = 0.25f;

    private Vector2 moveVec;
    private Vector2 colSize;
    private Vector2 slopePerpendicular;

    private Vector3 floor;

    private Collider2D ground;

    private CapsuleCollider2D playerCol;

    private SpriteRenderer sprite;

    private float slopeDownAngle;
    private float prevSlopeDownAngle;
    private float slopeSideAngle;

    private int prevFaceDir = 1;

    private bool onSlope;
    private bool jumping;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCol = GetComponent<CapsuleCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        colSize = playerCol.size;
    }

    // Update is called once per frame
    private void Update()
    {
        facing = GetFacingDir(InputManager.Instance.GetGroundMoveRaw());

        if (facing == 1) { sprite.flipX = false; }
        else { sprite.flipX = true; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(floor, floorCheckRad);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(floor, slopeCheckRad);
    }

    void FixedUpdate()
    {
        if (DialougeManager.Instance.dialougePlaying) { return; }

        ground = Physics2D.OverlapCircle(floor, floorCheckRad, groundedMask);
        floor = transform.position - new Vector3(0f, colSize.y / 2);

        GroundCheck();
        SlopeCheck();

        MovePlayer();
        JumpPlayer();
    }

    private void MovePlayer()
    {
        float xIn = InputManager.Instance.GetGroundMoveRaw();

        float moveValPos = xIn * groundSpeed * Time.deltaTime;
        float moveValNeg = -xIn * groundSpeed * Time.deltaTime;

        if (grounded && !onSlope && !jumping)
        {
            moveVec.Set(moveValPos, 0f);
            rb.velocity = moveVec;
        }
        else if (grounded && onSlope && !jumping)
        {
            moveVec.Set(moveValNeg * slopePerpendicular.x, moveValNeg * slopePerpendicular.y);
            rb.velocity = moveVec;
        }
        else if (!grounded)
        {
            moveVec.Set(moveValPos, rb.velocity.y);
            rb.velocity = moveVec;
        }
    }

    private void JumpPlayer()
    {
        if (grounded && InputManager.Instance.GetJumping())
        {
            jumping = true;
            rb.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    private void GroundCheck()
    {
        if (rb.velocity.y <= 0f) { jumping = false; }

        if (ground != null && !jumping) { grounded = true; }
        else { grounded = false; }
    }

    private void SlopeCheck()
    {
        SlopeCheckY(floor);
        SlopeCheckX(floor);
    }

    private void SlopeCheckX(Vector2 pos)
    {
        RaycastHit2D hitFront = Physics2D.Raycast(pos, transform.right, slopeCheckRad, groundedMask);
        RaycastHit2D hitBack = Physics2D.Raycast(pos, -transform.right, slopeCheckRad, groundedMask);

        if (hitFront)
        {
            onSlope = true;
            slopeSideAngle = Vector2.Angle(hitFront.normal, Vector2.up);
        }
        else if (hitBack)
        {
            onSlope = true;
            slopeSideAngle = Vector2.Angle(hitBack.normal, Vector2.up);
        }
        else
        {
            onSlope = false;
            slopeSideAngle = 0f;
        }
    }

    private void SlopeCheckY(Vector2 pos)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.down, slopeCheckRad, groundedMask);

        if(hit)
        {
            slopePerpendicular = Vector2.Perpendicular(hit.normal).normalized;
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != prevSlopeDownAngle) { onSlope = true; }

            prevSlopeDownAngle = slopeDownAngle;

            Debug.DrawRay(hit.point, hit.normal, Color.green);
            Debug.DrawRay(hit.point, slopePerpendicular, Color.red);
        }

        if (onSlope && InputManager.Instance.GetGroundMoveRaw() == 0f)
        {
            rb.sharedMaterial = fullFriction;
        }
        else
        {
            rb.sharedMaterial = noFriction;
        }
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
}
