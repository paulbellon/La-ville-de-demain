using UnityEngine;

public class capsuleLight : MonoBehaviour
{

  private Light energy;
  private GameObject energySphere;


    void Start()
    {
        energy = this.GetComponentInChildren<Light>();
        energySphere = this.transform.GetChild(2).gameObject;
    }

    void Update()
    {
      energySphere.transform.localScale =;
    }

}
