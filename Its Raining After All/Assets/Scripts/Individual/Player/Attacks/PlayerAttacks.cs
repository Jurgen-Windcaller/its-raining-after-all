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
    [SerializeField] private int dashDmg;

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

    private void Hit(GameObject obj) 
    {
        Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();
        EnemyHealth objHealth = obj.GetComponent<EnemyHealth>();

        objHealth.UpdateHealth(-dashDmg);
        objRb.AddForce(new Vector2(knockbackForce.x * groundMove.facing, knockbackForce.y), ForceMode2D.Impulse);
    }

    private IEnumerator DashTimer(float time)
    {
        dashing = true;
        rb.velocity = new Vector2(dashSpeed * groundMove.facing * Time.fixedDeltaTime, 0f);

        yield return new WaitForSeconds(time);

        rb.velocity = Vector2.zero;
        dashing = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyLayers.ContainsLayer(collision.gameObject.layer))
        {
            if (dashing) { Hit(collision.gameObject); }
        }
    }
}
