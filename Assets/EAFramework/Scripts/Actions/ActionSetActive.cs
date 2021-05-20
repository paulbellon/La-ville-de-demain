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
    public class ActionSetActive : Action
    {
        public bool setActive = true;
        public bool toggle;
        public bool me = true;

        [Tooltip("Other object(s) to set active. Note: default Action Target is ignored by this action. You shoud either check the Me option or add object(s) to this list.")]
        public GameObject[] others;

        override public void Execute(string eventName)
        {
            if (me)
            {
                if (toggle)
                {
                    gameObject.SetActive(!gameObject.activeSelf);
                }
                else
                {
                    gameObject.SetActive(setActive);
                }
            }

            foreach (var go in others)
            {
                if (toggle)
                {
                    go.SetActive(!go.activeSelf);
                }
                else
                {
                    go.SetActive(setActive);
                }
            }

        }

    }
}