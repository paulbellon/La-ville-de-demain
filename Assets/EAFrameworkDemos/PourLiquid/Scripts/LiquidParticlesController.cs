using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidParticlesController : MonoBehaviour
{
    [Tooltip("Min angle between vertical and particle system's z axis to start Emitter")]
    public float startAngle = 95;

    ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Rotate(30, 0, 0);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            transform.Rotate(-30, 0, 0);
        }

        float angle = Vector3.Angle(Vector3.up, transform.forward);
        if (angle > startAngle)
        {
            if (!ps.isPlaying) ps.Play();
        }
        else
        {
            if (ps.isPlaying) ps.Stop();
        }
    }

    private void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 60;
        float angle = Vector3.Angle(Vector3.up, transform.forward);
        GUI.Label(new Rect(25, 25, 200, 30), "angle: " + angle, guiStyle);
    }
}
