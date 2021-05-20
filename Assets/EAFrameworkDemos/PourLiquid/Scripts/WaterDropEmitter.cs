using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EAFramework
{
    public class WaterDropEmitter : MonoBehaviour
    {
        public GameObject drop;
        public int number = 100;
        public float interval = 0.1f;

        float nextTime = 0;
        int nGenerated = 0;
        GameObject last;

        // Start is called before the first frame update
        void Start()
        {
            nextTime = Time.time + interval;
            last = drop;
        }

        // Update is called once per frame
        void Update()
        {
            // For simulation in editor where we cannot tild the head
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.parent.localRotation = Quaternion.Euler(0, 0, -30);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                transform.parent.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (nGenerated < number && Time.time >= nextTime)
            {
                Vector3 size = drop.GetComponent<SphereCollider>().bounds.size;

                if (!last || Vector3.Distance(transform.position, last.transform.position) > size.magnitude)
                {
                    GameObject newDrop = Instantiate(drop);
                    newDrop.transform.position = transform.position;
                    last = newDrop;
                    nGenerated++;
                    nextTime = Time.time + interval;

                }

            }
        }
    }
}