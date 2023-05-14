using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClipDetection : MonoBehaviour
{
    private int facingDir;

    private void Update()
    {
        facingDir = -GetComponent<PlayerAnimation>().facingGround;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "____") { return; }

        while (collision != null)
        {
            transform.Translate(new Vector3(10000, 10000, transform.position.z));
        }
    }
}
