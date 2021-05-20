/*
 * Detects collisions
 * 
 * Part of EAFramework
 * 
 * Created by: Pierre Rossel 2020-04-01
 * 
 */

using UnityEngine;
using UnityEngine.EventSystems;

namespace EAFramework
{

    public class EventCollision : Event
    {
        [Tooltip("Event will trigger only for a collision with these objects or with any object if empty.")]
        public System.Collections.Generic.List<GameObject> withObjects;
        
        public bool sendEventCollisionEnter;
        public bool sendEventCollisionStay;
        public bool sendEventCollisionExit;

        public bool sendEventTriggerEnter;
        public bool sendEventTriggerStay;
        public bool sendEventTriggerExit;

        [Tooltip("Delay after which the event will be sent.")]
        public float sendDelay = 0;

        private new void Awake()
        {
            if (eventSource == null) eventSource = gameObject;

            // Target must have a RigidBody to detect collision (and a Collider, but the RigidBody sends events)
            Rigidbody collisionSource = eventSource.GetComponentInParent<Rigidbody>();

            if (collisionSource == null)
            {
                Logger.LogWarning("EventCollision found no collision source", this);
            }


        }

        private void OnEnable()
        {
            Rigidbody collisionSource = eventSource.GetComponentInParent<Rigidbody>();

            // Add ColliderBridge if collider is not on this object
            if (collisionSource.gameObject != gameObject)
            {
                Logger.Log("Attaching colliderBridge to " + collisionSource.gameObject.name, collisionSource);
                collisionSource.gameObject.AddComponent<ColliderBridge>().SetTarget(this);
            }
        }

        private void OnDisable()
        {
            Rigidbody collisionSource = eventSource.GetComponentInParent<Rigidbody>();

            // Remove ColliderBridge if collider is not on this object
            if (collisionSource && collisionSource.gameObject != gameObject)
            {
                Logger.Log("Detaching colliderBridge from " + collisionSource.gameObject.name, collisionSource);

                // Find the bridge connected to this object
                ColliderBridge[] bridges = collisionSource.gameObject.GetComponents<ColliderBridge>();
                foreach (var bridge in bridges)
                {
                    if (bridge.GetTarget() == this)
                    {
                        Destroy(bridge);
                    }
                }
            }
        }

        bool AllowWith(GameObject obj)
        {
            return withObjects.Count == 0 || withObjects.Contains(obj);
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (sendEventCollisionEnter && AllowWith(collision.gameObject)) Invoke("SendCollisionEnter", sendDelay);
        }

        public void OnCollisionStay(Collision collision)
        {
            if (sendEventCollisionStay && AllowWith(collision.gameObject)) Invoke("SendCollisionStay", sendDelay);
        }

        public void OnCollisionExit(Collision collision)
        {
            if (sendEventCollisionExit && AllowWith(collision.gameObject)) Invoke("SendCollisionExit", sendDelay);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (sendEventTriggerEnter && AllowWith(other.gameObject)) Invoke("SendTriggerEnter", sendDelay);
        }

        public void OnTriggerStay(Collider other)
        {
            if (sendEventTriggerStay && AllowWith(other.gameObject)) Invoke("SendTriggerStay", sendDelay);
        }

        public void OnTriggerExit(Collider other)
        {
            if (sendEventTriggerExit && AllowWith(other.gameObject)) Invoke("SendTriggerExit", sendDelay);
        }

        void SendCollisionEnter()
        {
            SendEvent("CollisionEnter");
        }

        void SendCollisionStay()
        {
            SendEvent("CollisionStay");
        }

        void SendCollisionExit()
        {
            SendEvent("CollisionExit");
        }

        void SendTriggerEnter()
        {
            SendEvent("TriggerEnter");
        }

        void SendTriggerStay()
        {
            SendEvent("TriggerStay");
        }

        void SendTriggerExit()
        {
            SendEvent("TriggerExit");
        }


    }

    /// <summary>
    /// Collider bridge.
    /// 
    /// Helper class to forward collision and trigger events from one object to another.
    /// </summary>
    public class ColliderBridge : MonoBehaviour
    {
        MonoBehaviour bridgeTarget;

        public void SetTarget(MonoBehaviour target)
        {
            bridgeTarget = target;
        }

        public MonoBehaviour GetTarget()
        {
            return bridgeTarget;
        }

        private void OnCollisionEnter(Collision collision)
        {
            bridgeTarget.SendMessage("OnCollisionEnter", collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            bridgeTarget.SendMessage("OnCollisionStay", collision, SendMessageOptions.DontRequireReceiver);
        }

        private void OnCollisionExit(Collision collision)
        {
            bridgeTarget.SendMessage("OnCollisionExit", collision, SendMessageOptions.DontRequireReceiver);
        }

        private void OnTriggerEnter(Collider other)
        {
            bridgeTarget.SendMessage("OnTriggerEnter", other, SendMessageOptions.DontRequireReceiver);
        }

        private void OnTriggerStay(Collider other)
        {
            bridgeTarget.SendMessage("OnTriggerStay", other, SendMessageOptions.DontRequireReceiver);
        }

        private void OnTriggerExit(Collider other)
        {
            bridgeTarget.SendMessage("OnTriggerExit", other, SendMessageOptions.DontRequireReceiver);
        }

    }

}
