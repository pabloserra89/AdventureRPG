using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GenericDamage : MonoBehaviour
{
    public float damage;
    public List<string> tagToDamage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!tagToDamage.Contains(other.tag))
            return;

        GenericHealth genericHealthTemp = other.GetComponent<GenericHealth>();

        if(genericHealthTemp)
            genericHealthTemp.Damage(damage);
    }
}
