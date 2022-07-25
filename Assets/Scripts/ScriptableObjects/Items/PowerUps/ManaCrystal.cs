using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item/PowerUp/ManaCrystal")]
public class ManaCrystal : Item
{
    public override void ItemPickedBy(GameObject player)
    {
        player.GetComponent<Player>().ManaCrystalPicked();
    }
}
