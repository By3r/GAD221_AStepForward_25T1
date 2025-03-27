using System.Collections;
using TMPro;
using UnityEngine;

public class SentenceValidator : MonoBehaviour
{
    #region Variables
    [SerializeField] private TMP_Text sentenceTextDisplay;
    [SerializeField] private Sentences _currentSentence;

    [Header("Colours")]
    [Tooltip("Feel free to change the default colours.")]
    [SerializeField] private Color correctLettersColour = Color.green;
    [SerializeField] private Color letterToTypeColour = Color.cyan;
    [SerializeField] private Color incompleteLettersColour = Color.grey;
    [SerializeField] private Color incorrectLetterColour = Color.red;

    private bool _isFlashing = false;
    private int _currentSentenceIndex = 0;
    private string _sentenceToDisplay = "";
    private string _typedText = "";
    #endregion

    #region Public Functions
    public void LoadSentenceSet(Sentences sentenceSet)
    {
        _currentSentence = sentenceSet;
        _currentSentenceIndex = 0;
        _typedText = "";
        DisplaySentence();
    }

    private void DisplaySentence()
    {
        if (_currentSentence == null || _currentSentenceIndex >= _currentSentence.sentencesToType.Count)
        {
            Debug.Log("No more sentences to type");
            return;
        }

        _sentenceToDisplay = _currentSentence.sentencesToType[_currentSentenceIndex];
        _typedText = "";
        SkipSpaces(); 
        UpdateTypingProgress();
    }
    #endregion

    #region Private Functions
    private void Update()
    {
        if (_currentSentence == null || _currentSentenceIndex >= _currentSentence.sentencesToType.Count) return;

        foreach (char c in Input.inputString)
        {
            if (c != '\b')
            {
                if (_typedText.Length < _sentenceToDisplay.Length)
                {
                    if (c == _sentenceToDisplay[_typedText.Length])
                    {
                        _typedText += c;
                        SkipSpaces(); 
                        UpdateTypingProgress();
                    }
                    else if (!_isFlashing)
                    {
                        StartCoroutine(FlashRequiredLetter());
                    }
                }
            }

            if (_typedText == _sentenceToDisplay)
            {
                _currentSentenceIndex++;
                DisplaySentence();
            }
        }
    }

    private void UpdateTypingProgress()
    {
        string correctLetters = $"<color=#{ColorUtility.ToHtmlStringRGB(correctLettersColour)}>{_typedText}</color>";
        string letterToType = _typedText.Length < _sentenceToDisplay.Length
            ? $"<color=#{ColorUtility.ToHtmlStringRGB(letterToTypeColour)}>{_sentenceToDisplay[_typedText.Length]}</color>"
            : "";
        string incompleteLetters = _typedText.Length + 1 < _sentenceToDisplay.Length
            ? $"<color=#{ColorUtility.ToHtmlStringRGB(incompleteLettersColour)}>{_sentenceToDisplay.Substring(_typedText.Length + 1)}</color>"
            : "";

        sentenceTextDisplay.text = correctLetters + letterToType + incompleteLetters;
    }

    private IEnumerator FlashRequiredLetter()
    {
        _isFlashing = true;

        string correctLetters = $"<color=#{ColorUtility.ToHtmlStringRGB(correctLettersColour)}>{_typedText}</color>";
        string flashingLetter = _typedText.Length < _sentenceToDisplay.Length
            ? $"<color=#{ColorUtility.ToHtmlStringRGB(incorrectLetterColour)}>{_sentenceToDisplay[_typedText.Length]}</color>"
            : "";
        string remainingLetters = _typedText.Length + 1 < _sentenceToDisplay.Length
            ? $"<color=#{ColorUtility.ToHtmlStringRGB(incompleteLettersColour)}>{_sentenceToDisplay.Substring(_typedText.Length + 1)}</color>"
            : "";

        sentenceTextDisplay.text = correctLetters + flashingLetter + remainingLetters;

        yield return new WaitForSeconds(0.2f);

        UpdateTypingProgress();
        _isFlashing = false;
    }

    private void SkipSpaces()
    {
        while (_typedText.Length < _sentenceToDisplay.Length && _sentenceToDisplay[_typedText.Length] == ' ')
        {
            _typedText += ' ';
        }
    }
    #endregion
}
