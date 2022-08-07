using System;
using System.Collections;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] private bool autoStart;
    [SerializeField] private float timeInterval;
    public event Action onCountDownStart;
    public event Action onCountDownEnd;

    private void Start()
    {
        if (autoStart) StartCoroutine(CountDownCor(timeInterval));
    }

    public void StartCountDown(float _timeInterval)
    {
        StartCoroutine(CountDownCor(_timeInterval));
    }

    private IEnumerator CountDownCor(float _timeInterval)
    {
        onCountDownStart?.Invoke();
        var wait = new WaitForSeconds(_timeInterval);
        yield return wait;
        onCountDownEnd?.Invoke();
    }
}