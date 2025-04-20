using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private NPCDialogueLines dialogueLines;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private AudioSource audioSource; 

    private string currentLine = "";
    private string translatedLine = "";

    public void Speak(int sentenceIndex)
    {
        if (dialogueLines == null || dialogueLines.lines.Count == 0) return;

        if (sentenceIndex < dialogueLines.lines.Count)
        {
            currentLine = dialogueLines.lines[sentenceIndex];
            dialogueText.text = currentLine;

            translatedLine = sentenceIndex < dialogueLines.translatedLines.Count? dialogueLines.translatedLines[sentenceIndex]: "";

            if (sentenceIndex < dialogueLines.audioClips.Count && dialogueLines.audioClips[sentenceIndex] != null)
            {
                audioSource.clip = dialogueLines.audioClips[sentenceIndex];
                audioSource.Play();
                BackgroundMusicManager.Instance?.FadeOutMusic();
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