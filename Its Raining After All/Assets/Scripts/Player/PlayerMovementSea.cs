using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSea : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float waterGravity = 8f;
    
    private Rigidbody2D rb;
    private Vector2 moveVal;

    // Update is called once per frame
    void Update()
    {
        moveVal = InputManager.Instance.GetSeaMoveRaw() * speed * Time.deltaTime;
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
