using System.Collections;
using System.Collections.Generic;
using EAFramework;
using UnityEngine;

//
// A component to rotate a ring object with the gaze.
//
// Designed to work with Oculus VR OVRCameraRig and EAFramework.
//
// Made by Pierre Rossel for Gabriel Abergiel, HEAD-Genève, master Media Design
//
// 2020-05-08

public class RingGazeRotator : MonoBehaviour
{
    public int snapToAngle = 15;
    public bool randomStart;

    bool tracking;

    Transform eye;

    // Reference of cross product. Updated when start tracking
    Vector3 crossRef = Vector3.zero;

    // rotation angle when start tracking
    float startAngle;

    // Ring position is in place
    bool inPlace = false;

    // Start is called before the first frame update
    void Start()
    {
        eye = GameObject.Find("CenterEyeAnchor").transform;

        SetRingAngle(randomStart ? Random.Range(0, 360 / snapToAngle) * snapToAngle : 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (crossRef != Vector3.zero)
        {
            Vector3 cross = Vector3.Cross(transform.position - eye.position, eye.forward);
            //float angle = Vector3.Angle(cross, crossRef);
            float angle = Vector3.SignedAngle(cross, crossRef, transform.position - eye.position);

            // Snap to angle
            angle = (int)(angle / snapToAngle) * snapToAngle;

            //Logger.Log(angle);

            SetRingAngle(startAngle + angle);
        }
    }

    void SetRingAngle(float angle)
    {
        // Get current angle
        Vector3 angles = transform.localRotation.eulerAngles;

        // Determine if angle has changed
        bool changedAngle = Mathf.Abs(Mathf.DeltaAngle(angles.y, angle)) > 0.1f;
        if (changedAngle) {
            EAFramework.Logger.Log("changedAngle: " + angles.y + " -> " + angle);
        }

        // Apply the rotation to the start rotation
        angles.y = angle;
        transform.localRotation = Quaternion.Euler(angles);

        // Get the list of custom events
        EventCustom[] customEvents = GetComponentsInChildren<EventCustom>();

        // notify angle change
        if (changedAngle)
        {
            foreach (var eventCustom in customEvents)
            {
                eventCustom.TriggerEventCustom("ringMoved");
            }
        }

        // Ring is in place when it y angle is back to 0
        string eventName = "";
        bool nowInPlace = Mathf.Abs(Mathf.DeltaAngle(angles.y, 0)) < 0.1f;
        if (nowInPlace != inPlace)
        {
            inPlace = nowInPlace;
            eventName = inPlace ? "ringRight" : "ringWrong";

            // Trigger custom event
            foreach (var eventCustom in customEvents)
            {
                eventCustom.TriggerEventCustom(eventName);
            }
        }

    }

    public void StartTracking()
    {
        crossRef = Vector3.Cross(transform.position - eye.position, eye.forward);
        startAngle = transform.localRotation.eulerAngles.y;
    }


    public void StopTracking()
    {
        crossRef = Vector3.zero;
    }
}
