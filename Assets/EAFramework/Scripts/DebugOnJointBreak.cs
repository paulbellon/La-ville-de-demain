/*
 * 
 * 
 * Part of EAFramework
 * 
 * Created by: Pierre Rossel 2020-04-01
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugOnJointBreak : MonoBehaviour
{
    private void OnJointBreak(float breakForce)
    {
        Debug.Log("OnJointBreak force: " + breakForce, gameObject);
    }
}
