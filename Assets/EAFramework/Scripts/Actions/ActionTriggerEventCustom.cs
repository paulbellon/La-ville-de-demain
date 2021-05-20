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
    public class ActionTriggerEventCustom : Action
    {
        public GameObject target;
        public string message;

        override public void Execute(string eventName)
        {
            // Get defaut action target from event source
            if (!target)
            {
                target = eventSource;
            }

            EventCustom.TriggerEventCustom(message, target);
        }
    }

}