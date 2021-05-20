/*
 * 
 * 
 * Part of EAFramework
 * 
 * Created by: Pierre Rossel 2020-04-01
 * 
 */

using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace EAFramework
{
    /* This condition determines whether the next actions will be executed 
     * based on the value of a bool in an animator parameters.
     */
    public class ActionConditionAnimBool : Action
    {
        [Tooltip("Animator to look for the bool parameter value. If empty, try to find an animator on current game object, its parents or its children.")]
        public Animator animator;

        public string parameterName;

        public bool isValue;

        override public void Execute(string eventName)
        {
            // do nothing
        }

        public override bool CanContinue(string eventName)
        {
            // Try to find one on object or his parents or children
            if (animator == null) animator = GetComponentInParent<Animator>();
            if (animator == null) animator = GetComponentInChildren<Animator>();

            return animator.GetBool(parameterName) == isValue;
        }
    }

}