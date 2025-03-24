using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;

    private Vector3 _lastPlayerPosition;
    #endregion

    private void Start()
    {
        if (player != null)
        {
            _lastPlayerPosition = player.transform.position;
        }
    }

    private void LateUpdate()
    {
        if (player == null) return;

        if (player.transform.position != _lastPlayerPosition)
        {
            Vector3 targetPosition = new Vector3(
                player.transform.position.x + offset.x,
                player.transform.position.y + offset.y,
                player.transform.position.z + offset.z
            );

            transform.position = targetPosition;

            _lastPlayerPosition = player.transform.position;
        }
    }
}
