using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Smack();
        Wave();
    }

    private void Smack()
    {
        if (InputManager.Instance.GetSmack())
        {
            animator.SetTrigger("Smack");
            Debug.Log("The character is attacking");
        }
    }

    private void Wave()
    {
        if (InputManager.Instance.GetWave())
        {
            Debug.Log("The character is doing knockback");
        }
    }
}
