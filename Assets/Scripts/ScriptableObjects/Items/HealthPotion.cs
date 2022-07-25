using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item/HealthPotion")]
public class HealthPotion : ItemInventory
{
    public float hearts;

    public override void Use()
    {
        base.Use(); 
        GameController.player.Heal(hearts);
    }
}