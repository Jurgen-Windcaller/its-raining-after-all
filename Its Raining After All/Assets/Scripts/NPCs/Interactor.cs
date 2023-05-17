using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public bool inInteractableRange;

    [SerializeField] private SpriteRenderer interactableIcon;

    private void Update()
    {
        if (inInteractableRange) { interactableIcon.enabled = true; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") { inInteractableRange = true; }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") { inInteractableRange = false; }
    }
}
