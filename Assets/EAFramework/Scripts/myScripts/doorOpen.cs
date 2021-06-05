using UnityEngine;

namespace EAFramework
{
    public class doorOpen : Action
    {
        public GameObject door;
        public GameObject button;

        private Animator anim;
        private Material buttonMaterial;
        private Light doorLight;
        private Color lockedColor, unlockedColor, openedColor;

        public string lockState;

        void Start() {
          anim = door.GetComponent<Animator>();
          buttonMaterial = button.GetComponent<Renderer>().material;
          doorLight = button.GetComponentInChildren<Light>();

          lockedColor = new Color(1f, 0f, 0f, 1f);
          unlockedColor = new Color(1f, 0.3f, 0f, 1f);
          openedColor = new Color(0f, 1f, 0f, 1f);

          doorLock("Unlocked");
        }

        public void doorLock(string lockChange) {
          lockState = lockChange;

          if (lockState == "Locked") {
            doorLight.color = lockedColor;
            buttonMaterial.SetColor("_EmissionColor", lockedColor);
          } else if (lockState == "Unlocked") {
            doorLight.color = unlockedColor;
            buttonMaterial.SetColor("_EmissionColor", unlockedColor);
          } else if (lockState == "Opened") {
            doorLight.color = openedColor;
            buttonMaterial.SetColor("_EmissionColor", openedColor);
          }
        }

        override public void Execute(string eventName)
        {
          if (lockState == "Unlocked") {
            anim.Play("Open");
            doorLock("Opened");
          }
        }

    }

}
