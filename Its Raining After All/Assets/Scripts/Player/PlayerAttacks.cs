using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
