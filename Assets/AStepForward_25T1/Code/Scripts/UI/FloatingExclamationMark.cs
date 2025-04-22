using UnityEngine;

public class FloatingExclamationMark : MonoBehaviour
{
    #region Variables
    [SerializeField] private float floatSpeed = 2f;       
    [SerializeField] private float floatAmp = 0.25f; 

    private Vector3 _startPosition;
    #endregion

    private void Start()
    {
        _startPosition = transform.localPosition;
    }

    private void Update()
    {
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatAmp;
        transform.localPosition = _startPosition + new Vector3(0f, newY, 0f);
    }
}