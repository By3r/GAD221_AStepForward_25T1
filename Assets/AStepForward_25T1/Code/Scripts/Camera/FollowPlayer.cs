using UnityEngine;

namespace MainCam
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private float followSpeed = 5f;
        [SerializeField] private Vector3 offset;

        private Vector3 velocity = Vector3.zero;

        void Start()
        {
            if (player != null) offset = transform.position - player.transform.position;
        }

        void LateUpdate()
        {
            if (player == null) return;

            Vector3 targetPosition = player.transform.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1f / followSpeed);
        }
    }
}