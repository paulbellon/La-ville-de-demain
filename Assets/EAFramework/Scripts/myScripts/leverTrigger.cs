using UnityEngine;

namespace EAFramework
{
    public class leverTrigger : Action
    {
        public GameObject lever, leverSystem, door;

        private Animator leverAnim, doorAnim;
        private Light leverLight;
        private Color upColor, downColor;

        public string leverPosition;

        void Start() {
          leverAnim = lever.GetComponentInChildren<Animator>();
          doorAnim = door.GetComponentInChildren<Animator>();


          leverLight = lever.GetComponentInChildren<Light>();

          upColor = new Color(0f, 0.42f, 1f, 1f);
          downColor = new Color(1f, 0.79f, 0f, 1f);

          setLeverPos("Up");
        }

        void setLeverPos(string newPos) {
          leverPosition = newPos;

          if (leverPosition == "Up") {
            leverLight.color = upColor;
          } else if (leverPosition == "Down") {
            leverLight.color = downColor;
          }
        }

        override public void Execute(string eventName)
        {
          if (leverPosition == "Up") {
            leverAnim.Play("Lever_Down");
            //door.GetComponentInChildren<doorOpen>().doorLock("Unlocked");
            setLeverPos("Down");
          } else {
            leverAnim.Play("Lever_Up");
            setLeverPos("Up");
          }

          leverSystem.GetComponentInChildren<leverCombination>().goodCode();
          
          if (leverSystem.GetComponentInChildren<leverCombination>().leverCode == "Resolved")
          {
            door.GetComponentInChildren<doorOpen>().doorLock("Unlocked");
          }
        }
    }
}
