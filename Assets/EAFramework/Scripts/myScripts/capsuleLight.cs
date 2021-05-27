using UnityEngine;

public class capsuleLight : MonoBehaviour
{

  private Light energy;
  private GameObject energySphere;

  private Vector3 scaleChange;

  float originalRange;
  float originalIntensity;

    void Start()
    {
        energy = this.GetComponentInChildren<Light>();
        originalRange = energy.range;
        originalIntensity = energy.intensity;

        energySphere = this.transform.GetChild(1).gameObject;
    }

    void Update()
    {
      scaleChange = new Vector3(Mathf.PingPong(Time.time, 0.75f) + 1f, Mathf.PingPong(Time.time, 0.75f) + 1f, Mathf.PingPong(Time.time, 0.75f) + 1f);
      energySphere.transform.localScale = scaleChange;

      var amplitude = Mathf.PingPong(Time.time, 0.75f) + 1f;
      energy.range = originalRange * amplitude;
      energy.intensity = originalIntensity * amplitude;
    }

}
