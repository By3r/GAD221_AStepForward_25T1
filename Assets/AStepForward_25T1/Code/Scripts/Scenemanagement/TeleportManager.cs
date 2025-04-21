using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class TeleportManager : MonoBehaviour
{
    #region Variables
    public static TeleportManager Instance;

    private Dictionary<string, TeleportPoint> teleportPoints = new Dictionary<string, TeleportPoint>();
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            RegisterAllTeleportPoints();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void RegisterAllTeleportPoints()
    {
        teleportPoints.Clear();
        var points = FindObjectsOfType<TeleportPoint>();
        foreach (var point in points)
        {
            if (!teleportPoints.ContainsKey(point.TeleportID))
            {
                teleportPoints.Add(point.TeleportID, point);
            }
        }
    }

    public void TeleportPlayerTo(string teleportID, GameObject player)
    {
        if (!teleportPoints.ContainsKey(teleportID))
            return;

        var point = teleportPoints[teleportID];

        if (!point.IsUnlocked || player == null)
            return;

        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.Warp(point.Destination.position);
        }
    }

    public bool IsTeleportUnlocked(string teleportID)
    {
        return teleportPoints.ContainsKey(teleportID) && teleportPoints[teleportID].IsUnlocked;
    }
}