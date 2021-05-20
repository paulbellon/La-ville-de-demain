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
    /* Call a function in a MonoBehaviour to determine if the actions can continue
     *
     * The function must be public and return a bool.
     * 
     *     public bool IsCondition() { return true }
     * 
     */
    public class ActionIfFunction : Action, IActionIf
    {
        public MonoBehaviour target;
        public string function = "IsCondition";
        public bool returns = true;

        bool IActionIf.EvaluateCondition(string eventName)
        {
            // Call condition function
            Type type = target.GetType();
            MethodInfo theMethod = type.GetMethod(function);
            return (bool)theMethod.Invoke(target, null) == returns;
        }
    }

}