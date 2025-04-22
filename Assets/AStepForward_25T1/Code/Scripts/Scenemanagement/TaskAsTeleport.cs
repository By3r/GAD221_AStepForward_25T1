using UnityEngine;

public class TaskAsTeleport : MonoBehaviour
{
    [SerializeField] private string teleportID;
    private bool _canTeleport = false;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void EnableTeleportMode()
    {
        _canTeleport = true;
    }

    private void OnMouseDown()
    {
        if (!_canTeleport)
        {
            return;
        }

        if (TeleportManager.Instance != null)
        {
            TeleportManager.Instance.TeleportPlayerTo(teleportID, _player);
        }
    }
}