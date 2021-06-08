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
    public class ActionTeleport : Action
    {
        public Transform target;
        public bool useTargetOrientation;

        private OVRCameraRig camRig;
        private ParticleSystem particles;
        private Collider cameraCollide;
        private AudioSource teleporterSound;

        void Start() {
           camRig = FindObjectOfType<OVRCameraRig>();

           particles = eventSource.GetComponentInChildren<ParticleSystem>();
           cameraCollide = camRig.GetComponent<Collider>();

           teleporterSound = this.gameObject.GetComponent<AudioSource>();

        }

        private void OnTriggerEnter(Collider other) {
          particles.Stop();
        }

        private void OnTriggerExit(Collider other) {
          particles.Play();
        }

        override public void Execute(string eventName)
        {
            if (target == null)
            {
                target = eventSource.transform;
            }

            if (camRig != null && target != null)
            {
                camRig.transform.position = target.transform.position;
                teleporterSound.Play();

                if (useTargetOrientation)
                {
                    camRig.transform.rotation = target.transform.rotation;
                    OVRManager manager = OVRManager.instance;
                    manager.headPoseRelativeOffsetTranslation = Vector3.zero;
                    manager.headPoseRelativeOffsetRotation = Vector3.zero;

                }
            }
        }

    }

}
