using UnityEngine;
using TMPro;

public class TypingTimer : MonoBehaviour
{
    [Header("Timer UI")]
    [SerializeField] private TMP_Text timerText;

    public float timeLimitInSeconds = 60f;

    private float _remainingTime;
    private bool _isRunning = false;
    private bool _hasFailed = false;
    private Sentences _currentTask;

    private void OnEnable()
    {
        GameEvents.OnTaskStarted += StartTimer;
        GameEvents.OnTaskCompleted += _ => StopTimer();
        GameEvents.OnTaskFailed += _ => StopTimer();
    }

    private void OnDisable()
    {
        GameEvents.OnTaskStarted -= StartTimer;
        GameEvents.OnTaskCompleted -= _ => StopTimer();
        GameEvents.OnTaskFailed -= _ => StopTimer();
    }

    private void Update()
    {
        if (!_isRunning || _hasFailed) return;

        _remainingTime -= Time.deltaTime;
        timerText.text = FormatTime(_remainingTime);

        if (_remainingTime <= 0f)
        {
            _isRunning = false;
            _hasFailed = true;
            timerText.text = "0:00";
            GameEvents.OnTimerExpired?.Invoke(_currentTask);
        }
    }

    public void StartTimer(Sentences sentenceData)
    {
        _currentTask = sentenceData;
        _remainingTime = sentenceData.timeLimitInMinutes * 60f;
        _isRunning = true;
        _hasFailed = false;

        GameEvents.OnTimerStarted?.Invoke(sentenceData);
    }

    public void StopTimer()
    {
        _isRunning = false;
    }

    public void ResetTimer()
    {
        _remainingTime = timeLimitInSeconds;
        _isRunning = false;
        _hasFailed = false;
        timerText.text = FormatTime(_remainingTime);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{minutes}:{seconds:D2}";
    }
}