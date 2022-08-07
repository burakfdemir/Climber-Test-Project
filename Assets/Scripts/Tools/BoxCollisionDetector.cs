using UnityEngine;

namespace Tools
{
    public class BoxCollisionDetector
    {
        private LayerMask _detectionLayer;
        private BoxCollider _referenceCollider;
        private Transform _collisionCheckTransform;

        public BoxCollisionDetector(Transform collisionCheckTransform,LayerMask detectionLayer, BoxCollider referenceCollider)
        {
            _collisionCheckTransform = collisionCheckTransform; 
            _detectionLayer = detectionLayer;
            _referenceCollider = referenceCollider;
        }

        public Collider[] TryDetectCollisions()
        {
            return Physics.OverlapBox(_collisionCheckTransform.position + _referenceCollider.center,
                _collisionCheckTransform.TransformVector(_referenceCollider.size),
                _referenceCollider.transform.rotation,_detectionLayer);
        }
    }
}