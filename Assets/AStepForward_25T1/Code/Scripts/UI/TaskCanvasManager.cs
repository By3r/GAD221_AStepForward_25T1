using UnityEngine;

namespace UI
{
    public class TaskCanvasManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private GameObject nPCCanvas;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float lerpSpeed = 5f;
        [SerializeField] private float movementThreshold = 0.01f;

        private bool _isLerping = false;
        private bool _hasMatchedLastCameraDirection = false;
        private Vector3 _lastCameraPosition;
        private Quaternion _targetRotation;
        #endregion

        private void Start()
        {
            if (nPCCanvas == null || mainCamera == null) return;

            _lastCameraPosition = mainCamera.transform.position;
            _targetRotation = Quaternion.LookRotation(mainCamera.transform.forward); 
            nPCCanvas.SetActive(false);
        }

        private void Update()
        {
            if (nPCCanvas == null || mainCamera == null || !nPCCanvas.activeSelf) return;

            Vector3 currentCamPos = -mainCamera.transform.position;
            float distance = Vector3.Distance(currentCamPos, _lastCameraPosition);

            if (distance > movementThreshold)
            {
                _hasMatchedLastCameraDirection = false;
                _isLerping = false;
                _lastCameraPosition = currentCamPos;
            }
            else if (!_hasMatchedLastCameraDirection)
            {
                Vector3 forward = -mainCamera.transform.forward;
                _targetRotation = Quaternion.LookRotation(forward);
                _isLerping = true;
            }

            if (_isLerping)
            {
                nPCCanvas.transform.rotation = Quaternion.Lerp(nPCCanvas.transform.rotation, _targetRotation, Time.deltaTime * lerpSpeed);

                if (Quaternion.Angle(nPCCanvas.transform.rotation, _targetRotation) < 0.1f)
                {
                    _isLerping = false;
                    _hasMatchedLastCameraDirection = true;
                }
            }
        }
    }
}