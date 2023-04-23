using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [HideInInspector] public int facing;

    private InputManager inputManager;
    private Animator animator;

    private int prevFaceDir = 1;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        facing = GetFacingDir(inputManager.groundMoveRaw);

        animator.SetFloat("Direction", inputManager.groundMoveRaw);
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
}
