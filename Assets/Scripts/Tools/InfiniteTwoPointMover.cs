using System.Collections;
using UnityEngine;

namespace Tools
{
    public class InfiniteTwoPointMover : MonoBehaviour
    {
        [SerializeField] private Transform firstTransform;
        [SerializeField] private Transform secondTransform;
        [SerializeField] private float timeInterval;
        [SerializeField] private bool autoStart;

        private void Start()
        {
            if (autoStart) StartCoroutine(MoveCor(timeInterval));
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public void Move(float _timeInterval) => StartCoroutine(MoveCor(_timeInterval));
    
        private IEnumerator MoveCor(float timeInterval)
        {
            var wait = new WaitForEndOfFrame();
            var elapsedTime = 0f;

            var firstTarget = firstTransform.position;
            var secondTarget = secondTransform.position;

            var target = firstTarget;
            var startPos = secondTarget;
            
            while (true)
            {
                transform.position = Vector3.Lerp(startPos, target, elapsedTime / timeInterval);
                elapsedTime += Time.deltaTime;
            
                if (elapsedTime > timeInterval)
                {
                    target = target == firstTarget ? secondTarget : firstTarget;
                    startPos = target == firstTarget ? secondTarget : firstTarget;
                    elapsedTime = 0f;
                }
            
                yield return wait;
            }
        }
    }
}