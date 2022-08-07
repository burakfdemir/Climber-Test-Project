using UnityEngine;

namespace ClimbItem
{
    [CreateAssetMenu(fileName = "TimedLedgeClimbItemData", menuName = "ScriptableObjects/TimedLedgeClimbItemData", order = 0)]
    public class TimedClimbItemData : ClimbItemData
    {
        public float timeInterval;
    }
}