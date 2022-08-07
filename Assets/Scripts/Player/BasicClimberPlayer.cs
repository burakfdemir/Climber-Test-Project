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
        public event Action OnFatalDamage;
        public Rigidbody InteractionBody { get; private set; }
        public CharacterJoint InteractionJoint { get; private set; }
        public Transform PlayerTransform => transform;

        [SerializeField] private Rigidbody rightHand;
        [SerializeField] private Rigidbody leftHand;
        [SerializeField] private CharacterJoint rightHandJoint;
        [SerializeField] private CharacterJoint leftHandJoint;
        [SerializeField] private ClimberPlayerData playerData;
        [SerializeField] private RigidBodySpringJointMover rigidBodyMover;
        [SerializeField] private Transform headTransform;
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

        public void TryReachTarget(IClimbItem climbItem)
        {
            var target = climbItem.ClimbTargetTransforms; 
            if (_isPlayerKinematic)
            {
                _isPlayerKinematic = false;
                SetPlayerKinematic(_isPlayerKinematic);
            }

            var playerPos = headTransform.position;

            if (Vector3.Distance(playerPos, target.GetNearestTargetPosition(playerPos)) > playerData.reachDistance)
            {
                InteractionBody = null;
                InteractionJoint = null;
                OnPlayerCantReached?.Invoke(this);
                return;
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
            var currentHangItems = GetCurrentHangItems();
            var isTargetLeftSide = transform.position.x > _currentTargetPos.x;

            if (currentHangItems.Item1 == null)
            {
                if (isTargetLeftSide)
                    SetInteractionSideLeft();
                else
                    SetInteractionSideRight();
                
            }
            else
            {
                var isCurrentSideLeft = currentHangItems.Item1 == leftHand;
                
                if (isCurrentSideLeft)
                    SetInteractionSideRight();
                else
                    SetInteractionSideLeft();
            }
            yield return rigidBodyMover.MoveRigidBody(InteractionBody, _currentTargetPos,
                playerData.movementTime,InteractionBody == rightHand);
            OnPlayerClimbed?.Invoke(this);
            
            _previousTarget = _currentTarget;

        }

        private void SetInteractionSideLeft()
        {
            InteractionBody = leftHand;
            InteractionJoint = leftHandJoint;
        }

        private void SetInteractionSideRight()
        {
            InteractionBody = rightHand;
            InteractionJoint = rightHandJoint;
        }

        private (Rigidbody, CharacterJoint) GetCurrentHangItems()
        {
            return (InteractionBody, InteractionJoint);
        }

        private void SetPlayerKinematic(bool status)
        {
            var childBodies = GetComponentsInChildren<Rigidbody>();
            foreach (var body in childBodies)
            {
                body.isKinematic = status;
            }
        }

        public void FatalDamage()
        {
            OnFatalDamage?.Invoke();
        }

        private void OnDrawGizmos()
        {
            if (_currentTargetPos.IsApproximately(Vector3.zero)) return;
            Gizmos.DrawSphere(_currentTargetPos, .1f);
        }
    }
}