using Player;
using UnityEngine;

namespace ClimbItem
{
    public class LedgeClimbItem : MonoBehaviour, IClimbItem
    {
        public ClimbTarget ClimbTargetTransforms => climbTarget;
        public ClimbItemData ClimbItemData => climbItemData;

        [SerializeField] private SpringJoint connectibleJoint;
        [SerializeField] private ClimbTarget climbTarget;
        [SerializeField]private ClimbItemData climbItemData;
        
        
        public void InteractWithThePlayer(IClimberPlayer player)
        {
            connectibleJoint.connectedBody = player.InteractionBody;
        }

        public void ReleasePlayer(IClimberPlayer player)
        {
            connectibleJoint.connectedBody = null;
        }
    }
}