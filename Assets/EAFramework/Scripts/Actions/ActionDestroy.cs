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
    public class ActionDestroy : Action
    {
        public bool destroyEventSource = true;

        [Tooltip("Other object(s) to destroy. Note: default Action Target is ignored by this action. You shoud either check the Me option or add object(s) to this list.")]
        public GameObject[] other;

        override public void Execute(string eventName)
        {

            if (destroyEventSource && eventSource != null)
            {
                Destroy(eventSource);
            }

            foreach (var go in other)
            {
                Destroy(go);
            }
        }

    }
}