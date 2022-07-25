using System.Collections.Generic;

[System.Serializable]
public class ItemData
{
    public string name;
    public int quantity;

    public ItemData(string _name, int _quantity)
    {
        name = _name;
        quantity = _quantity;
    }
}

[System.Serializable]
public class SaveData
{
    // Scene Stuff
    public string currentScene;
    public bool houseTimeline;

    // Player Stuff
    public bool playerMage;
    public float playerPositionX;
    public float playerPositionY;
    public float playerCurrentHealth;
    public float playerMaxHealth;
    public float playerCurrentMana;
    public float playerMaxMana;
    public int playerCoins;

    // Inventory
    public int numberOfKeys;
    public List<ItemData> items;

    public void SaveInventory(Inventory inventory)
    {
        numberOfKeys = inventory.numberOfKeys;
        items = new List<ItemData>();

        foreach (ItemInventory item in inventory.items)
            items.Add(new ItemData(item.name, item.quantity));
    }

    public void GetInventory(Inventory _inventory)
    {
        _inventory.numberOfKeys = numberOfKeys;
        _inventory.items = new List<ItemInventory>();

        foreach (ItemData itemSaved in items)
        {
            ItemInventory item = InventoryManager.instance.GetItemByName(itemSaved.name);
            item.quantity = itemSaved.quantity;
            _inventory.items.Add(item);
        }   
    }
}
