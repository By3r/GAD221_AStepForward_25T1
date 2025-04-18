using System.Collections;
using UnityEngine;

public class TextSlider : MonoBehaviour
{
    #region Variables
    [Header("Path Points")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float yOffsetRange = 1f;
    [SerializeField] private float zOffsetRange = 0.5f;
    [SerializeField] private float resetDelay = 1f;

    private Vector3 _currentStart;
    private Vector3 _currentEnd;
    private Vector3 _direction;
    private bool _isMoving = true;
    #endregion

    void Start()
    {
        SetPathwithOffsets();
        transform.position = _currentStart;
    }

    void Update()
    {
        if (!_isMoving) return;

        transform.position += _direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, _currentEnd) < 0.1f)
        {
            StartCoroutine(ResetTextPosition());
        }
    }

    private void SetPathwithOffsets()
    {
        float randomYOffset = Random.Range(-yOffsetRange, yOffsetRange);
        float randomZOffset = Random.Range(-zOffsetRange, zOffsetRange);

        Vector3 offset = new Vector3(0f, randomYOffset, randomZOffset);
        _currentStart = startPoint.position + offset;
        _currentEnd = endPoint.position + offset;
        _direction = (_currentEnd - _currentStart).normalized;
    }

    private IEnumerator ResetTextPosition()
    {
        _isMoving = false;

        yield return new WaitForSeconds(resetDelay);

        SetPathwithOffsets();
        transform.position = _currentStart;

        _isMoving = true;
    }
}
