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
        [SerializeField] private float forwardHeadForce;
        [SerializeField] private float armForce;
        [SerializeField] private float armForceStartAngle;
        [SerializeField] private Rigidbody ribs;

        private void FixedUpdate()
        {
            head.AddForce(Vector3.up * forwardHeadForce);
            var rotation = ragdollRibs.localRotation.eulerAngles;

            print($"rotation value : {rotation.x}");
            
            if (Mathf.Abs(rotation.x) <= armForceStartAngle) return;
            
            if (rotation.x < 0)
            {
                print($"adding force to right {rotation.y}");
                rightArm.AddForce(Vector3.right * armForce);
            }
            else
            {
                print($"adding force to left {rotation.y}");
                leftArm.AddForce(Vector3.left * armForce);
            }
        }
    }
}