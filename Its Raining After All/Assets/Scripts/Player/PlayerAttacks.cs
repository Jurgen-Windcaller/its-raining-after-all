using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public LayerMask enemyLayers;

    [Header("Hurtbox Objects")]
    [SerializeField] private GameObject smackHurtbox;
    [SerializeField] private GameObject waveHurtbox;

    [Header("Animations")]
    [SerializeField] private AnimationClip smackAnim;
    [SerializeField] private AnimationClip waveAnim;

    [Header("Settings")]
    [SerializeField] private Vector2 knockbackForce;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        smackHurtbox.SetActive(false);
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
            StartCoroutine(smackHurtboxTimer(0.40f));
        }
    }

    private void Wave()
    {
        if (InputManager.Instance.GetWave())
        {
            Debug.Log("The character is doing knockback");
        }
    }

    private IEnumerator smackHurtboxTimer(float inactiveTime)
    {
        yield return new WaitForSeconds(inactiveTime);
        smackHurtbox.SetActive(true);
        yield return new WaitForSeconds(smackAnim.length - inactiveTime);
        smackHurtbox.SetActive(false);
    }

    public void HitEnemy(Rigidbody2D enemy) 
    {
        Debug.Log("Hit " + enemy.name);
        enemy.AddForce(knockbackForce, ForceMode2D.Impulse);
    }
}
