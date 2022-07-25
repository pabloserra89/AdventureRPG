using UnityEngine;

public class PlayerHealth : GenericHealth
{
    [SerializeField] private Player player;
    [SerializeField] private FloatValue maxHealth;
    [SerializeField] private FloatValue currentHealth;
    
    [SerializeField] private SignalSender healthSignal;

    public void Start()
    {
        healthSignal.Raise();
    }

    public override void Heal(float amountToHeal)
    {
        currentHealth.value += amountToHeal;

        if (currentHealth.value > maxHealth.value)
            currentHealth.value = maxHealth.value;

        healthSignal.Raise();
    }

    public override void FullHeal()
    {
        currentHealth.value = maxHealth.value;

        healthSignal.Raise();
    }

    public override void Damage(float damage)
    {
        currentHealth.value -= damage;

        if (currentHealth.value < 0)
        {
            currentHealth.value = 0;

            player.Die();
        }

        healthSignal.Raise();
    }
}
