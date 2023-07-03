using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private GameObject smackHurtbox;
    //[SerializeField] private Transform waveHurtbox;
    [SerializeField] private AnimationClip smackAnim;

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
}
