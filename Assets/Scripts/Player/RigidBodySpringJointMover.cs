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
            _springJoint.spring = 1000;
            _springJoint.damper = 1000;
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.anchor = Vector3.zero;
            _rigidbody = gameObject.GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
        }
        
        public IEnumerator MoveRigidBodyToTarget(Rigidbody body, Vector3 target,float movementTime)
        {
            var waitForFixUpdate = new WaitForFixedUpdate();
            
            var elapsedTime = 0f;
            _springJoint.connectedBody = body;
            var springJointTransform = _springJoint.transform;
            springJointTransform.position = body.transform.position;

            var pos = springJointTransform.position;
            while (elapsedTime < movementTime)
            {
                var fixedDeltaTime = Time.fixedDeltaTime;
                var nextPos = Vector3.Lerp(pos, target, elapsedTime/movementTime);
                _rigidbody.MovePosition(nextPos);
                elapsedTime += fixedDeltaTime;
                yield return waitForFixUpdate;
            }

            _springJoint.connectedBody = null;
        }
    }
}