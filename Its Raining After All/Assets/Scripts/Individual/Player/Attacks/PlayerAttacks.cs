using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayers;

    public bool dashing = false;

    [Header("Hurtbox Objects")]
    [SerializeField] private GameObject waveHurtbox;

    [Header("Animations")]
    [SerializeField] private AnimationClip dashAnim;
    [SerializeField] private AnimationClip waveAnim;

    [Header("Settings")]
    [SerializeField] private Vector2 knockbackForce;

    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;

    private Animator animator;

    private Rigidbody2D rb;

    private PlayerMovementGround groundMove;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        groundMove = GetComponent<PlayerMovementGround>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Dash();
        //Wave();
    }

    private void Dash()
    {
        if (!dashing && InputManager.Instance.GetDash())
        {
            animator.SetTrigger("Dash");
            StartCoroutine(DashTimer(dashTime));
        }
    }

    private void Wave()
    {
        if (InputManager.Instance.GetWave())
        {
            Debug.Log("The character is doing knockback");
        }
    }

    private void HitEnemy(GameObject enemy) 
    {
        Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
        

        enemyRb.AddForce(new Vector2(knockbackForce.x * groundMove.facing, knockbackForce.y), ForceMode2D.Impulse);
    }

    private IEnumerator DashTimer(float time)
    {
        dashing = true;
        rb.velocity = new Vector2(dashSpeed * groundMove.facing * Time.fixedDeltaTime, rb.velocity.y);

        yield return new WaitForSeconds(time);

        rb.velocity = new Vector2(0f, rb.velocity.y);
        dashing = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyLayers.ContainsLayer(collision.gameObject.layer))
        {
            if (dashing) { HitEnemy(collision.gameObject); }
        }
    }
}
