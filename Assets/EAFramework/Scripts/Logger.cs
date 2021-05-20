/*
 * Logger static class 
 * 
 * Part of EAFramework
 * 
 * Created by: Pierre Rossel 2020-04-01
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EAFramework
{
    public class Logger : MonoBehaviour
    {
        public enum Level { NONE, ERROR, WARNING, INFO }
        public static Level level = Application.isEditor ? Level.INFO : Level.NONE;

        public static void LogError(object message, Object context = null)
        {
            if (level >= Level.ERROR)
            {
                Debug.LogError(message, context);
            }
        }
        public static void LogWarning(object message, Object context = null)
        {
            if (level >= Level.WARNING)
            {
                Debug.LogWarning(message, context);
            }
        }
        public static void Log(object message, Object context = null)
        {
            if (level >= Level.INFO)
            {
                Debug.Log(message, context);
            }
        }
    }
}
