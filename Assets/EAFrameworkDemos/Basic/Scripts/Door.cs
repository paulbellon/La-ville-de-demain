using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EAFramework
{
    public class Door : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public bool IsDoorOpen()
        {
            return GetComponent<Animator>().GetBool("open");
        }

        public bool IsDoorClosed()
        {
            return !GetComponent<Animator>().GetBool("open");
        }
    }
}