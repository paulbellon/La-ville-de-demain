/*
 * Allows to send custom events. 
 * 
 * Can be triggered from animation timeline.
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

    public class EventCustom : Event
    {
        [Tooltip("Name of this custom event. Can be triggered by ActionSendEventCustom.")]
        public new string name;

        [Tooltip("Delay after which the event will be sent.")]
        public float sendDelay = 0;


        public bool TriggerEventCustom(string name)
        {
            //Logger.Log("TriggerEventCustom " + name, this);

            if (name == this.name)
            {
                Invoke("SendEventCustom", sendDelay);

                return true;
            }
            return false;
        }

        void SendEventCustom()
        {
            SendEvent("EventCustom" + "-" + name);
        }

        // Static function to trigger custom events on an object
        public static void TriggerEventCustom(string name, GameObject target)
        {
            if (target == null)
            {
                return;
            }

            // Get the list of custom events
            EventCustom[] customEvents = target.GetComponentsInChildren<EventCustom>();

            // Trigger events
            foreach (var eventCustom in customEvents)
            {
                eventCustom.TriggerEventCustom(name);
            }

        }
    }

}
