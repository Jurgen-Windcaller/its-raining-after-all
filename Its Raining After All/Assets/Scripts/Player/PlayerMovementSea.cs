using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSea : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float waterGravity = 8f;

    private InputManager input;
    private Rigidbody2D rb;
    private Vector2 moveVal;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        moveVal = input.seaMoveRaw * speed * Time.deltaTime;
    }

    private void FixedUpdate()
    { 
        rb.MovePosition(rb.position + moveVal);
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = waterGravity;
    }

    private void OnDisable()
    {
        rb.gravityScale = 1f;
    }
}
