using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    [SerializeField] private string teleportID;
    public string TeleportID => teleportID;

    [SerializeField] private Transform teleportDestination;
    public Transform Destination => teleportDestination;

    [SerializeField] private bool unlockedAtStart = false; 

    private bool _isUnlocked;
    public bool IsUnlocked => _isUnlocked;

    private void Awake()
    {
        if (unlockedAtStart)
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        if (_isUnlocked) return;

        _isUnlocked = true;
    }
}