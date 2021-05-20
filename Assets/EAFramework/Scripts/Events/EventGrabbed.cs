/*
 * Detects when an object is grabbed
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

    public class EventGrabbed : Event
    {

        [Tooltip("Delay after which the event will be sent.")]
        public float sendDelay = 0;


        public static void TriggerOn (GameObject go)
        {
            EventGrabbed[] evts = go.GetComponentsInChildren<EventGrabbed>(false);
            foreach (var evt in evts)
            {
                if (evt.enabled) { 
                    evt.TriggerEventGrabbed();
                }
            }
        }

        void TriggerEventGrabbed()
        {
            Invoke("SendGrabbed", sendDelay);
        }

        void SendGrabbed()
        {
            SendEvent("Grabbed");
        }

    }

}
