using System;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class SpringJointTest : MonoBehaviour
    {
        private GameObject _firstChain, _secondChain, _thirdChain;

        private void Start()
        {
            CreateSpringJoints();
        }

        private void CreateSpringJoints()
        {
            _firstChain = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _secondChain = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _thirdChain = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            _firstChain.transform.localScale = Vector3.one;
            _firstChain.transform.position = new Vector3(0f, 30f, 0f);
            _secondChain.transform.localScale = Vector3.one;
            _thirdChain.transform.localScale = Vector3.one;

            _secondChain.transform.position = new Vector3(0f, 20f, 0f);
            _thirdChain.transform.position = new Vector3(0f, 10f, 0f);

            var chains = new List<GameObject>() {_firstChain, _secondChain, _thirdChain};

            for (var index = 0; index < chains.Count; index++)
            {
                var chain = chains[index];
                chain.AddComponent<Rigidbody>();

                if (index != 0)
                    chain.AddComponent<SpringJoint>();

                chain.name = $"{index} chain";
            }

            _firstChain.GetComponent<Rigidbody>().isKinematic = true;
            _secondChain.GetComponent<SpringJoint>().connectedBody = _firstChain.GetComponent<Rigidbody>();
            _thirdChain.GetComponent<SpringJoint>().connectedBody = _secondChain.GetComponent<Rigidbody>();
        }

        [ContextMenu("Add Force")]
        private void AddForce()
        {
            _thirdChain.AddComponent<ConstantForce>().force = new Vector3(10f, 0f, 0f);
        }
    }
}
