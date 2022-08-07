using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class RigidBodySpringJointMover : MonoBehaviour
    {
        private SpringJoint _springJoint;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            CreateSpringJoint();
        }

        private void CreateSpringJoint()
        {
            _springJoint = gameObject.AddComponent<SpringJoint>();
            _springJoint.spring = 2000;
            _springJoint.damper = 0;
            _springJoint.maxDistance = 0f;
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.anchor = Vector3.zero;
            _rigidbody = gameObject.GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
        }

        public IEnumerator MoveRigidBody(Rigidbody body, Vector3 target, float movementTime, bool toRightWay)
        {
            _springJoint.connectedBody = body;
            var springJointTransform = _springJoint.transform;
            springJointTransform.position = body.transform.position;

            var pos = springJointTransform.position;

            var distance = Vector3.Distance(target,pos);
            var distanceDifference = distance * 0.5f;
            
            var firstReachPoint = new Vector3(pos.x + (toRightWay ? distanceDifference: -distanceDifference),
                pos.y + distanceDifference, pos.z - distanceDifference);
            //
            // yield return MoveRigidbodyLinear(body, firstReachPoint, movementTime * 0.33f);
            //
            // var secondReachPoint = new Vector3(firstReachPoint.x, target.y + distanceDifference, firstReachPoint.z);
            // yield return MoveRigidbodyLinear(body, secondReachPoint, movementTime * 0.33f);
            //
            // yield return MoveRigidbodyLinear(body, target, movementTime * 0.5f);
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