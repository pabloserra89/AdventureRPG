using UnityEngine;

public class BreakableHealth : GenericHealth
{
    [SerializeField] private Breakable breakable;
    
    public override void Damage(float damage)
    {
        breakable.Break();
    }

    public override void FullHeal()
    {
        return;
    }

    public override void Heal(float amountToHeal)
    {
        return;
    }
}
