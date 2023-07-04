using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] private PlayerAttacks attackManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // checks if collision is in the enemy layer mask
        if (attackManager.enemyLayers == (attackManager.enemyLayers | (1 << collision.gameObject.layer)))
        {
            attackManager.AddHitEnemy(collision);
        }
    }
}
