/*
 * 
 * 
 * Part of EAFramework
 * 
 * Created by: Pierre Rossel 2020-04-01
 * 
 */

using UnityEngine;
using UnityEngine.SceneManagement;

namespace EAFramework
{
    public class ActionLoadScene : Action
    {
        public string sceneName = "Scene name";

        override public void Execute(string eventName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

}