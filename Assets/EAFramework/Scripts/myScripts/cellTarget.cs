using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EAFramework
{
  public class cellTarget : ActionGrab
  {
    public GameObject energyWall, lightning, door, generator;

    private Material engravedLight, engravedLight2;
    private Color energyOn, energyOn2;

    public AudioClip[] energySounds;
    private AudioSource energySound;
    private AudioSource cellSound;

    private string energyLevel;

      void Start()
      {
        engravedLight = energyWall.GetComponent<Renderer>().material;
        engravedLight2 = generator.GetComponent<Renderer>().material;
        energyOn = new Color(0f, 0.25f, 1f, 0f);
        energyLevel = "Off";

        energySound = this.GetComponent<AudioSource>();
        energySound.clip = energySounds[0];
        energySound.Play();

        cellSound = rb.gameObject.GetComponent<AudioSource>();
      }

      void Update()
      {
        var amplitude = Mathf.PingPong(Time.time, 1.5f) + 2f;
        energyOn2 = energyOn * amplitude;

        if (energyLevel == "On")
        {
           engravedLight.SetColor("_EmissionColor", energyOn2);
           engravedLight2.SetColor("_EmissionColor", energyOn2);
        }
      }

      override public void Execute(string eventName)
      {
        if (rb.gameObject.GetComponent<FixedJoint>())
        {
          rb.transform.position = eventSource.transform.position;
          rb.transform.rotation = Quaternion.identity;
          rb.isKinematic = true;
          cellSound.Play();

          energyLevel = "On";

          energySound.clip = energySounds[1];
          energySound.Play();

          lightning.SetActive(true);

          door.GetComponentInChildren<doorEnergy>().doorLock("Unlocked");

          Destroy(rb.gameObject.GetComponent<FixedJoint>());
        }
      }

  }
}
