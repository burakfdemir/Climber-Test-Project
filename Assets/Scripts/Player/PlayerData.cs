using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 0)]
    public class PlayerData : ScriptableObject
    {
        public float movementTime;
        //public float weight;

        // public float GetWeightInNewton()
        // {
        //     return weight * 9.81f;
        // }
    }
}
