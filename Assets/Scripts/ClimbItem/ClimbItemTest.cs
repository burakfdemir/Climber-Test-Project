using System;
using UnityEngine;

namespace Climbs
{
    public class ClimbItemTest : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        [SerializeField] private Transform holdTransform;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
        }

        public Rigidbody GetRb()
        {
            return _rigidbody;
        }

        public Transform GetHoldTransform()
        {
            return holdTransform;
        }
    }
}
