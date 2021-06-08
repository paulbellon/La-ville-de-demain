using UnityEngine;
using TMPro;

public class endOfDemo : MonoBehaviour
{

  public Animator textControl;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other) {
      textControl.Play("To End");
    }
}
