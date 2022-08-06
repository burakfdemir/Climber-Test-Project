using Player;
using UnityEngine;

namespace ClimbItem
{
    public class TimedLedgeClimbItem : MonoBehaviour, IClimbItem
    {
        private TimedClimbItemData _climbItemData;

        public ClimbTarget ClimbTargetTransforms { get; }
        ClimbItemData IClimbItem.ClimbItemData => _climbItemData;

        public void InteractWithThePlayer(IClimberPlayer player)
        {
            
        }

        public void ReleasePlayer(IClimberPlayer player)
        {
            
        }
    }
}