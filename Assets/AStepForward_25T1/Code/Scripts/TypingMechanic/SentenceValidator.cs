using System.Collections;
using TMPro;
using UnityEngine;

public class SentenceValidator : MonoBehaviour
{
    #region Variables
    [SerializeField] private TMP_Text sentenceTextDisplay;
    [SerializeField] private Sentences currentSentence;

    [Header("Colours")]
    [Tooltip("Feel free to change the default colours.")]
    [SerializeField] private Color correctLettersColour = Color.green;
    [SerializeField] private Color letterToTypeColour = Color.cyan;
    [SerializeField] private Color incompleteLettersColour = Color.grey;
    [SerializeField] private Color incorrectLetterColour = Color.red;

    [Header("Script References")]
    [SerializeField] private Tooltip tooltip;
    [SerializeField] private UmlautCharConverter umlautConverter;
    [SerializeField] private NPCDialogue npcDialogue;

    private int _mistakeCount = 0;
    private bool _hasStartedTyping = false;
    private int _spacePressCount = 0;
    private bool _warnAboutSpace = true;
    private bool _isFlashing = false;
    private int _currentSentenceIndex = 0;
    private string _sentenceToDisplay = "";
    private string _typedText = "";
    #endregion

    #region Public Functions
    public void LoadSentenceSet(Sentences sentenceSet)
    {
        currentSentence = sentenceSet;
        _currentSentenceIndex = 0;
        _typedText = "";
        DisplaySentence();
    }

    private void DisplaySentence()
    {
        if (currentSentence == null || _currentSentenceIndex >= currentSentence.sentencesToType.Count)
        {
            Debug.Log("No more sentences to type");
            sentenceTextDisplay.text = "";
            return;
        }

        _sentenceToDisplay = currentSentence.sentencesToType[_currentSentenceIndex];
        _typedText = "";
        SkipSpaces();
        SkipPunctuation(); 
        UpdateTypingProgress();

        npcDialogue?.Speak(_currentSentenceIndex);
        _hasStartedTyping = false;
    }

    #endregion

    #region Private Functions
    private void Update()
    {
        if (currentSentence == null || _currentSentenceIndex >= currentSentence.sentencesToType.Count) return;

        char? umlautInput = umlautConverter?.GetUmlautCharacter();
        if (umlautInput.HasValue)
        {
            ProcessInputChar(umlautInput.Value);
        }

        foreach (char c in Input.inputString)
        {
            if (c != '\b')
            {
                ProcessInputChar(c);
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

    private void ProcessInputChar(char c)
    {
        if (_typedText.Length >= _sentenceToDisplay.Length) return;

        char expectedChar = _sentenceToDisplay[_typedText.Length];

        if (expectedChar == ' ' && c != ' ')
        {
            if (_warnAboutSpace)
            {
                _spacePressCount++;

                if (_spacePressCount <= 3)
                {
                    tooltip?.ShowTooltip("You don't need to press space!");
                }

                if (_spacePressCount >= 3)
                {
                    _warnAboutSpace = false;
                }
            }
            return;
        }

        bool isCorrect = false;

        #region Accept any char for punctuation chars
        if (IsSkippablePunctuation(expectedChar))
        {
            isCorrect = true;
        }
        else if (char.ToLowerInvariant(c) == char.ToLowerInvariant(expectedChar))
        {
            isCorrect = true;
        }
        #endregion

        if (isCorrect)
        {
            _typedText += expectedChar;
            SkipSpaces();
            UpdateTypingProgress();
        }
        else if (!_isFlashing)
        {
            StartCoroutine(FlashRequiredLetter());

            _mistakeCount++;

            if (_mistakeCount >= 2 && IsUmlautOrSharpS(expectedChar))
            {
                tooltip?.ShowTooltip("Hold Tab and the letter you want to type. For ß (sharp S), it's Tab + S.");
                _mistakeCount = 0;
            }
        }

        if (_typedText == _sentenceToDisplay)
        {
            npcDialogue?.Speak(_currentSentenceIndex);
            _currentSentenceIndex++;
            _hasStartedTyping = false;
            DisplaySentence();
        }
    }

    private bool IsUmlautOrSharpS(char c)
    {
        return c == 'ä' || c == 'ö' || c == 'ü' || c == 'Ä' || c == 'Ö' || c == 'Ü' || c == 'ß';
    }

    private void SkipSpaces()
    {
        while (_typedText.Length < _sentenceToDisplay.Length && _sentenceToDisplay[_typedText.Length] == ' ')
        {
            _typedText += ' ';
        }
    }

    private void SkipPunctuation()
    {
        while (_typedText.Length < _sentenceToDisplay.Length &&
               IsSkippablePunctuation(_sentenceToDisplay[_typedText.Length]))
        {
            _typedText += _sentenceToDisplay[_typedText.Length];
        }
    }

    private bool IsSkippablePunctuation(char c)
    {
        return char.IsPunctuation(c) && !IsUmlautOrSharpS(c);
    }

    #endregion
}