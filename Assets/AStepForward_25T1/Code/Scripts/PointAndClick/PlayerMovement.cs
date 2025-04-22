using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;

    private bool _uiLocked;
    private bool _movementLocked = false;
    #endregion

    private void Awake()
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.updateRotation = false; 
        }
    }

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
        _uiLocked = UIManager.Instance != null && UIManager.Instance.IsAnyUIOpen;
        bool isLocked = _movementLocked || _uiLocked;

        if (isLocked)
        {
            navMeshAgent.ResetPath();
            UpdateAnimation(0f);
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            MovePlayerIfDestinationIsValid();
        }

        UpdateAnimation(navMeshAgent.velocity.magnitude);
        UpdateRotation();
    }

    private void MovePlayerIfDestinationIsValid()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            navMeshAgent.SetDestination(hit.point);
        }
    }

    private void UpdateAnimation(float speed)
    {
        if (animator == null) return;
        animator.SetBool("IsWalking", speed > 0.1f);
    }

    private void UpdateRotation()
    {
        if (navMeshAgent.velocity.sqrMagnitude > 0.1f)
        {
            Vector3 direction = navMeshAgent.velocity.normalized;
            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10f * Time.deltaTime);
            }
        }
    }
}