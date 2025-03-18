using UnityEngine;

public class TaskInteractions : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject taskDifficultyPanel;
    private bool canDoTask = false;
    #endregion

    void Update()
    {
        if (taskDifficultyPanel.activeSelf != canDoTask)
        {
            taskDifficultyPanel.SetActive(canDoTask);
        }
    }

    private void OnMouseEnter()
    {
        canDoTask = true;
    }

    private void OnMouseExit()
    {
        StopTask();
    }

    #region Public Functions
    public void StopTask()
    {
        canDoTask = false;
    }
    #endregion
}
