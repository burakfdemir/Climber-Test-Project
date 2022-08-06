using UnityEngine;

namespace Player
{
    public interface IPlayer
    {
        protected PlayerData PlayerData { get;}
        Transform PlayerTransform { get; }
    }
}
