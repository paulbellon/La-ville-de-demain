/*
 * Detect when main camera enters/leaves an object
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

    public class EventGaze : Event
    {

        public bool sendEventGazeEnter = true;
        public bool sendEventGazeStay;
        public bool sendEventGazeExit;

        [Tooltip("Delay after which the event will be sent. Allows to cancel event if gaze exits before delay expires when sendOnGazeEnter is active. Similar for exit.")]
        public float sendDelay = 0;

        [Tooltip("Show GazeStay events (and actions) in console. Usually many lines.")]
        public bool logStayEvents = false;

        GazeRaycaster gazeRaycaster;

        protected bool gazing;

        void OnEnable()
        {
            //Logger.Log("OnEnable " + name);

            // Get or add the raycaster
            gazeRaycaster = eventSource.GetComponent<GazeRaycaster>();
            if (gazeRaycaster == null)
            {
                gazeRaycaster = eventSource.AddComponent<GazeRaycaster>();
            }

            // Register our callbacks
            gazeRaycaster.onGazeEnter += GazeEnter;
            gazeRaycaster.onGazeExit += GazeExit;
        }

        private void Update()
        {
            if (gazing)
            {
                if (sendEventGazeStay)
                {
                    Invoke("SendGazeStay", sendDelay);
                }
            }
        }

        private void OnDisable()
        {
            CancelInvoke("SendGazeEnter");
            CancelInvoke("SendGazeStay");
            CancelInvoke("SendGazeExit");

            // Unregister our callbacks
            gazeRaycaster.onGazeEnter -= GazeEnter;
            gazeRaycaster.onGazeExit -= GazeExit;
        }

        protected void GazeEnter()
        {
            gazing = true;

            // Cancel pending gazeExit in any
            CancelInvoke("SendGazeExit");

            if (sendEventGazeEnter)
            {
                Invoke("SendGazeEnter", sendDelay);
            }
        }

        protected void GazeExit()
        {
            gazing = false;

            // Cancel pending gazeEnter in any
            CancelInvoke("SendGazeEnter");
            CancelInvoke("SendGazeStay");

            if (sendEventGazeExit)
            {
                Invoke("SendGazeExit", sendDelay);
            }

        }

        void SendGazeEnter()
        {
            SendEvent("GazeEnter");
        }

        void SendGazeStay()
        {
            SendEvent("GazeStay", logStayEvents);
        }

        void SendGazeExit()
        {
            SendEvent("GazeExit");
        }

    }

}
