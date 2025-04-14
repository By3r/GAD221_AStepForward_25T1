using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeneralTextTranslator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Variables
    [SerializeField] private string hoverText;
    [SerializeField] private string defaultText;
    [SerializeField] private TMP_Text _text;
    #endregion

    private void Awake()
    {
        if (_text == null)
            _text = GetComponent<TMP_Text>();

        if (string.IsNullOrEmpty(defaultText))
            defaultText = _text.text;
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
