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
    public class ActionAnimSetBool : Action
    {
        public enum Action
        {
            set,
            toggle
        }
        public Action action;
        public string parameter = "name";
        public bool toValue;

        [Tooltip("Animator in which to set the value. If empty, try to find an animator on current game object, its parents or its children.")]
        public Animator animator;

        override public void Execute(string eventName)
        {
            // Try to find one on object or his parents or children
            if (animator == null) animator = GetComponentInParent<Animator>();
            if (animator == null) animator = GetComponentInChildren<Animator>();

            bool value = toValue;
            if (action == Action.toggle)
            {
                value = !animator.GetBool(parameter);
            }

            animator.SetBool(parameter, value);
        }
    }

}