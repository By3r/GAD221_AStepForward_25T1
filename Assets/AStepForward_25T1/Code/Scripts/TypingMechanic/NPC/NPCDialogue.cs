using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private NPCDialogueLines dialogueLines;
    [SerializeField] private TMP_Text dialogueText; 

    public void Speak(int sentenceIndex)
    {
        if (dialogueLines == null || dialogueLines.lines.Count == 0) return;

        if (sentenceIndex < dialogueLines.lines.Count)
        {
            dialogueText.text = dialogueLines.lines[sentenceIndex];
        }
        else
        {
            dialogueText.text = "";
        }
    }
}
