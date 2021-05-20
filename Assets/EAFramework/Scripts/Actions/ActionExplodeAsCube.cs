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
using EAFramework;

namespace EAFramework
{

    public class ActionExplodeAsCube : Action
    {
        public GameObject target;

        public float cubeSize = 0.2f;
        public int cubesInRow = 5;

        float cubesPivotDistance;
        Vector3 cubesPivot;

        public float explosionForce = 50f;
        public float explosionRadius = 4f;
        public float explosionUpward = 0.4f;

        [Tooltip("Optional prefab for pieces. If not set pieces will be cubes.")]
        public GameObject piecesPrefab;

        // Use this for initialization
        void Start()
        {
            // Get defaut action target from event source  
            if (!target)
            {
                target = eventSource;
            }

            //calculate pivot distance
            cubesPivotDistance = cubeSize * cubesInRow / 2;
            //use this value to create pivot vector)
            cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);

        }

        override public void Execute(string eventName)
        {
            Explode();
        }

        public void Explode()
        {
            //make object disappear
            target.SetActive(false);

            //loop 3 times to create 5x5x5 pieces in x,y,z coordinates
            for (int x = 0; x < cubesInRow; x++)
            {
                for (int y = 0; y < cubesInRow; y++)
                {
                    for (int z = 0; z < cubesInRow; z++)
                    {
                        CreatePiece(x, y, z);
                    }
                }
            }

            //UnityEditor.EditorApplication.isPaused = true;
            //return;

            //get explosion position
            //Vector3 explosionPos = target.transform.position;
            Vector3 explosionPos = target.GetComponent<Renderer>().bounds.center;

            //get colliders in that position and radius
            Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
            //add explosion force to all colliders in that overlap sphere
            foreach (Collider hit in colliders)
            {
                //get rigidbody from collider object
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    //add explosion force to this body with given parameters
                    rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius, explosionUpward);
                }
            }

        }

        void CreatePiece(int x, int y, int z)
        {

            //create piece
            GameObject piece;
            if (piecesPrefab != null)
            {
                piece = Instantiate(piecesPrefab);
            }
            else
            {
                piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
            }

            //Vector3 centerPos = target.transform.position;
            Vector3 centerPos = target.GetComponent<Renderer>().bounds.center;

            //set piece position and scale
            piece.transform.position = centerPos + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
            piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

            //add rigidbody and set mass
            Rigidbody rb = piece.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = piece.AddComponent<Rigidbody>();
            }
            rb.mass = cubeSize;
        }

    }
}