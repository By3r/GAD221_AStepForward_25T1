using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private NavMeshAgent navMeshAgent;
    #endregion

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MovePlayerIfDestinationIsValid();
        }
    }

    #region Private Functions
    private void MovePlayerIfDestinationIsValid()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            navMeshAgent.SetDestination(hit.point);
        }
    }
    #endregion
}
