using System;
using ClimbItem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ClickInfo
{
    public class ClimbItemClickInfoProvider : MonoBehaviour, IClickInfoProvider<IClimbItem>
    {
        public IClimbItem Item { get; private set; }
        public static event Action<IClimbItem> OnItemClick; 

        private void Awake()
        {
            Item = GetComponent<IClimbItem>();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnItemClick?.Invoke(Item);
            print("on click");
        }
    }
}