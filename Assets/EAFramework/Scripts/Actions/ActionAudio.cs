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
    public class ActionAudio : Action
    {
        public enum Action { play, pause, stop };

        public Action action;

        [Tooltip("Optional audio source. Audio source in object or parent or child will be automatically found.")]
        public AudioSource audioSource;

        override public void Execute(string eventName)
        {
            // Find audio source on object or parents or children if not provided
            if (audioSource == null) audioSource = GetComponentInParent<AudioSource>();
            if (audioSource == null) audioSource = GetComponentInChildren<AudioSource>();

            Debug.Log("Now playing the record");

            switch (action)
            {
                case Action.play:
                    audioSource.Play();
                    break;
                case Action.pause:
                    audioSource.Pause();
                    break;
                case Action.stop:
                    audioSource.Stop();
                    break;
            }
        }
    }

}
