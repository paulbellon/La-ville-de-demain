using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EAFramework
{

public class triggerAudio : Action
{
  public AudioSource audioSource;
  public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = target.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    override public void Execute(string eventName)
    {
        audioSource.Play();
    }
}
}
