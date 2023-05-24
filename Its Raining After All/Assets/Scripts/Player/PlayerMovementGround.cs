using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementGround : MonoBehaviour
{
    [SerializeField] private float groundSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private LayerMask groundedMask;
    [SerializeField] private Transform floorTransform;

    [HideInInspector] public Rigidbody2D rb;

    [HideInInspector] public bool grounded;

    private Vector2 moveVec;
    private Collider2D ground;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(floorTransform.position, 0.07f);
    }

    void FixedUpdate()
    {
        if (DialougeManager.Instance.dialougePlaying) { return; }

        ground = Physics2D.OverlapCircle(floorTransform.position, 0.07f, groundedMask);

        if (ground != null) { grounded = true; }
        else { grounded = false; }

        MovePlayer();

        if (InputManager.Instance.GetJumping()) { JumpPlayer(); }
    }

    private void MovePlayer()
    {
        float moveVal = InputManager.Instance.GetGroundMoveRaw() * groundSpeed * Time.deltaTime;
        moveVec = new Vector2(moveVal, rb.velocity.y);

        rb.velocity = moveVec;
    }

    private void JumpPlayer()
    {
        if (grounded)
        {
            rb.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}
