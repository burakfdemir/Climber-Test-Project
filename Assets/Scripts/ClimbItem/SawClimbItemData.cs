using UnityEngine;

namespace ClimbItem
{
    [CreateAssetMenu(fileName = "SawClimbItemData", menuName = "ScriptableObjects/SawClimbItemData", order = 0)]

    public class SawClimbItemData : TimedClimbItemData
    {
        public float rotateSpeed;
    }
}