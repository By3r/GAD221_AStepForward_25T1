using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextLanguageEditor : MonoBehaviour
{
    [SerializeField] private string hoverText;
    [SerializeField] private string defaultText;

    [SerializeField] private TMP_Text _text;

    private void Awake()
    {
        if (string.IsNullOrEmpty(defaultText) && _text != null)
        {
            defaultText = _text.text;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_text != null)
            _text.text = hoverText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_text != null)
            _text.text = defaultText;
    }
}