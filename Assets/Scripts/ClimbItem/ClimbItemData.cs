using UnityEngine;

namespace ClimbItem
{
    [CreateAssetMenu(fileName = "ClimbItemData", menuName = "ScriptableObjects/ClimbItemData", order = 0)]
    public class ClimbItemData : ScriptableObject
    {
        public string Name;
    }
}