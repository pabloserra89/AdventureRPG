using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public ItemInventory bow;
    public ItemInventory sword;
    public ItemInventory smallHealthPotion;
    public ItemInventory smallManaPotion;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public ItemInventory GetItemByName(string itemName)
    {
        if(itemName.Equals("Bow"))        
            return bow;
        else if (itemName.Equals("Sword"))
            return sword;
        else if (itemName.Equals("SmallHealthPotion"))
            return smallHealthPotion;
        else if (itemName.Equals("SmallManaPotion"))
            return smallManaPotion;

        return null;
    }
}
