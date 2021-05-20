using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EAFramework
{

    public class WaterDropSpawner : MonoBehaviour
    {
        public GameObject drop;
        public int number = 100;
        public int batch = 100;
        public float interval = 0.1f;

        float nextTime = 0;
        int nGenerated = 0;

        // Start is called before the first frame update
        void Start()
        {
            nextTime = Time.time + interval;
        }

        // Update is called once per frame
        void Update()
        {
            if (nGenerated < number && Time.time >= nextTime)
            {
                Vector3 size = drop.GetComponent<SphereCollider>().bounds.size;
                Debug.Log(size.ToString("F4"));

                int nBySide = Mathf.RoundToInt(Mathf.Sqrt(batch));
                for (int x = nBySide; x > 0; x--)
                {
                    for (int z = nBySide; z > 0; z--)
                    {
                        GameObject newDrop = Instantiate(drop, transform);
                        newDrop.transform.position = transform.position + new Vector3(
                            (x - nBySide / 2f) * size.x,
                            0,
                            (z - nBySide / 2f) * size.z
                        );
                        nGenerated++;

                        if (nGenerated >= number)
                        {
                            return;
                        }
                    }
                }

                nextTime = Time.time + interval;
            }
        }
    }
}