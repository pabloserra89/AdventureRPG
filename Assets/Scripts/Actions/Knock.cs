using UnityEngine;

public class Knock : MonoBehaviour
{
    public float thrust;
    public float knockTime;

    void OnTriggerEnter2D(Collider2D other)
    {
        Knockeable knockeable = other.gameObject.GetComponentInParent<Knockeable>();

        if(knockeable)
            knockeable.Knocked(thrust, knockTime, transform.position);
    }
}
