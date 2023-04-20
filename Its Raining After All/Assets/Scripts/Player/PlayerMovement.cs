using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float groundSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private LayerMask groundedMask;
    [SerializeField] private Transform floorTransform;

    private InputManager input;
    private Rigidbody2D rb;
    private Vector2 moveVec;
    private Collider2D ground;

    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 floor = new Vector2(floorTransform.position.x, floorTransform.position.y);
        ground = Physics2D.OverlapCircle(floor, 0.01f, groundedMask);

        if (ground != null)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(floorTransform.position, 0.01f);
    }

    void FixedUpdate()
    {
        MovePlayer();

        if (input.jumping)
        {
            JumpPlayer();
        }
    }

    private void MovePlayer()
    {
        float moveVal = input.groundMoveRaw * groundSpeed * Time.deltaTime;
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
