using UnityEngine;

public class LiquidSurface : MonoBehaviour
{
    public Animator animator;

    public float fill = 0;

    public float collisionIncrement = 0.005f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        animator?.SetFloat("fill", fill);
    }

    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log("OnParticleCollision " + other.name);
        fill = Mathf.Min(1f, fill + collisionIncrement);
    }

}
