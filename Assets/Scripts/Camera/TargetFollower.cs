using UnityEngine;

namespace CameraFollow
{
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 followDistance;
        void LateUpdate()
        {
            var targetPos = target.position;
            transform.position = targetPos + followDistance;
        }
    }
}
