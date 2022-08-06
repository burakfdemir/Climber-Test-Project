using System;
using UnityEngine;

public class RigidbodyTest : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;

    [SerializeField] private bool _isForceEnabled;
    [SerializeField] private float speed;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        if (!_isForceEnabled) return;

        var currentPos = _rigidbody.position;
        var targetPos = new Vector3(currentPos.x, currentPos.y + speed * Time.fixedDeltaTime, currentPos.z);
        _rigidbody.AddForce(targetPos);
    }

    [ContextMenu("UnSet Kinematic All Rigidbodies")]
    private void UnSetAllKinematic()
    {
        var rigidbodies = FindObjectsOfType<Rigidbody>();
        foreach (var rigidBody in rigidbodies)
        {
            rigidBody.isKinematic = false;
        }
    }
}
