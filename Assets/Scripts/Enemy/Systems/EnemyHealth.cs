using UnityEngine;

public class EnemyHealth : GenericHealth
{
    [SerializeField] private float maxHealth;
    private float currentHealth;


    [SerializeField] private Enemy enemy;

    public void OnEnable()
    {
        currentHealth = maxHealth;


    }

    public override void Heal(float amountToHeal)
    {
        currentHealth += amountToHeal;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public override void FullHeal()
    {
        currentHealth = maxHealth;
    }

    public override void Damage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        { 
            currentHealth = 0;
            enemy.Die();
        }
    }
}
