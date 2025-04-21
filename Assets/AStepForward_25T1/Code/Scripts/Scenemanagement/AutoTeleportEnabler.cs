using UnityEngine;

public class AutoTeleportEnabler : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TaskAsTeleport>()?.EnableTeleportMode();
    }
}