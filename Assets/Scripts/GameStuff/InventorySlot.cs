using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, ISelectHandler
{
    [Header("Slot Stuff")]
    public Image itemImage;
    public Text quantityText;

    [Header("Item")]
    public ItemInventory item;

    private InventoryController inventoryController;

    public void SetItem(ItemInventory _itemInventory, InventoryController _inventoryController)
    {
        item = _itemInventory;
        itemImage.sprite = _itemInventory.GetSprite();
        quantityText.text = _itemInventory.quantity.ToString();

        inventoryController = _inventoryController;
    }

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        inventoryController.SetDescription(item.description);
    }

    public void SelectItem()
    {
        if(item.isUsable)
            inventoryController.ItemSelected(this);
    }
}
