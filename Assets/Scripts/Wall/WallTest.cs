using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Climbs
{
    public class WallTest : MonoBehaviour
    {
        private ClimbItemTest[] _climbItems;
        private ClimbItemTest _current;
        private int _itemIndex;

        private void Awake()
        {
            _climbItems = GetComponentsInChildren<ClimbItemTest>();
        }

        public Vector3 GetNextClimItem()
        {
            // if (_current == null)
            // {
            //     _current = _climbItems[0];
            //     return _current.transform.position;
            // }

            _current = _climbItems[_itemIndex++];
            return _current.transform.position;
        }

        public ClimbItemTest GetNearestClimbItem(ClimbItemTest currentItemTest)
        {
            var sortedItems =
                _climbItems.OrderBy(g => Vector3.Distance(g.transform.position, currentItemTest.transform.position)).ToList();
            return sortedItems[1];
        }

    }
}
