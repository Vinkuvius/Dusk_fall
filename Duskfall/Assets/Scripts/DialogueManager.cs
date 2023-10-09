using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    private bool isDialogueActive = false;

    private void Start()
    {
        dialogueText.gameObject.SetActive(false);
    }

    public void StartDialogue(string dialogue)
    {
        isDialogueActive = true;
        dialogueText.text = dialogue;
        dialogueText.gameObject.SetActive(true);
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        dialogueText.gameObject.SetActive(false);
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
}
