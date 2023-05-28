using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialougeManager : Singleton<DialougeManager>
{
    [HideInInspector] public bool dialougePlaying { get; private set; }

    [SerializeField] private GameObject dialougePanel;
    [SerializeField] private TextMeshProUGUI dialougeTextUI;

    private Story story;
    private Vector3 NPCPosition;

    // Start is called before the first frame update
    void Start()
    {
        SetDialouge(false);
        dialougeTextUI.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialougePlaying) { return; }
        if (InputManager.Instance.GetSubmitting()) { ContinueDialouge(); }
    }

    public void EnterDialouge(TextAsset JSON, Transform NPCTransform)
    {
        story = new Story(JSON.text);
        NPCPosition = NPCTransform.position;

        SetDialouge(true);
        ContinueDialouge();
    }

    private void ContinueDialouge()
    {
        if (story.canContinue) { dialougeTextUI.text = story.Continue(); }
        else { StartCoroutine(ExitDialogue()); }
    }

    private IEnumerator ExitDialogue()
    {
        yield return new WaitForSeconds(0.2f);

        SetDialouge(false);
        dialougeTextUI.text = "";
    }

    private void SetDialouge(bool set)
    {
        dialougePlaying = set;
        dialougePanel.SetActive(set);

        if (set == true) { dialougePanel.transform.position = new Vector3(NPCPosition.x, NPCPosition.y + 2.5f, 0f); }
    }
}
