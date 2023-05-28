using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private GameObject interactableIcon;
    [SerializeField] private TextAsset textJSON;

    private GameObject parent;

    private bool inInteractableRange;

    private void Start()
    {
        inInteractableRange = false;
        interactableIcon.SetActive(false);
    }

    private void Update()
    {
        if (inInteractableRange && !DialougeManager.Instance.dialougePlaying)
        {
            if (InputManager.Instance.GetInteracting())
            {
                DialougeManager.Instance.EnterDialouge(textJSON, transform);
            } 
            else { interactableIcon.SetActive(true); } // change to set true when dialouge is not playing

        } else { interactableIcon.SetActive(false); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") { inInteractableRange = true; }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") { inInteractableRange = false; }
    }
}
