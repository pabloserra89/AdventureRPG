using UnityEngine;

public abstract class GenericHealth : MonoBehaviour
{
    public abstract void Heal(float amountToHeal);

    public abstract void FullHeal();

    public abstract void Damage(float damage);
}