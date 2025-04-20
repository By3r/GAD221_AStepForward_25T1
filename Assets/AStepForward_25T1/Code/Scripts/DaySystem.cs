using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DaySystem : MonoBehaviour
{
    #region Variables
    public int totalRequiredTasksPerDay = 3;

    [SerializeField] private TMP_Text dayTrackerText;
    [SerializeField] private TMP_Text tasksCompletedText;
    [SerializeField] private GameObject taskOngoingPanel;
    [SerializeField] private GameObject failPanel;

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
    }

    #region Public Functison
    public void CompleteTask(Sentences task)
    {
        if (completedTasks.Contains(task)) return;

        completedTasks.Add(task);
        failedTasks.Remove(task);

        Debug.Log($"Task {task.name} completed!");
        UpdateUI();

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

    private void UpdateUI()
    {
        if (tasksCompletedText != null)
            tasksCompletedText.text = $"{completedTasks.Count} / {totalRequiredTasksPerDay}";

        if (dayTrackerText != null)
            dayTrackerText.text = $"Day {Mathf.Clamp(_currentDay, 1, _maximumDays)}";
    }
    #endregion
}