using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ClimbItem
{
    public class ClimbTarget : MonoBehaviour
    {
        [SerializeField]private Transform leftSidePos;
        [SerializeField]private Transform rightSidePos;
        [SerializeField]private Transform middlePos;

        public Vector3 GetLeftMostPosition => leftSidePos.position;
        public Vector3 GetRightMostPosition => rightSidePos.position;
        public Vector3 GetMiddlePosition => middlePos.position;

        public Vector3 GetNearestTargetPosition(Vector3 basePosition)
        {
            var transformList = new List<Transform>() {leftSidePos, rightSidePos, middlePos};
            transformList = transformList.OrderBy((t) => Vector3.Distance(basePosition, t.position)).ToList();
            return transformList[0].position;
        }

    }
}