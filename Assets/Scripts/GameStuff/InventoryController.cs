using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class InventoryController : MonoBehaviour
{
    [Header("Event System")] 
    public EventSystem eventSystem;
    
    [Header("Player Inventory")]
    public Inventory inventory;

    [Header("Inventory Content")]
    public Transform contentTransform;

    [Header("Inventory Slot Prefab")]
    public InventorySlot inventorySlotPrefab;

    [Header("Inventory Item Description")]
    public Text itemDescriptionText;

    [Header("Use Cancel Panel")]
    public GameObject useCancelPanel;
    public Button useButton;

    private List<InventorySlot> slots = new List<InventorySlot>();
    private InventorySlot slotSelected;

    public void Awake()
    {
        UpdateUI();
    }

    public void OnEnable()
    {
        GameController.player.characterState.state = State.interact;
        UIController.AssignEventSystem(eventSystem, GetFirstItem());
    }

    public void OnDisable()
    {
        useCancelPanel.SetActive(false);
        GameController.player.characterState.state = State.idle;
        UIController.UnassignEventSystem(eventSystem);
    }

    public void UpdateUI()
    {
        foreach(ItemInventory ii in inventory.items)
        {
            if (slots.Exists(slot => slot.item == ii))
                UpdateItem(ii);
            else
                AddItem(ii);
        }

        inventory.UpdateInventory();
    }

    private void AddItem(ItemInventory itemInventory)
    {
        if (itemInventory.quantity <= 0)
            return;
        
        InventorySlot slot = Instantiate(inventorySlotPrefab, contentTransform).GetComponent<InventorySlot>();
        slot.SetItem(itemInventory, this);

        slots.Add(slot);
    }

    private void UpdateItem(ItemInventory itemInventory)
    {
        int index = slots.IndexOf(slots.Find(slotAux => slotAux.item == itemInventory));
        InventorySlot slot = slots[index];

        // Actualizo el número de items
        slot.quantityText.text = itemInventory.quantity.ToString();

        // Si la cantidad es menor igual a cero, borro el item del inventario
        if (slot.item.quantity <= 0)
        {
            slots.Remove(slot);
            Destroy(slot.gameObject);

            // Por defecto selecciono el primer item cuando se acaba un item
            if (slots.Count > 0)
            {
                if (slots.Count == index)
                    slot = slots[index-1];
                else if (slots.Count > index)
                    slot = slots[index];
                else
                    slot = slots[0];

                itemDescriptionText.text = slot.item.description;
                UIController.AssignEventSystem(eventSystem, slot.GetComponent<Button>());
            }
        }   
    }

    public Button GetFirstItem()
    {
        if (slots.Count == 0)
            return null;

        itemDescriptionText.text = slots[0].item.description;
        return slots[0].GetComponent<Button>();
    }

    public void SetDescription(string _description)
    {
        itemDescriptionText.text = _description;
    }

    public void ItemSelected(InventorySlot _slotSelected)
    {
        slotSelected = _slotSelected;
        UIController.AssignEventSystem(eventSystem, useButton);
        useCancelPanel.SetActive(true);
    }

    public void UseSelectedItem()
    {
        slotSelected.item.Use();
        CancelSelectedItem(true);
    }

    public void CancelSelectedItem(bool updateUI)
    {
        UIController.AssignEventSystem(eventSystem, slotSelected.GetComponent<Button>()); 
        slotSelected = null;            
        useCancelPanel.SetActive(false);

        if(updateUI)
            UpdateUI();
    }
}
