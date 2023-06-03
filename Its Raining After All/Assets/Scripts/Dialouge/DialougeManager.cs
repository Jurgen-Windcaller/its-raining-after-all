using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialougeManager : Singleton<DialougeManager>
{
    [HideInInspector] public bool dialougePlaying { get; private set; }

    [SerializeField] private GameObject dialougePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialougeTextUI;

    [SerializeField] private float typingSpeed = 0.05f;

    private Story story;
    private Vector3 NPCPosition;
    private Coroutine lineCoroutine;

    private bool canContinue = false;

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
        if (canContinue && InputManager.Instance.GetSubmitting()) { ContinueDialouge(); }
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
        if (story.canContinue) 
        {
            if (lineCoroutine != null) { StopCoroutine(lineCoroutine); }

            lineCoroutine = StartCoroutine(DisplayLine(story.Continue())); 
        }
        else { StartCoroutine(ExitDialogue()); }
    }

    private IEnumerator ExitDialogue()
    {
        yield return new WaitForSeconds(0.2f);

        SetDialouge(false);
        dialougeTextUI.text = "";
    }

    private IEnumerator DisplayLine(string line)
    {
        dialougeTextUI.text = "";

        canContinue = false;
        continueIcon.SetActive(false);

        foreach (char letter in line.ToCharArray())
        {
            if (InputManager.Instance.GetSubmitting()) { dialougeTextUI.text = line; break; }

            dialougeTextUI.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        canContinue = true;
        continueIcon.SetActive(true);
    }

    private void SetDialouge(bool set)
    {
        dialougePlaying = set;
        dialougePanel.SetActive(set);

        if (set == true) { dialougePanel.transform.position = new Vector3(NPCPosition.x, NPCPosition.y + 2.5f, 0f); }
    }
}
