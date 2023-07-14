using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] private PlayerAttacks attackManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (attackManager.enemyLayers.ContainsLayer(collision.gameObject.layer))
        {
            attackManager.HitEnemy(collision.GetComponent<Rigidbody2D>());
        }
    }
}
