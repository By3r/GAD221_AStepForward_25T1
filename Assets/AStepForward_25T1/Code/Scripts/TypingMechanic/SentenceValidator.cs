using System.Collections;
using TMPro;
using UnityEngine;

public class SentenceValidator : MonoBehaviour
{
    #region Variables
    [SerializeField] private TMP_Text sentenceTextDisplay;
    public Sentences currentSentence;
    public NPCDialogue npcDialogue;

    [Header("Colours")]
    [SerializeField] private Color correctLettersColour = Color.green;
    [SerializeField] private Color letterToTypeColour = Color.cyan;
    [SerializeField] private Color incompleteLettersColour = Color.grey;
    [SerializeField] private Color incorrectLetterColour = Color.red;

    [Header("Script References")]
    [SerializeField] private Tooltip tooltip;
    [SerializeField] private UmlautCharConverter umlautConverter;
    [SerializeField] private TypingTimer typingTimer;
    [SerializeField] private DaySystem daySystem;

    private int _mistakeCount = 0;
    private bool _hasStartedTyping = false;
    private int _spacePressCount = 0;
    private bool _warnAboutSpace = true;
    private bool _isFlashing = false;
    private int _currentSentenceIndex = 0;
    private string _sentenceToDisplay = "";
    private string _typedText = "";
    private bool _showingTranslation = false;
    private bool _taskTimerStarted = false;

    private SentenceTriggerer _currentTriggerer;
    #endregion

    public void LoadSentenceSet(Sentences sentenceSet, SentenceTriggerer triggerer)
    {
        currentSentence = sentenceSet;
        _currentTriggerer = triggerer;
        npcDialogue = triggerer.GetNPCDialogue();
        _currentSentenceIndex = 0;
        _typedText = "";
        _taskTimerStarted = false;
        DisplaySentence();
    }

    public int GetCurrentSentenceIndex() => _currentSentenceIndex;

    public void ShowSentenceTranslation()
    {
        if (currentSentence.translatedSentences.Count > _currentSentenceIndex)
        {
            _showingTranslation = true;
            sentenceTextDisplay.text = currentSentence.translatedSentences[_currentSentenceIndex];
        }
    }

    public void HideSentenceTranslation()
    {
        _showingTranslation = false;
        UpdateTypingProgress();
    }

    private void DisplaySentence()
    {
        if (currentSentence == null || _currentSentenceIndex >= currentSentence.sentencesToType.Count)
        {
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

    #region Private Functions
    private void UpdateTypingProgress()
    {
        if (_showingTranslation)
        {
            ShowSentenceTranslation();
            return;
        }

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

        if (!_hasStartedTyping)
        {
            _hasStartedTyping = true;

            if (!_taskTimerStarted)
            {
                _taskTimerStarted = true;
                typingTimer.timeLimitInSeconds = currentSentence.timeLimitInMinutes * 60f;
                typingTimer.StartTimer();
                typingTimer.OnTimeOut += HandleTimerExpired;
            }
        }

        if (c == ' ' && expectedChar != ' ')
        {
            if (_warnAboutSpace)
            {
                _spacePressCount++;
                if (_spacePressCount <= 3)
                    tooltip?.ShowTooltip("You don't need to press space!");
                if (_spacePressCount >= 3)
                    _warnAboutSpace = false;
            }
            return;
        }

        bool isCorrect = IsSkippablePunctuation(expectedChar) || char.ToLowerInvariant(c) == char.ToLowerInvariant(expectedChar);

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
            _currentSentenceIndex++;
            _hasStartedTyping = false;

            if (_currentSentenceIndex >= currentSentence.sentencesToType.Count)
            {
                typingTimer.StopTimer();
                _taskTimerStarted = false;
                daySystem.CompleteTask(currentSentence);
                _currentTriggerer?.OnTaskCompleted();
            }

            DisplaySentence();
        }
    }

    private void HandleTimerExpired()
    {
        Debug.Log("Time’s up! Task failed.");
        typingTimer.StopTimer();
        _taskTimerStarted = false;

        if (_currentTriggerer != null)
        {
            daySystem.FailTask(currentSentence, _currentTriggerer.GetOngoingPanel());
            _currentTriggerer.OnTaskFailed();
        }
    }


    private bool IsUmlautOrSharpS(char c)
    {
        return "äöüÄÖÜß".Contains(c.ToString());
    }

    private bool IsSkippablePunctuation(char c)
    {
        return char.IsPunctuation(c) && !IsUmlautOrSharpS(c);
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
    #endregion
}
