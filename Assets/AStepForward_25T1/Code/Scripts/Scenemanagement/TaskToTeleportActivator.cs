using UnityEngine;

public class TaskToTeleportActivator : MonoBehaviour
{
    #region Variables
    [SerializeField] private Sentences taskToWatch;
    [SerializeField] private GameObject targetObjectToBecomeTeleport;
    #endregion

    private void OnEnable()
    {
        GameEvents.OnTaskCompleted += HandleTaskCompleted;
    }

    private void OnDisable()
    {
        GameEvents.OnTaskCompleted -= HandleTaskCompleted;
    }

    private void HandleTaskCompleted(Sentences completedTask)
    {
        if (completedTask != taskToWatch || targetObjectToBecomeTeleport == null) return;

        Invoke("ChangeLayer", 2f);
    }

    private void ChangeLayer()
    {
        targetObjectToBecomeTeleport.layer = LayerMask.NameToLayer("Teleport"); var teleportScript = targetObjectToBecomeTeleport.GetComponent<TaskAsTeleport>();
        if (teleportScript != null)
        {
            teleportScript.EnableTeleportMode();
            Debug.Log($"'{targetObjectToBecomeTeleport.name}' is now a teleport point!");
        }
    }
}