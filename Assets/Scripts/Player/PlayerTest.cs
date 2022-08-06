using System;
using System.Collections;
using System.Collections.Generic;
using Climbs;
using UnityEngine;
using Utilities;

namespace Player
{
    public class PlayerTest : MonoBehaviour
    {
        [SerializeField] private WallTest wallTest;
        [SerializeField] private Rigidbody playerKinematicRoot;
        [SerializeField] private Rigidbody rightHandBody;
        [SerializeField] private Rigidbody leftHandBody;
        [SerializeField] private ClimbItemTest holdItemTest;
        [SerializeField] private float distance;
        [SerializeField] private float force;
        [SerializeField] private Rigidbody hips;
        private Rigidbody[] _bodies;
        private ClimbItemTest _connectedClimbItemTest;

        private void Start()
        {
            _bodies = GetComponentsInChildren<Rigidbody>();
        }

        private float counter = 0f;
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.S)) return;
            
            if(counter == 0)
            {
                StartCoroutine(FixPlayer(holdItemTest,rightHandBody));
                counter++;
                return;
            }

            var newItem = wallTest.GetNearestClimbItem(holdItemTest);
            var hand = newItem.transform.position.x > holdItemTest.transform.position.x ? rightHandBody : leftHandBody;
            StartCoroutine(FixPlayer(newItem, hand));
        }

        private IEnumerator FixPlayer(ClimbItemTest climbItemTest, Rigidbody handBody)
        {
            var climbItemPos = climbItemTest.GetHoldTransform().position;
            var handPos = handBody.transform.position;
            var direction = climbItemPos - handPos;
            var wait = new WaitForEndOfFrame();
            var speed = force;

            var counter = 0f;
            while (Vector3.Distance(climbItemTest.transform.position, handBody.transform.position) > distance && counter < 2f)
            {
                handBody.AddForce(direction * (Time.fixedDeltaTime * speed));   
                direction = climbItemTest.transform.position - handBody.transform.position;
                counter += Time.fixedDeltaTime;
                yield return wait;
            }

            print("distance reached");
            UnConnectKinematicRoot();
            ConnectToClimbItem(climbItemTest,handBody);
        }

        private void UnConnectKinematicRoot()
        {
            var fixedJoint = GetComponentInChildren<FixedJoint>();
            //fixedJoint.connectedBody = null;
            Destroy(fixedJoint);
        }

        private void ConnectToClimbItem(ClimbItemTest itemTest, Rigidbody hand)
        {
            itemTest.gameObject.AddComponent<CharacterJoint>().connectedBody = hand;
        }
    }
}
