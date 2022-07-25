using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item/ItemInventory")]

public class ItemInventory : Item
{
    public int quantity;
    
    public bool isKey;
    public bool isUsable;
    public bool isUnique;

    public override void ItemPickedBy(GameObject _player)
    {
        _player.GetComponent<Player>().ItemPicked(this);
    }

    public virtual void Use()
    {
        if (isUsable)
            quantity--;
    }
}
