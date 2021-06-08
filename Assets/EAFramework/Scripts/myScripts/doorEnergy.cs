using UnityEngine;

namespace EAFramework
{
  public class doorEnergy : Action
  {
    public GameObject door;
    public GameObject button;

    private Animator anim;
    private Material buttonMaterial;
    private Light doorLight;
    private Color unlockedColor, openedColor;
    private AudioSource doorSound;

    public string lockState;

    void Start() {
      anim = door.GetComponent<Animator>();
      buttonMaterial = button.GetComponent<Renderer>().material;
      doorLight = button.GetComponentInChildren<Light>();
      doorSound = door.GetComponent<AudioSource>();

      unlockedColor = new Color(1f, 0.3f, 0f, 1f);
      openedColor = new Color(0f, 1f, 0f, 1f);
    }

    public void doorLock(string lockChange) {
      lockState = lockChange;

      if (lockState == "Unlocked") {
        doorLight.enabled = true;
        buttonMaterial.EnableKeyword("_EMISSION");
      } else if (lockState == "Opened") {
        doorLight.color = openedColor;
        buttonMaterial.SetColor("_EmissionColor", openedColor);
      }
    }

    override public void Execute(string eventName)
    {
      if (lockState == "Unlocked") {
        anim.Play("Open");
        doorSound.Play();
        doorLock("Opened");
      }
    }
  }

}
