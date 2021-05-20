/*
 * 
 * 
 * Part of EAFramework
 * 
 * Created by: Pierre Rossel 2020-04-01
 * 
 */

using System.Collections.Generic;
using UnityEngine;

namespace EAFramework
{
    public class ActionGrab : Action
    {
        [Tooltip("Disable all colliders in object (and children). Allows to gaze \"trough\" the grabbed object.")]
        public bool disableColliders = false;

        [Tooltip("Disable the kinematic option on the Rigidbody. This action cannot grab an kinematic object.")]
        public bool disableKinematic = false;

        [Tooltip("Positive value will move grabbed object to specified ditance from the camera. Negative value will do nothing.")]
        public float bringToDistance = -1;

        [Tooltip("Positive value will move grabbed object to specified ditance from the camera. Negative value will do nothing.")]
        public float breakForce = float.PositiveInfinity;

        OVRCameraRig camRig;
        GameObject cam;
        Rigidbody rbCam;

        private void Start()
        {
            // Get Camera Rig
            camRig = FindObjectOfType<OVRCameraRig>();
            cam = camRig.GetComponentInChildren<Camera>(false).gameObject;
            rbCam = cam.GetComponent<Rigidbody>();
            if (rbCam == null)
            {
                rbCam = cam.AddComponent<Rigidbody>();
                rbCam.isKinematic = true;
            }
        }

        override public void Execute(string eventName)
        {

            Rigidbody rb = eventSource.GetComponentInParent<Rigidbody>();

            if (rbCam != null && rb != null)
            {

                // Check if rb is alread being dragged (i.e. There is already a FixedJoint on the cam Rigidbody)
                FixedJoint[] existingJoints = rb.GetComponents<FixedJoint>();
                foreach (var existingJoint in existingJoints)
                {
                    if (existingJoint.connectedBody == rbCam)
                    {
                        return;
                    }
                }

                if (bringToDistance > 0)
                {
                    rb.transform.position = cam.transform.TransformPoint(new Vector3(0, 0, bringToDistance));
                }

                //FixedJoint joint = cam.AddComponent<FixedJoint>();
                //Logger.Log("creaging fixed joint on " + rb.gameObject.name + " to " + rbCam.name);
                FixedJoint joint = rb.gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = rbCam;
                cam.GetComponent<Rigidbody>().isKinematic = true;
                joint.breakForce = breakForce;
                //Debug.Log("breakforce: " + joint.breakForce, joint);

                if (disableColliders)
                {
                    foreach (var col in rb.gameObject.GetComponentsInChildren<Collider>())
                    {
                        col.enabled = false;
                    }
                }

                if (disableKinematic)
                {
                    rb.isKinematic = false;
                }

                EventGrabbed.TriggerOn(eventSource);
            }

        }

    }

}