using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerMovementSea : MonoBehaviour
{
    [HideInInspector] public int facing = 1;

    [SerializeField] private float speed = 5f;
    
    private Rigidbody2D rb;

    private SpriteRenderer sprite;

    private Vector2 moveVal;

    private int prevFaceDir = 1;

    private void Update()
    {
        facing = GetFacingDir(InputManager.Instance.GetSeaMoveRaw().x);

        if (facing == 1) { sprite.flipX = false; }
        else { sprite.flipX = true; }
    }

    private void FixedUpdate()
    {
        if (DialougeManager.Instance.dialougePlaying) { return; }

        moveVal = InputManager.Instance.GetSeaMoveRaw() * speed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVal);
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
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
