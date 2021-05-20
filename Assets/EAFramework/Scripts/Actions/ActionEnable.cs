/*
 * 
 * 
 * Part of EAFramework
 * 
 * Created by: Pierre Rossel 2020-04-01
 * 
 */

using System.Reflection;
using UnityEngine;

namespace EAFramework
{
    public class ActionEnable : Action
    {
        public bool setEnable = true;
        public bool toggle;

        [Tooltip("Behaviours to enable / disable.")]
        public Object[] behaviours;

        override public void Execute(string eventName)
        {
            foreach (var beh in behaviours)
            {
                PropertyInfo property = beh.GetType().GetProperty("enabled");
                if (toggle)
                {
                    property.SetValue(beh, !(bool)(property.GetValue(beh)));
                }
                else
                {
                    property.SetValue(beh, setEnable);
                }
            }

        }

    }
}