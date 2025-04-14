using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private NPCDialogueLines dialogueLines;
    [SerializeField] private TMP_Text dialogueText;

    private string currentLine = "";
    private string translatedLine = "";

    public void Speak(int sentenceIndex)
    {
        if (dialogueLines == null || dialogueLines.lines.Count == 0) return;

        if (sentenceIndex < dialogueLines.lines.Count)
        {
            currentLine = dialogueLines.lines[sentenceIndex];
            translatedLine = dialogueLines.translatedLines.Count > sentenceIndex
                ? dialogueLines.translatedLines[sentenceIndex]
                : "";
            dialogueText.text = currentLine;
        }
        else
        {
            dialogueText.text = "";
        }
    }

    public void ShowTranslation()
    {
        if (!string.IsNullOrEmpty(translatedLine))
            dialogueText.text = translatedLine;
    }

    public void HideTranslation()
    {
        dialogueText.text = currentLine;
    }
}
