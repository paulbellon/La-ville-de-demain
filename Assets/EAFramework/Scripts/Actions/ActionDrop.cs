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
    public class ActionDrop : Action
    {
        [Tooltip("Enable all colliders in object (and children). Allows to restore colliders disabled by ActionGrab.")]
        public bool enableColliders = false;

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

                FixedJoint[] joints = rb.GetComponentsInChildren<FixedJoint>();

                // Find the joint that is connected to cam Rigidbody
                foreach (var joint in joints)
                {
                    if (joint.connectedBody == rbCam)
                    {
                        //Logger.Log("destroying joint " + joint + " on " + rb.gameObject.name, joint.gameObject);
                        joint.breakForce = 0;
                        Destroy(joint);
                    }
                }


                if (enableColliders)
                {
                    foreach (var col in rb.gameObject.GetComponentsInChildren<Collider>())
                    {
                        col.enabled = true;
                    }
                }
            }
        }
    }
}

