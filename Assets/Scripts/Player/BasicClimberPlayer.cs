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
        [SerializeField] private RigidBodySpringJointMover rigidBodyMover;
        PlayerData IPlayer.PlayerData => playerData;

        private Vector3 _currentTargetPos;
        private bool _isPlayerKinematic = true;
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
                yield return rigidBodyMover.MoveRigidBodyToTarget(leftHand, _previousTarget.GetLeftMostPosition,
                    playerData.movementTimeInSameItem);
                yield return rigidBodyMover.MoveRigidBodyToTarget(rightHand, _previousTarget.GetRightMostPosition,
                    playerData.movementTimeInSameItem);

                // move left hand to the target item.

                InteractionBody = leftHand;
                InteractionJoint = leftHandJoint;
                yield return rigidBodyMover.MoveRigidBodyToTarget(InteractionBody, _currentTargetPos, playerData.movementTime);
                OnPlayerClimbed?.Invoke(this);
            }

            if (!isHangingWithLeftHand && isTargetLeftSide)
            {
                // move left hand to the target item.
                InteractionBody = leftHand;
                InteractionJoint = leftHandJoint;
                yield return rigidBodyMover.MoveRigidBodyToTarget(InteractionBody, _currentTargetPos, playerData.movementTime);
                print("coroutine finished");
                OnPlayerClimbed?.Invoke(this);
            }

            if (isHangingWithLeftHand && !isTargetLeftSide)
            {
                //move right hand to the target item.
                InteractionBody = rightHand;
                InteractionJoint = rightHandJoint;
                yield return rigidBodyMover.MoveRigidBodyToTarget(InteractionBody, _currentTargetPos, playerData.movementTime);
                print("coroutine finished");
                OnPlayerClimbed?.Invoke(this);
            }

            if (!isHangingWithLeftHand && !isTargetLeftSide)
            {
                //move left hand to current item.
                //move right hand to the target item.
            }

            _previousTarget = _currentTarget;
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
            if(_currentTargetPos.IsApproximately(Vector3.zero)) return;
            Gizmos.DrawSphere(_currentTargetPos,.5f);
        }
    }
}