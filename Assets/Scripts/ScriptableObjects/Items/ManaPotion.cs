using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item/ManaPotion")]
public class ManaPotion : ItemInventory
{
    public float mana;

    public override void Use()
    {
        base.Use(); 
        GameController.player.RecoverMana(mana);
    }
}
