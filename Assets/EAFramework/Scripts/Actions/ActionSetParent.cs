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
    public class ActionSetParent : Action
    {

        public GameObject whichObject;
        public GameObject setParentTo;

        override public void Execute(string eventName)
        {
            if (whichObject == null) whichObject = eventSource;

            whichObject.transform.SetParent(setParentTo.transform);
        }
    }

}