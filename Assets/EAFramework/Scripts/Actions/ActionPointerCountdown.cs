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
    public class ActionPointerCountdown : Action
    {
        public float duration = 1;

        protected float countDown = -1;

        private void Update()
        {
            if (countDown > 0)
            {
                countDown -= Time.deltaTime;

                float fill = Mathf.Clamp01(countDown / duration);
                if (fill <= float.Epsilon)
                {
                    fill = 1;
                }

                Debug.Log(fill);
            }
        }

        override public void Execute(string eventName)
        {
            countDown = duration;
        }
    }

}