using System;
using Player;
using UnityEngine;

namespace ClimbItem
{
    public class TimedLedgeClimbItem : LedgeClimbItem
    {
        [SerializeField] private CountDownTimer countDownTimer;

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
        }
    }
}