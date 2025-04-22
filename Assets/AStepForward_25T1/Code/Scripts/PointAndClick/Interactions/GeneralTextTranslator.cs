using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeneralTextTranslator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string hoverText;
    [SerializeField] private string defaultText;
    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        if (text == null) text = GetComponent<TMP_Text>();
        if (string.IsNullOrEmpty(defaultText)) defaultText = text.text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.text = hoverText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.text = defaultText;
    }
}