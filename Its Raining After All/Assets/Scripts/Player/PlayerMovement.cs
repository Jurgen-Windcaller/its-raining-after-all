using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float groundSpeed = 5f;

    private InputManager input;
    private Rigidbody2D rb;
    private Vector2 moveVec;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveVal = input.groundMoveRaw * groundSpeed * Time.deltaTime;
        moveVec = new Vector2(moveVal, rb.velocity.y);

        rb.velocity = moveVec;
    }
}
