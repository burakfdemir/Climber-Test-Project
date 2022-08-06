using ClimbItem;
using UnityEngine.EventSystems;

namespace ClickInfo
{
    public interface IClickInfoProvider<out T> : IPointerClickHandler where T : IClimbItem
    {
        T Item { get; }
    }
}