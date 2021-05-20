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
    public class ActionLog : Action
    {
        public string message = "Action executed";

        override public void Execute(string eventName)
        {
            Logger.Log("        " + message);
        }
    }

}
