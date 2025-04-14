using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DaySystem : MonoBehaviour
{
    #region Variables
    public int totalRequiredTasks = 3;

    [SerializeField] private TMP_Text dayTrackerText;
    [SerializeField] private TMP_Text tasksCompletedText;
    [SerializeField] private GameObject failPanel;

    private int _currentDay = 1;
    private int _maximumDays = 30;

    private HashSet<Sentences> completedTasks = new HashSet<Sentences>();
    private HashSet<Sentences> failedTasks = new HashSet<Sentences>();
    #endregion

    private void Start()
    {
        UpdateUI();
    }

    #region Public Functions
    public void CompleteTask(Sentences task)
    {
        if (!completedTasks.Contains(task))
        {
            completedTasks.Add(task);
            failedTasks.Remove(task);

            Debug.Log($"Task \"{task.name}\" completed!");
            UpdateUI();

            if (completedTasks.Count >= totalRequiredTasks)
            {
                Debug.Log("Day Completed!");
                AdvanceDay();
            }
        }
    }

    public void FailTask(Sentences task, GameObject taskOngoingPanel = null)
    {
        if (!completedTasks.Contains(task))
        {
            failedTasks.Add(task);
            Debug.Log($"Task \"{task.name}\" failed. Can retry.");

            if (failPanel != null)
                failPanel.SetActive(true);

            if (taskOngoingPanel != null)
                StartCoroutine(TurnOffPanelsDelayed(taskOngoingPanel));

            UpdateUI();
        }
    }

    private IEnumerator TurnOffPanelsDelayed(GameObject panel)
    {
        yield return new WaitForSeconds(3f);
        if (failPanel != null) failPanel.SetActive(false);
        if (panel != null) panel.SetActive(false);
    }

    public void AdvanceDay()
    {
        if (completedTasks.Count >= totalRequiredTasks)
        {
            _currentDay++;
            completedTasks.Clear();
            failedTasks.Clear();
            UpdateUI();
            Debug.Log($"Moving to Day {_currentDay}");
        }
    }

    public bool CanAttemptTask(Sentences task)
    {
        return !completedTasks.Contains(task);
    }
    #endregion

    private void UpdateUI()
    {
        if (tasksCompletedText != null)
            tasksCompletedText.text = $"{completedTasks.Count} / {totalRequiredTasks}";

        if (dayTrackerText != null)
            dayTrackerText.text = $"Day {Mathf.Clamp(_currentDay, 1, _maximumDays)}";
    }
}
