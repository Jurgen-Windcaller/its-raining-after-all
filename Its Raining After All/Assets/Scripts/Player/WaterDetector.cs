using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDetector : MonoBehaviour
{
    public bool inWater;

    [SerializeField] int waterLayer;

    private PlayerMovementGround groundMove;
    private PlayerMovementSea seaMove;

    // Start is called before the first frame update
    void Start()
    {
        groundMove = GetComponent<PlayerMovementGround>();
        seaMove = GetComponent<PlayerMovementSea>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inWater)
        {
            seaMove.enabled = true;
            groundMove.enabled = false;
        }
        else
        {
            groundMove.enabled = true;
            seaMove.enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer != waterLayer) { return; }

        inWater = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != waterLayer) { return; }

        inWater = false;
    }
}
