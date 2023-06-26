using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSea : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    
    private Rigidbody2D rb;
    private Vector2 moveVal;

    private void FixedUpdate()
    {
        if (DialougeManager.Instance.dialougePlaying) { return; }

        moveVal = InputManager.Instance.GetSeaMoveRaw() * speed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVal);
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
