using System;
using Player;
using UnityEngine;

namespace ClimbItem
{
    public interface IClimbItem
    {
        ClimbTarget ClimbTargetTransforms { get; }
        ClimbItemData ClimbItemData { get; }
        void InteractWithThePlayer(IClimberPlayer player);
        void ReleasePlayer(IClimberPlayer player);
    }
}
