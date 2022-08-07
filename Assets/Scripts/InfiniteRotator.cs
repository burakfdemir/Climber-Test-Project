using System;
using System.Collections;
using UnityEngine;

public class InfiniteRotator : MonoBehaviour
{
    [SerializeField] private bool autoStart = true;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private RotateDirection direction;

    private void Start()
    {
        if (autoStart) StartCoroutine(RotateCor(rotateSpeed));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void StartRotate(float _rotateSpeed)
    {
        StartCoroutine(RotateCor(_rotateSpeed));
    }

    private IEnumerator RotateCor(float _rotateSpeed)
    {
        var wait = new WaitForEndOfFrame();
        var directionVector = GetDirectionVector(direction);
        
        while (true)
        {
            gameObject.transform.Rotate(directionVector,_rotateSpeed * Time.deltaTime);
            yield return wait;
        }
    }

    private Vector3 GetDirectionVector(RotateDirection rotateDirection)
    {
        return rotateDirection switch
        {
            RotateDirection.Up => Vector3.up,
            RotateDirection.Down => Vector3.down,
            RotateDirection.Right => Vector3.right,
            RotateDirection.Left => Vector3.left,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public enum RotateDirection
    {
        Up, Down,Right,Left
    }
}