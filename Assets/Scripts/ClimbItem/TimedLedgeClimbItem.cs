using System;
using Player;
using Tools;
using UnityEngine;

namespace ClimbItem
{
    public class TimedLedgeClimbItem : MonoBehaviour, IClimbItem
    {
        [SerializeField] private CountDownTimer countDownTimer;
        [SerializeField] private ClimbTarget climbTarget;
        [SerializeField] private TimedClimbItemData climbItemData;
        [SerializeField] private SpringJoint connectibleJoint;
        [SerializeField] private Rigidbody climbItemBody;

        public ClimbTarget ClimbTargetTransforms => climbTarget;
        public ClimbItemData ClimbItemData => climbItemData;
        private void Start()
        {
            countDownTimer.onCountDownEnd += TimeEndHandler;
        }

        private void OnDestroy()
        {
            countDownTimer.onCountDownEnd -= TimeEndHandler;
        }

        private void TimeEndHandler()
        {
            ReleasePlayer(null);
            climbItemBody.isKinematic = false;
            Destroy(connectibleJoint);
        }
        
        public void InteractWithThePlayer(IClimberPlayer player)
        {
            connectibleJoint.connectedBody = player.InteractionBody;
            countDownTimer.StartCountDown(climbItemData.timeInterval);
        }

        public void ReleasePlayer(IClimberPlayer player)
        {
            connectibleJoint.connectedBody = null;
        }
    }
}