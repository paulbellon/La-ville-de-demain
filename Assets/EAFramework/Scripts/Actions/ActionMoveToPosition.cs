/*
 * 
 * 
 * Part of EAFramework
 * 
 * Created by: Pierre Rossel 2020-04-01
 * 
 */

using UnityEngine;

namespace EAFramework
{
    public class ActionMoveToPosition : Action
    {
        public GameObject what;
        public Transform targetPosition;
        public float duration = 2;
        public EasingFunction.Ease easing = EasingFunction.Ease.Linear;


        protected Vector3 startPosition;
        protected Rigidbody rbToMove;
        protected float startTime;
        protected EasingFunction.Function easingFunction; // cached at every execute

        override public void Execute(string eventName)
        {
            if (what == null)
            {
                what = eventSource;
            }
            if (targetPosition == null)
            {
                targetPosition = transform;
            }

            rbToMove = what.GetComponent<Rigidbody>();
            startTime = Time.time;
            startPosition = what.transform.position;
            easingFunction = EasingFunction.GetEasingFunction(easing);

            // Apply immediately
            if (duration < 0.00001f)
            {
                ApplyProgress(1);
            }
        }

        private void Update()
        {
            if (startTime > 0)
            {
                ApplyProgress((Time.time - startTime) / duration);
            }
        }

        protected void ApplyProgress(float progress)
        {
            if (progress >= 1)
            {
                progress = 1;
                startTime = 0;
            }

            // Apply easing to progress
            float easedProgress = easingFunction(0, 1, progress);

            Vector3 newPos = Vector3.Lerp(startPosition, targetPosition.position, easedProgress);

            if (rbToMove != null)
            {
                rbToMove.MovePosition(newPos);
            }
            else
            {
                what.transform.position = newPos;
            }

            if (startTime < float.Epsilon)
            {
                // TODO notify end of move

            }
        }

    }
}