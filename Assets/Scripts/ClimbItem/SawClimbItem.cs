using System;
using System.Collections;
using Player;
using UnityEngine;

namespace ClimbItem
{
    public class SawClimbItem : MonoBehaviour,IClimbItem
    {
        [SerializeField] private SawClimbItemData climbItemData;
        [SerializeField] private ClimbTarget climbTarget;

        [SerializeField] private InfiniteRotator infiniteRotator;
        [SerializeField] private InfiniteTwoPointMover mover;
        public ClimbTarget ClimbTargetTransforms => climbTarget;
        ClimbItemData IClimbItem.ClimbItemData => climbItemData;

        private void OnEnable()
        {
            infiniteRotator.StartRotate(climbItemData.rotateSpeed);
            mover.Move(climbItemData.timeInterval);
        }

        public void InteractWithThePlayer(IClimberPlayer player)
        {
            
        }

        public void ReleasePlayer(IClimberPlayer player)
        {
            
        }
    }
}