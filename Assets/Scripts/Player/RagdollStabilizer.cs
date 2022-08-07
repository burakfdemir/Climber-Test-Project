using System;
using UnityEngine;

namespace Player
{
    public class RagdollStabilizer : MonoBehaviour
    {
        [SerializeField] private Transform ragdollRibs;
        [SerializeField] private Rigidbody head;
        [SerializeField] private Rigidbody rightArm;
        [SerializeField] private Rigidbody leftArm;
        [SerializeField] private float headForce;
        [SerializeField] private float armForce;
        [SerializeField] private float armForceStartAngle;

        private void FixedUpdate()
        {
            head.AddForce(Vector3.up * headForce);
            var rotation = ragdollRibs.localRotation.eulerAngles;

            
            if (Mathf.Abs(rotation.x) <= armForceStartAngle) return;
            
            if (rotation.x < 0)
            {
                rightArm.AddForce(Vector3.right * armForce);
            }
            else
            {
                leftArm.AddForce(Vector3.left * armForce);
            }
        }
    }
}