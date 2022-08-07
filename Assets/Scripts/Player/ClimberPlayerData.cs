using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/BasicPlayerData", order = 0)]
    public class ClimberPlayerData : PlayerData
    {
        public float reachDistance;
    }
}
