using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    #region Variables
    [SerializeField] private NPCDialogueLines dialogueLines;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Image npcImageUI;
    [SerializeField] private Image backgroundImageUI;

    private string _currentLine = "";
    private string _translatedLine = "";
    #endregion
    public void Speak(int sentenceIndex)
    {
        if (dialogueLines == null || dialogueLines.lines.Count == 0) return;

        if (sentenceIndex < dialogueLines.lines.Count)
        {
            _currentLine = dialogueLines.lines[sentenceIndex];
            dialogueText.text = _currentLine;

            _translatedLine = sentenceIndex < dialogueLines.translatedLines.Count ? dialogueLines.translatedLines[sentenceIndex] : "";

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

            if (backgroundImageUI != null)
            {
                bool showBG = sentenceIndex < dialogueLines.showBackgroundImage.Count && dialogueLines.showBackgroundImage[sentenceIndex];
                if (showBG && sentenceIndex < dialogueLines.backgroundImages.Count && dialogueLines.backgroundImages[sentenceIndex] != null)
                {
                    backgroundImageUI.gameObject.SetActive(true);
                    backgroundImageUI.sprite = dialogueLines.backgroundImages[sentenceIndex];
                }
                else
                {
                    backgroundImageUI.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            dialogueText.text = "";
            RestoreMusic();

            if (backgroundImageUI != null)
                backgroundImageUI.gameObject.SetActive(false);
        }
    }

    public void ShowTranslation()
    {
        if (!string.IsNullOrEmpty(_translatedLine))
        {
            dialogueText.text = _translatedLine;
        }
    }

    public void HideTranslation()
    {
        dialogueText.text = _currentLine;
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