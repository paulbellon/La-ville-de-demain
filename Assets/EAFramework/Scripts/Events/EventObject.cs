/*
 * Detect events on a GameObject
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

    public class EventObject : Event
    {
        [Tooltip("Awake is sent only once before object (or component) is enabled for the first time.")]
        public bool sendEventAwake;

        [Tooltip("Enable is sent each time the object (or component) is activated (also when Play if object is already active.")]
        public bool sendEventEnable = true;

        [Tooltip("Start is sent only once after object (or component) is enabled for the first time.")]
        public bool sendEventStart;

        [Tooltip("Disable is sent each time the object (or component) is disabled.")]
        public bool sendEventDisable;

        [Tooltip("Delay after which the event will be sent.")]
        public float sendDelay = 0;

        new void Awake()
        {
            base.Awake();

            if (eventSource == null)
            {
                eventSource = gameObject;
            }

            //Logger.Log("Awake " + name, this);
            if (sendEventAwake) Invoke("SendObjectAwake", sendDelay);
        }

        void OnEnable()
        {
            //Logger.Log("OnEnable " + name, this);
            if (sendEventEnable) Invoke("SendObjectEnable", sendDelay);
        }

        void Start()
        {
            //Logger.Log("Start " + name, this);
            if (sendEventStart) Invoke("SendObjectStart", sendDelay);
        }

        private void OnDisable()
        {
            //Logger.Log("OnDisable " + name, gameObject);
            if (sendEventDisable) Invoke("SendObjectDisable", sendDelay);
        }


        void SendObjectAwake()
        {
            SendEvent("ObjectAwake");
        }

        void SendObjectEnable()
        {
            SendEvent("ObjectEnable");
        }

        void SendObjectStart()
        {
            SendEvent("ObjectStart");
        }

        void SendObjectDisable()
        {
            SendEvent("ObjectDisable");
        }

    }

}
