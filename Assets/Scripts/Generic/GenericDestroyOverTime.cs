using UnityEngine;

public class GenericDestroyOverTime : MonoBehaviour
{
    public float lifetime;
    private float lifetimeSeconds;

    void OnEnable()
    {
        lifetimeSeconds = lifetime;
    }

    void Update()
    {
        lifetimeSeconds -= Time.deltaTime;

        if(lifetimeSeconds < 0)
        {
            transform.Rotate(Vector3.zero);
            gameObject.SetActive(false);
        }
    }
}
