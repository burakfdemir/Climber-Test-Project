using UnityEngine;

namespace CameraFollow
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float followDistance = 10f;
        void LateUpdate()
        {
            var targetPos = target.position;
            transform.position = new Vector3(targetPos.x, targetPos.y + 1f, targetPos.z - followDistance);
        }
    }
}
