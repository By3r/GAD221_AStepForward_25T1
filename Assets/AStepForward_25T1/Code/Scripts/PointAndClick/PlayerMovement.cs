using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator; 

    private bool _movementLocked = false;
    #endregion

    private void OnEnable()
    {
        GameEvents.OnTaskStarted += _ => _movementLocked = true;
        GameEvents.OnTaskCompleted += _ => _movementLocked = false;
        GameEvents.OnTaskFailed += _ => _movementLocked = false;
    }

    private void OnDisable()
    {
        GameEvents.OnTaskStarted -= _ => _movementLocked = true;
        GameEvents.OnTaskCompleted -= _ => _movementLocked = false;
        GameEvents.OnTaskFailed -= _ => _movementLocked = false;
    }

    private void Update()
    {
        if (_movementLocked) return;

        if (Input.GetMouseButtonDown(0))
        {
            MovePlayerIfDestinationIsValid();
        }

        UpdateAnimation();
    }

    private void MovePlayerIfDestinationIsValid()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            navMeshAgent.SetDestination(hit.point);
        }
    }

    private void UpdateAnimation()
    {
        if (animator == null) return;

        float speed = navMeshAgent.velocity.magnitude;

        animator.SetBool("IsWalking", speed > 0.1f); 
    }
}