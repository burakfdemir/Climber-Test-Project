using System;
using System.Collections;
using ClimbItem;
using UnityEngine;
using Utilities;

namespace Player
{
    public class BasicClimberPlayer : MonoBehaviour, IClimberPlayer
    {
        public event Action<IClimberPlayer> OnPlayerClimbed;
        public event Action<IClimberPlayer> OnPlayerCantReached;
        public Rigidbody InteractionBody { get; private set; }
        public CharacterJoint InteractionJoint { get; private set; }
        public Transform PlayerTransform => transform;

        [SerializeField]private Rigidbody rightHand;
        [SerializeField]private Rigidbody leftHand;
        [SerializeField]private CharacterJoint rightHandJoint;
        [SerializeField]private CharacterJoint leftHandJoint;
        [SerializeField]private ClimberPlayerData playerData;
        PlayerData IPlayer.PlayerData => playerData;

        private Vector3 _currentTargetPos;
        private bool _isPlayerKinematic = true;
        private SpringJoint _currentSpringJoint;
        private ClimbTarget _previousTarget;
        private ClimbTarget _currentTarget;

        private void Awake()
        {
            _isPlayerKinematic = true;
            SetPlayerKinematic(_isPlayerKinematic);
        }

        public void TryReachTarget(ClimbTarget target)
        {
            if(_isPlayerKinematic)
            {
                _isPlayerKinematic = false;
                SetPlayerKinematic(_isPlayerKinematic);
            }

            var playerPos = transform.position;

            if (Vector3.Distance(playerPos, target.GetNearestTargetPosition(playerPos)) > playerData.reachDistance)
            {
                InteractionBody = null;
                InteractionJoint = null;
                OnPlayerCantReached?.Invoke(this);
            }

            ReachTarget(target);
        }

        private void ReachTarget(ClimbTarget target)
        {
            var playerPos = transform.position;
            _currentTargetPos = target.GetNearestTargetPosition(playerPos);
            _currentTarget = target;
            StartCoroutine(ClimbPlayer());
        }

        private IEnumerator ClimbPlayer()
        {
            var currentHangItem = GetCurrentHangItems();
            var isTargetLeftSide = transform.position.x > _currentTargetPos.x;
            var isHangingWithLeftHand = currentHangItem.Item1 == leftHand;

            if (isHangingWithLeftHand && isTargetLeftSide)
            {
                // move right hand to the current item.
                yield return MoveRigidBodyToTarget(leftHand, _previousTarget.GetLeftMostPosition,
                    playerData.movementTimeInSameItem);
                Destroy(_currentSpringJoint.gameObject);
                yield return MoveRigidBodyToTarget(rightHand, _previousTarget.GetRightMostPosition,
                    playerData.movementTimeInSameItem);
                Destroy(_currentSpringJoint.gameObject);

                // move left hand to the target item.

                InteractionBody = leftHand;
                InteractionJoint = leftHandJoint;
                yield return MoveRigidBodyToTarget(InteractionBody, _currentTargetPos, playerData.movementTime);
                OnPlayerClimbed?.Invoke(this);
                Destroy(_currentSpringJoint.gameObject);
            }

            if (!isHangingWithLeftHand && isTargetLeftSide)
            {
                // move left hand to the target item.
                InteractionBody = leftHand;
                InteractionJoint = leftHandJoint;
                yield return MoveRigidBodyToTarget(InteractionBody, _currentTargetPos, playerData.movementTime);
                print("coroutine finished");
                OnPlayerClimbed?.Invoke(this);
                Destroy(_currentSpringJoint.gameObject);
            }

            if (isHangingWithLeftHand && !isTargetLeftSide)
            {
                //move right hand to the target item.
                InteractionBody = rightHand;
                InteractionJoint = rightHandJoint;
                yield return MoveRigidBodyToTarget(InteractionBody, _currentTargetPos, playerData.movementTime);
                print("coroutine finished");
                OnPlayerClimbed?.Invoke(this);
                Destroy(_currentSpringJoint.gameObject);
            }

            if (!isHangingWithLeftHand && !isTargetLeftSide)
            {
                //move left hand to current item.
                //move right hand to the target item.
            }

            _previousTarget = _currentTarget;
        }

        //Don't use speed, change it to the time.
        private IEnumerator MoveRigidBodyToTarget(Rigidbody body, Vector3 target,float movementTime)
        {
            var waitForFixUpdate = new WaitForFixedUpdate();

            var go = new GameObject("Spring Joint Target")
            {
                transform =
                {
                    position = body.transform.position
                }
            };
            var springJoint = go.AddComponent<SpringJoint>();
            _currentSpringJoint = springJoint;
            springJoint.connectedBody = body;
            springJoint.spring = 1000;
            springJoint.damper = 1000;
            springJoint.anchor = Vector3.zero;
            
            go.GetComponent<Rigidbody>().isKinematic = true;
            
            var elapsedTime = 0f;
            var pos = springJoint.transform.position;
            while (elapsedTime < movementTime)
            {
                var fixedDeltaTime = Time.fixedDeltaTime;
                springJoint.transform.position = Vector3.Lerp(pos, target, elapsedTime/movementTime);
                elapsedTime += fixedDeltaTime;
                yield return waitForFixUpdate;
            }

            //body.transform.position = target;
        }
        
        private (Rigidbody, CharacterJoint) GetCurrentHangItems()
        {
            return (InteractionBody, InteractionJoint);
        }

        [ContextMenu("Set Player UnKinematic")]
        public void SetPlayerUnKinematic()
        {
            SetPlayerKinematic(false);
        }
        
        private void SetPlayerKinematic(bool status)
        {
            var childBodies = GetComponentsInChildren<Rigidbody>();
            foreach (var body in childBodies)
            {
                body.isKinematic = status;
            }
        }

        private void OnDrawGizmos()
        {
            if(_currentTargetPos == null) return;
            Gizmos.DrawSphere(_currentTargetPos,.5f);
        }
    }
}