using UnityEngine;
using UnityEngine.EventSystems;

public class TaskTextTranslator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Variables
    [SerializeField] private SentenceValidator sentenceValidator;
    [SerializeField] private bool isPlayerSentence = false;
    #endregion

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (sentenceValidator == null) return;

        if (isPlayerSentence)
        {
            sentenceValidator.ShowSentenceTranslation();
        }
        else
        {
            sentenceValidator.npcDialogue?.ShowTranslation();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (sentenceValidator == null) return;

        if (isPlayerSentence)
        {
            sentenceValidator.HideSentenceTranslation();
        }
        else
        {
            sentenceValidator.npcDialogue?.HideTranslation();
        }
    }
}
