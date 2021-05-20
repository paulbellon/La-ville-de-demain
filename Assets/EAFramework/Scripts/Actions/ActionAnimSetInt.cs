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
    public class ActionAnimSetInt : Action
    {
        public string parameter = "name";
        public enum Action
        {
            set,
            add,
            remove
        }
        public Action action;
        public int value;

        [Tooltip("Animator in which to set the value. If empty, try to find an animator on current game object, its parents or its children.")]
        public Animator animator;

        override public void Execute(string eventName)
        {
            // Try to find one on object or his parents or children
            if (animator == null) animator = GetComponentInParent<Animator>();
            if (animator == null) animator = GetComponentInChildren<Animator>();

            if (animator != null)
            {

                int newValue = value;
                if (action ==  Action.add)
                {
                    newValue = animator.GetInteger(parameter) + value;
                }
                else if (action == Action.remove)
                {
                    newValue = animator.GetInteger(parameter) - value;
                }

                animator.SetInteger(parameter, newValue);
            }
            else
            {
                Logger.LogWarning("No animator found", this);
            }

        }
    }

}