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

namespace EAFramework
{
    public class GrabberConnector : MonoBehaviour
    {
        public Rigidbody rb;

        [Range(0.01f, 1f)]
        public float speed = 0.2f;

        private void Start()
        {
            Grab(rb);
        }

        // Update is called once per frame
        void Update()
        {
            if (rb != null)
            {
                rb.MovePosition(Vector3.Lerp(rb.position, transform.position, speed));
                //rb.MovePosition(transform.position);
                rb.MoveRotation(transform.rotation);
            }
        }

        void Grab(Rigidbody newGrab)
        {
            Drop();

            if (newGrab != null)
            {
                rb = newGrab;
                transform.position = rb.transform.position;
                transform.rotation = rb.transform.rotation;
                rb.isKinematic = true;
            }
        }

        void Drop()
        {
            if (rb)
            {
                rb.isKinematic = false;
                rb = null;
            }
        }


    }
}