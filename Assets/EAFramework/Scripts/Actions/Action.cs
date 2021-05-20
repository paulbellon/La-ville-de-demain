/*
 * Base class for specific Actions
 * 
 * Part of EAFramework
 * 
 * Created by: Pierre Rossel 2020-04-01
 * 
 */

using UnityEngine;

namespace EAFramework
{
    public class Action : MonoBehaviour
    {
        // Source of the event. Event and actions can be on empty children. The source is set be the event connected to this action.
        protected GameObject eventSource;

        private void Start() { }

        // Execute an action. 
        virtual public void Execute(string eventName)
        {
            Logger.LogWarning("TODO: Override Execute() in " + this.GetType());
        }

        // Return true to continue next actions or false to stop.
        virtual public bool CanContinue(string eventName)
        {
            return true;
        }

        virtual public void OnEventSource(GameObject eventSource)
        {
            this.eventSource = eventSource;
        }
    }

    public interface IActionIf
    {
        bool EvaluateCondition(string eventName);
    }

    public interface IActionElseIf : IActionIf
    { }

}