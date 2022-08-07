using System;
using System.Collections;
using Player;
using Tools;
using UnityEngine;

namespace ClimbItem
{
    public class SawClimbItem : MonoBehaviour,IClimbItem
    {
        [SerializeField] private SawClimbItemData climbItemData;
        [SerializeField] private ClimbTarget climbTarget;

        [SerializeField] private InfiniteRotator infiniteRotator;
        [SerializeField] private InfiniteTwoPointMover mover;
        [SerializeField] private PlayerCollisionDetectionBehaviour collisionDetectionBehaviour;
        public ClimbTarget ClimbTargetTransforms => climbTarget;
        ClimbItemData IClimbItem.ClimbItemData => climbItemData;

        private void OnEnable()
        {
            infiniteRotator.StartRotate(climbItemData.rotateSpeed);
            mover.Move(climbItemData.timeInterval);
            collisionDetectionBehaviour.OnPlayerDetected += PlayerHitHandler;
        }

        private void OnDisable()
        {
            collisionDetectionBehaviour.OnPlayerDetected -= PlayerHitHandler;
        }

        public void InteractWithThePlayer(IClimberPlayer player)
        {
            
        }

        public void ReleasePlayer(IClimberPlayer player)
        {
            
        }

        private void PlayerHitHandler(IClimberPlayer player)
        {
            player.FatalDamage();
        }
    }
}