/*
 * Base class for events
 * 
 * Part of EAFramework
 * 
 * Created by: Pierre Rossel 2020-04-01
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

namespace EAFramework
{
    public class Event : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Optional object to get event from.\nBy defaut, if not set, it's the first parent with a Renderer, RigidBody or Collider.\nBetter leave empty unless default does not apply. Check the detected source at runtime.")]
        private GameObject _eventSource;

        public GameObject eventSource
        {
            get => _eventSource;
            set
            {
                _eventSource = value;
                //Logger.Log("Get all actions on event " + this, this);
                // Notify actions
                foreach (var action in GetActions())
                {
                    action.OnEventSource(value);
                }
            }
        }

        enum IfState
        {
            OFF,   // outside if elseif else end block
            IDLE,  // waiting for true condition
            RUN,   // in true condition block
            DONE   // block done, waiting for end
        };

        protected void Awake()
        {
            if (!eventSource)
            {
                GameObject source = GuessEventSource();
                if (source)
                {
                    eventSource = source;
                }
            }
            else
            {
                // Reaffect to notify actions
                eventSource = eventSource;
            }
        }


        private void Start() { }

        /// <summary>
        /// Guesses the event source. Defaults to first object with a Renderer in object and parents.
        /// Override if you need another default source
        /// </summary>
        /// <returns>The event source.</returns>
        virtual public GameObject GuessEventSource()
        {
            GameObject evtSource = null;

            // Defaults to first objet with a Renderer or Rigidbody or Collider, looking into this object and its parents
            if (evtSource == null) 
                evtSource = GetComponentInParent<Renderer>()?.gameObject;

            if (evtSource == null)
                evtSource = GetComponentInParent<Rigidbody>()?.gameObject;

            if (evtSource == null)
                evtSource = GetComponentInParent<Collider>()?.gameObject;

            return evtSource;
        }

        /// <summary>
        /// Gets the actions related to this event.
        /// </summary>
        /// <returns>The actions.</returns>
        protected List<Action> GetActions()
        {
            List<Action> actions = new List<Action>();

            bool foundEvent = false;
            MonoBehaviour[] components = GetComponentsInChildren<MonoBehaviour>();
            foreach (var component in components)
            {
                //Logger.Log("Looking " + component, component);

                if (!component.enabled)
                {
                    continue;
                }

                if (component is Event)
                {
                    // It is another event, stop looking for actions
                    if (foundEvent) break;

                    // Still looking for afterEvent
                    foundEvent = component == this;
                }

                if (foundEvent && component is Action)
                {
                    //Logger.Log("ADD " + component, component);
                    actions.Add((Action)component);
                }
            }

            return actions;
        }

        protected void SendEvent(string eventName, bool canLog = true)
        {
            if (canLog) Logger.Log("▼ event: " + eventName + " (" + name + ")", this);

            // To manage the if, else, elseif, end blocks
            IfState ifState = IfState.OFF;
            Stack<IfState> stackIfState = new Stack<IfState>();

            foreach (var action in GetActions())
            {
                if (action is IActionIf)
                {
                    stackIfState.Push(ifState);
                    ifState = ((IActionIf)action).EvaluateCondition(eventName) ? IfState.RUN : IfState.IDLE;
                }
                else if (action is IActionElseIf)
                {
                    if (ifState == IfState.IDLE)
                    {
                        ifState = ((IActionIf)action).EvaluateCondition(eventName) ? IfState.RUN : IfState.IDLE;
                    }
                    else if (ifState == IfState.RUN)
                    {
                        ifState = IfState.DONE;
                    }
                }
                else if (action is ActionElse)
                {
                    if (ifState == IfState.IDLE)
                    {
                        ifState = IfState.RUN;
                    }
                    else if (ifState == IfState.RUN)
                    {
                        ifState = IfState.DONE;
                    }
                }
                else if (action is ActionEndIf)
                {
                    if (stackIfState.Count > 0)
                    {
                        ifState = stackIfState.Pop();
                    }
                    else
                    {
                        Logger.LogError("extra ActionEndIf component found without matching ActionIf" + " (" + name + ")", this);
                    }
                }
                else if (ifState == IfState.OFF || ifState == IfState.RUN)
                {
                    // Update event source in case action was enabled after event
                    action.OnEventSource(eventSource);

                    if (canLog) Logger.Log("                                ▶︎  action " + action.GetType() + " (" + name + ", eventSource: " + eventSource.name + ")", this);
                    action.Execute(eventName);
                }

                // Verify if we can continue after this action
                bool canContinue = (action as Action).CanContinue(eventName);
                if (!canContinue)
                {
                    break;
                }
            }
        }
    }
}