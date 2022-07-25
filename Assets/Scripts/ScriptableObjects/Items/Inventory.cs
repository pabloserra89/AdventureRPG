using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item/Inventory")]
[System.Serializable]
public class Inventory : ScriptableObject
{
    [Header("Items")]
    public SignalSender inventoryUISignal; 
    public List<ItemInventory> items = new List<ItemInventory>();    

    [Header("Keys")]
    public int numberOfKeys;

    [Header("Coins")]
    public IntValue playerCoins;
    public SignalSender coinsSignal;

    [Header("Attributes")]
    public bool isOpen;

    public void NewItem(ItemInventory item)
    {
        if (item == null)
            return;

        if (item.isKey)
        {   
            numberOfKeys++;
        }
        else
        {
            item.quantity++; 
            
            if (!items.Contains(item))
                items.Add(item);
        }

        inventoryUISignal.Raise();
    }

    public void UpdateInventory()
    {
        while (true)
            if (!items.Remove(items.Find(item => item.quantity == 0)))
                break;
    }

    public bool UseKey()
    {
        if (numberOfKeys > 0)
        {
            numberOfKeys--;
            return true;
        }
        else
            return false;
    }

    public void CoinPicked(int _coins)
    {
        playerCoins.value += _coins;
        coinsSignal.Raise();
    }
}
