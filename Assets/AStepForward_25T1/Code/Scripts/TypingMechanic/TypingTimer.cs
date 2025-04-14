using UnityEngine;
using System;
using TMPro;

public class TypingTimer : MonoBehaviour
{
    #region Variables
    public float timeLimitInSeconds = 10f;

    [SerializeField] private TMP_Text timerText;

    private float _remainingTime;
    private bool _isRunning = false;
    private bool _hasFailed = false;

    public Action OnTimeOut;
    #endregion

    private void Update()
    {
        if (!_isRunning || _hasFailed) return;

        _remainingTime -= Time.deltaTime;
        timerText.text = FormatTime(_remainingTime);

        if (_remainingTime <= 0f)
        {
            _hasFailed = true;
            _isRunning = false;
            timerText.text = "0:00";
            OnTimeOut?.Invoke();
        }
    }

    #region Public Functions
    public void StartTimer()
    {
        _remainingTime = timeLimitInSeconds;
        _isRunning = true;
        _hasFailed = false;
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
    }
    #endregion 

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{minutes}:{seconds:D2}";
    }
}