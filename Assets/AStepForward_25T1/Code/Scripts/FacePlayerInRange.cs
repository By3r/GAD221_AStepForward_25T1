using UnityEngine;

public class FacePlayerInRange : MonoBehaviour
{
    #region Variables
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private Transform player;
    #endregion

    private Quaternion _defaultRotation;

    private void Start()
    {
        _defaultRotation = transform.rotation;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            RotateTowards(player.position);
        }
        else
        {
            RotateTowards(transform.position + _defaultRotation * Vector3.forward);
        }
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
