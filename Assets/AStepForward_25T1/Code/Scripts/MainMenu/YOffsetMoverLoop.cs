using UnityEngine;

public class YOffsetMoverLoop : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform startPointTransform;
    [SerializeField] private Transform endPointTransform;
    [SerializeField] private float yOffset = 1.2f;
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _currentTarget;
    #endregion

    void Start()
    {
        _startPosition = new Vector3(
            startPointTransform.position.x,
            startPointTransform.position.y + yOffset,
            startPointTransform.position.z
        );

        _endPosition = new Vector3(
            endPointTransform.position.x,
            endPointTransform.position.y + yOffset,
            endPointTransform.position.z
        );

        transform.position = _startPosition;
        _currentTarget = _endPosition;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _currentTarget) < 0.01f)
        {
            if (_currentTarget == _endPosition)
            {
                transform.position = _startPosition;
                _currentTarget = _endPosition;
            }
        }
    }
}