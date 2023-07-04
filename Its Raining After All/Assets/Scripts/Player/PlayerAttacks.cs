using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public LayerMask enemyLayers;

    [SerializeField] private GameObject smackHurtbox;
    [SerializeField] private GameObject waveHurtbox;
    [SerializeField] private AnimationClip smackAnim;
    [SerializeField] private AnimationClip waveAnim;

    private List<Collider2D> hitEnemies = new List<Collider2D>();
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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

    public void AddHitEnemy(Collider2D enemy) { hitEnemies.Add(enemy); }
}
