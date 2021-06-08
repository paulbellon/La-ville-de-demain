using UnityEngine;

namespace EAFramework
{
  public class leverCombination : Action
  {

    public GameObject lever1, lever2, lever3, door;

    public string leverCode;

      void Start()
      {
        //lever1.GetComponent<leverTrigger>();
        if (lever1.GetComponent<leverTrigger>().leverPosition == "Up")
        {
          Debug.Log("Levier levé !");
        } else if (lever1.GetComponent<leverTrigger>().leverPosition == "Down")
        {
          Debug.Log("Levier baissé !");
        }
      }

      public void goodCode()
      {
        if (lever1.GetComponent<leverTrigger>().leverPosition == "Down" && lever2.GetComponent<leverTrigger>().leverPosition == "Up" && lever3.GetComponent<leverTrigger>().leverPosition == "Down")
        {
          leverCode = "Resolved";
          if (leverCode == "Resolved")
          {
             door.GetComponentInChildren<doorLocked>().doorLock("Unlocked");
          } else {
            return;
          }
        } else {
          return;
        }
      }
  }
}
