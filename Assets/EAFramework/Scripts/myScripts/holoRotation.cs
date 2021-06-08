using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EAFramework {

  public class holoRotation : MonoBehaviour
  {
    public AudioSource audioSource;
    public float rotationSpeed;
    public GameObject target;

    private Light holoLight;

    Color lerpedColor, startColor, endColor;
    Renderer holoRenderer;

      void Start() {
        holoRenderer = target.GetComponent<Renderer>();
        holoLight = target.transform.GetComponentInChildren<Light>();

        startColor = new Color(0f, 0.34f, 1f, 1f);
        endColor = new Color(1f, 0.17f, 0f, 1f);
      }

      void Update()
      {
          if (audioSource.isPlaying) {
            transform.Rotate(0f, rotationSpeed, 0f, Space.World);

            lerpedColor = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time, 3));
            holoRenderer.material.SetColor("_col", lerpedColor);

            holoLight.color = lerpedColor;
          } else if (!audioSource.isPlaying) {
            transform.Rotate(0f, 0f, 0f, Space.World);

            holoRenderer.material.SetColor("_col", startColor);
            holoLight.color = startColor;
          }
      }
  }

}
