using System;
using Player;
using Tools;
using UnityEngine;

namespace ClimbItem
{
    public class PlayerCollisionDetectionBehaviour: MonoBehaviour
    {
        [SerializeField] private LayerMask detectionLayer;
        [SerializeField] private BoxCollider referenceCollider;
        private BoxCollisionDetector _collisionDetector;
        public event Action<IClimberPlayer> OnPlayerDetected;

        private void Start()
        {
            _collisionDetector = new BoxCollisionDetector(transform, detectionLayer, referenceCollider);
        }

        private void FixedUpdate()
        {
            var colliders = _collisionDetector.TryDetectCollisions();
            if(colliders.Length == 0) return;

            foreach (var col in colliders)
            {
                var playerComponent = col.gameObject.GetComponentInParent<IClimberPlayer>();
                if(playerComponent == null) continue;
                OnPlayerDetected?.Invoke(playerComponent);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position,transform.TransformVector(referenceCollider.size));
        }
    }
}