using Player;
using UnityEngine;

namespace ClimbItem
{
    public class SawClimbItem : MonoBehaviour,IClimbItem
    {
        private ClimbItemData _climbItemData;

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