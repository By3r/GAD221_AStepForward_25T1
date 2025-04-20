using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private NPCDialogueLines dialogueLines;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Image npcImageUI;

    private string currentLine = "";
    private string translatedLine = "";

    public void Speak(int sentenceIndex)
    {
        if (dialogueLines == null || dialogueLines.lines.Count == 0) return;

        if (sentenceIndex < dialogueLines.lines.Count)
        {
            currentLine = dialogueLines.lines[sentenceIndex];
            dialogueText.text = currentLine;

            translatedLine = sentenceIndex < dialogueLines.translatedLines.Count ? dialogueLines.translatedLines[sentenceIndex]: "";

            if (sentenceIndex < dialogueLines.audioClips.Count && dialogueLines.audioClips[sentenceIndex] != null)
            {
                audioSource.clip = dialogueLines.audioClips[sentenceIndex];
                audioSource.Play();
                BackgroundMusicManager.Instance?.FadeOutMusic();
            }

            if (sentenceIndex < dialogueLines.npcImages.Count && npcImageUI != null)
            {
                npcImageUI.sprite = dialogueLines.npcImages[sentenceIndex];
            }
        }
        else
        {
            dialogueText.text = "";
            RestoreMusic();
        }
    }

    public void ShowTranslation()
    {
        if (!string.IsNullOrEmpty(translatedLine))
        {
            dialogueText.text = translatedLine;
        }
    }

    public void HideTranslation()
    {
        dialogueText.text = currentLine;
    }

    public bool IsSpeaking()
    {
        return audioSource.isPlaying;
    }

    private void RestoreMusic()
    {
        BackgroundMusicManager.Instance?.FadeInMusic();
    }
}