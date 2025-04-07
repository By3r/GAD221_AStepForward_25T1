using UnityEngine;

namespace UI
{
    public class TaskCanvasManager : MonoBehaviour
    {
        #region 
        [SerializeField] private GameObject nPCCanvas;
        [SerializeField] private Camera mainCamera;

        private bool _isFacingCamera = false;
        #endregion

        private void Start()
        {
            if (nPCCanvas != null && mainCamera != null && !_isFacingCamera)
            {
                nPCCanvas.transform.LookAt(mainCamera.transform);
                nPCCanvas.transform.rotation = Quaternion.LookRotation(nPCCanvas.transform.forward);
                _isFacingCamera = false;
            }

            nPCCanvas.SetActive(false);
        }
    }
}