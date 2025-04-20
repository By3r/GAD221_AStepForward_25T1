using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DaySystem : MonoBehaviour
{
    #region Variables
    public int totalRequiredTasksPerDay = 3;

    [Header("UI")]
    [SerializeField] private TMP_Text dayTrackerText;
    [SerializeField] private TMP_Text tasksCompletedText;
    [SerializeField] private GameObject taskOngoingPanel;
    [SerializeField] private GameObject failPanel;
    [SerializeField] private GameObject successPanel;
    [SerializeField] private TMP_Text successMessageText;
    [SerializeField] private GameObject dayStartPanel;
    [SerializeField] private CanvasGroup dayStartCanvasGroup;

    private int _currentDay = 1;
    private int _maximumDays = 10;

    private HashSet<Sentences> completedTasks = new HashSet<Sentences>();
    private HashSet<Sentences> failedTasks = new HashSet<Sentences>();
    #endregion

    private void OnEnable()
    {
        GameEvents.OnTaskCompleted += CompleteTask;
        GameEvents.OnTaskFailed += FailTask;
    }

    private void OnDisable()
    {
        GameEvents.OnTaskCompleted -= CompleteTask;
        GameEvents.OnTaskFailed -= FailTask;
    }

    private void Start()
    {
        UpdateUI();
        StartCoroutine(ShowDayPanelRoutine());
    }

    #region Public Functions
    public void CompleteTask(Sentences task)
    {
        if (completedTasks.Contains(task)) return;

        completedTasks.Add(task);
        failedTasks.Remove(task);

        Debug.Log($"Task {task.name} completed!");
        UpdateUI();

        if (successPanel != null && successMessageText != null)
        {
            Debug.Log("Showing Success Panel");

            successPanel.SetActive(true);
            successMessageText.text = $"{completedTasks.Count} / {totalRequiredTasksPerDay}";
            StartCoroutine(HideSuccessPanelAfterDelay());
        }

        if (completedTasks.Count >= totalRequiredTasksPerDay)
        {
            Debug.Log("Day Completed!");
            AdvanceDay();
        }
    }


    public void FailTask(Sentences task)
    {
        if (completedTasks.Contains(task)) return;

        failedTasks.Add(task);
        Debug.Log($"Task {task.name} failed. retry");

        if (failPanel != null)
            failPanel.SetActive(true);

        StartCoroutine(HideFailAndTaskPanelAfterDelay());

        UpdateUI();
    }

    public bool CanAttemptTask(Sentences task)
    {
        return !completedTasks.Contains(task);
    }

    public void AdvanceDay()
    {
        _currentDay++;
        completedTasks.Clear();
        failedTasks.Clear();
        UpdateUI();

        GameEvents.OnNewDayStarted?.Invoke(_currentDay);

        Debug.Log($"moving to Dsy {_currentDay}");
    }
    #endregion

    #region Private Functions
    private IEnumerator HideFailAndTaskPanelAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        failPanel?.SetActive(false);
        taskOngoingPanel?.SetActive(false);
    }
    private IEnumerator HideSuccessPanelAfterDelay()
    {
        yield return new WaitForSeconds(2.5f);
        successPanel?.SetActive(false);
        taskOngoingPanel?.SetActive(false);
    }

    private IEnumerator ShowDayPanelRoutine()
    {
        GameEvents.OnTogglePlayerMovement?.Invoke(false);

        if (dayStartPanel != null)
            dayStartPanel.SetActive(true);

        if (dayStartCanvasGroup != null)
        {
            dayStartCanvasGroup.alpha = 1f;
        }

        yield return new WaitForSeconds(2f);

        float fadeDuration = 3f;
        float fadeTimer = 0f;

        while (fadeTimer < fadeDuration)
        {
            fadeTimer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);

            if (dayStartCanvasGroup != null)
                dayStartCanvasGroup.alpha = newAlpha;

            yield return null;
        }

        if (dayStartCanvasGroup != null)
            dayStartCanvasGroup.alpha = 0f;

        if (dayStartPanel != null)
            dayStartPanel.SetActive(false);

        GameEvents.OnTogglePlayerMovement?.Invoke(true);
    }


    private void UpdateUI()
    {
        if (tasksCompletedText != null)
        {
            tasksCompletedText.text =
                $"Tasks: {completedTasks.Count} / {totalRequiredTasksPerDay}";
        }

        if (dayTrackerText != null)
        {
            dayTrackerText.text = $"Day {Mathf.Clamp(_currentDay, 1, _maximumDays)}";
        }
    }
    #endregion
}