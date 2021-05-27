/*
 * This class is a helper for EventGaze
 * There is no need to add it manually to any object
 *
 * Part of EAFramework
 *
 * Created by: Pierre Rossel 2020-04-01
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeRaycaster : MonoBehaviour
{
    public delegate void OnGazeEvent();

    public event OnGazeEvent onGazeEnter;
    public event OnGazeEvent onGazeExit;

    protected static Camera s_cam;
    protected static int s_lastRaycastFrame;
    protected static GazeRaycaster s_gazedObject;
    protected static GazeRaycaster s_gazedObjectLast;
    protected static RaycastHit s_hit; // Only contains valid info when s_gazedObject is not null


    // Update is called once per frame
    void Update()
    {
        // TODO: avoid doing raycast if all event subscribers unregistered. Probably rare cases

        // Only do it one per frame, the first active instance will do for every others
        if (Time.frameCount != s_lastRaycastFrame)
        {
            s_lastRaycastFrame = Time.frameCount;

            // Get the common raycast camera
            if (!s_cam) {
                s_cam = FindObjectOfType<Camera>();
                Debug.Log("GazeRaycaster will use camera " + s_cam.name, s_cam);
            }

            s_gazedObjectLast = s_gazedObject;

            LayerMask mask = LayerMask.GetMask("Interactible");

            bool hitSomething = Physics.Raycast(s_cam.transform.position, s_cam.transform.forward, out s_hit, 50f, mask);
            if (hitSomething)
            {
                s_gazedObject = s_hit.collider.GetComponentInParent<GazeRaycaster>();

                // Display actual raycast being projected by the camera
                Debug.DrawRay(s_cam.transform.position, s_cam.transform.forward * 50f, Color.red);
            }
            else
            {
                s_gazedObject = null;
            }

            // Send events
            if (s_gazedObject != s_gazedObjectLast)
            {
                if (s_gazedObjectLast)
                {
                    s_gazedObjectLast.onGazeExit();
                }
                if (s_gazedObject)
                {
                    s_gazedObject.onGazeEnter();
                }
            }
        }
    }
}
