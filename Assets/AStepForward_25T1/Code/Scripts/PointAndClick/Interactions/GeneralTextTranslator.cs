using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeneralTextTranslator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    #region Variables
    [SerializeField] private string hoverText;
    [SerializeField] private string defaultText;
    [SerializeField] private TMP_Text text;
    [SerializeField] private bool isButton;

    private bool _isHovered = false;
    private bool _isClicked = false;
    #endregion

    private void Awake()
    {
        if (text == null) text = GetComponent<TMP_Text>();

        if (string.IsNullOrEmpty(defaultText)) defaultText = text.text;
    }
    private void Update()
    {
        if (_isClicked && !_isHovered)
        {
            _isClicked = false;
            text.text = defaultText;
        }
    }

    #region Cursor behaviour
    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovered = true;

        if (_isClicked || isButton) text.text = hoverText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovered = false;
        text.text = defaultText;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _isClicked = true;

        if (_isHovered) text.text = hoverText;
    }
    #endregion
}