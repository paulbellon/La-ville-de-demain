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
    public class ActionMoveToRotation : Action
    {
        public GameObject what;
        public Transform targetRotation;
        public float duration = 2;
        public EasingFunction.Ease easing = EasingFunction.Ease.Linear;


        protected Quaternion startRotation;
        protected Quaternion startRigRotation;
        protected Quaternion endRotation;
        protected Rigidbody rbToMove;
        protected OVRCameraRig camRig;
        protected float startTime;
        protected EasingFunction.Function easingFunction; // cached at every execute

        override public void Execute(string eventName)
        {
            if (what == null)
            {
                what = eventSource;
            }

            if (targetRotation == null)
            {
                targetRotation = transform;
            }

            rbToMove = what.GetComponent<Rigidbody>();
            camRig = what.GetComponentInChildren<OVRCameraRig>();
            easingFunction = EasingFunction.GetEasingFunction(easing);
            startRotation = what.transform.rotation;
            endRotation = targetRotation.rotation;
            if (camRig != null)
            {
                startRigRotation = Quaternion.Euler(OVRManager.instance.headPoseRelativeOffsetRotation);
                Quaternion startCamRotation = camRig.GetComponentInChildren<Camera>().transform.rotation;
                startRotation = camRig.GetComponentInChildren<Camera>().transform.rotation;
                Quaternion diff = targetRotation.rotation * Quaternion.Inverse(startCamRotation);
                Quaternion diff2 = Quaternion.Inverse(startCamRotation) * targetRotation.rotation;
                endRotation = diff * startRotation;
            }

            // direct test
            OVRManager.instance.headPoseRelativeOffsetRotation = Vector3.zero;
            what.transform.rotation = endRotation;
            //return;

            //startTime = Time.time;

            // Apply immediately
            /*if (duration < 0.00001f)
            {
                ApplyProgress(1);
            }*/
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

            //Quaternion newRot = Quaternion.Lerp(startRotation, targetRotation.rotation, easedProgress);
            Quaternion newRot = Quaternion.Lerp(startRotation, endRotation, easedProgress);

            // If there is a rig, must move its orientation to zero as well
            if (camRig != null)
            {
                //Quaternion newRigRot = Quaternion.Lerp(startRigRotation, Quaternion.Euler(0, 0, 0), easedProgress);
                //OVRManager.instance.headPoseRelativeOffsetRotation = newRigRot.eulerAngles;
            }

            if (rbToMove != null)
            {
                rbToMove.MoveRotation(newRot);
            }
            else
            {
                what.transform.rotation = newRot;
            }

            if (startTime < float.Epsilon)
            {
                // TODO notify end of move

            }
        }

    }
}
