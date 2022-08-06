using System;
using ClimbItem;
using UnityEngine;

namespace Player
{
    public interface IClimberPlayer : IPlayer
    {
        event Action<IClimberPlayer> OnPlayerClimbed;
        event Action<IClimberPlayer> OnPlayerCantReached;
        Rigidbody InteractionBody { get;}
        CharacterJoint InteractionJoint { get; }
        void TryReachTarget(ClimbTarget target);
    }
}