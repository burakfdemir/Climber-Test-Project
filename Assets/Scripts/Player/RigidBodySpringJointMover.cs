using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class RigidBodySpringJointMover : MonoBehaviour
    {
        private SpringJoint _springJoint;
        private Rigidbody _rigidbody;
        [SerializeField] private float spring = 2000f;
        [SerializeField] private float damper = 0f;
        [SerializeField] private float maxDistance = 0f;
        [SerializeField] private bool autoConfigureConnectedAnchor = false;
        [SerializeField] private Vector3 anchor = Vector3.zero;

        private void Awake()
        {
            CreateSpringJoint();
        }

        private void CreateSpringJoint()
        {
            _springJoint = gameObject.AddComponent<SpringJoint>();
            _springJoint.spring = spring;
            _springJoint.damper = damper;
            _springJoint.maxDistance = maxDistance;
            _springJoint.autoConfigureConnectedAnchor = autoConfigureConnectedAnchor;
            _springJoint.anchor = anchor;
            _rigidbody = gameObject.GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
        }

        public IEnumerator MoveRigidBody(Rigidbody body, Vector3 target, float movementTime, bool toRightWay)
        {
            _springJoint.connectedBody = body;
            var springJointTransform = _springJoint.transform;
            springJointTransform.position = body.transform.position;
            
            yield return MoveRigidbodyLinear(body, target, movementTime);

            _springJoint.connectedBody = null;
        }

        private IEnumerator MoveRigidbodyLinear(Rigidbody body, Vector3 targetPos, float movementTime)
        {
            var elapsedTime = 0f;
            var wait = new WaitForFixedUpdate();
            
            var firstPos = body.position;
            while (elapsedTime < movementTime)
            {
                var nextPos = Vector3.Lerp(firstPos, targetPos, elapsedTime / movementTime);
                _rigidbody.MovePosition(nextPos);
                elapsedTime += Time.fixedDeltaTime;
                yield return wait;
            }
            
            body.MovePosition(targetPos);
        }

        private IEnumerator MoveRigidbodySpherically(Rigidbody body, Vector3 targetPos, float movementTime)
        {
            var elapsedTime = 0f;
            var wait = new WaitForFixedUpdate();
            
            var firstPos = body.position;
            while (elapsedTime < movementTime)
            {
                var nextPos = Vector3.Slerp(firstPos, targetPos, elapsedTime / movementTime);
                elapsedTime += Time.fixedDeltaTime;
                yield return wait;
            }
            
            body.MovePosition(targetPos);
        }

        private void OnDrawGizmos()
        {
            if(_springJoint == null || _springJoint.connectedBody == null) return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_springJoint.transform.position,.1f);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(_springJoint.connectedBody.position,_springJoint.transform.position);
        }
    }
}