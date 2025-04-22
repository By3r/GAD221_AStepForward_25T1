using UnityEngine;

public class TaskToTeleportActivator : MonoBehaviour
{
    #region Variables
    [SerializeField] private Sentences taskToWatch;
    [SerializeField] private GameObject targetObjectToBecomeTeleport;
    [SerializeField] private GameObject player;
    [SerializeField] private string teleportID;
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

        player = GameObject.FindGameObjectWithTag("Player");

        Invoke(nameof(UnlockAndTeleport), 2f);
    }

    private void UnlockAndTeleport()
    {
        targetObjectToBecomeTeleport.layer = LayerMask.NameToLayer("Teleport");

        var teleportScript = targetObjectToBecomeTeleport.GetComponent<TaskAsTeleport>();
        if (teleportScript != null)
        {
            teleportScript.EnableTeleportMode();
        }

        if (player != null && TeleportManager.Instance != null)
        {
            StartCoroutine(TeleportWithFade(player, teleportID));
        }
    }

    private System.Collections.IEnumerator TeleportWithFade(GameObject player, string teleportID)
    {
        yield return FadeManager.Instance.FadeOut();

        TeleportManager.Instance.TeleportPlayerTo(teleportID, player);

        yield return new WaitForSeconds(0.2f); 
        yield return FadeManager.Instance.FadeIn(); 
    }
}