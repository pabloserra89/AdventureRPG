using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    [Header("Pause Stuff")]
    public GameObject pausePanel;        

    [Header("Inventory Stuff")]
    public InventoryController inventoryPanel;
    public Inventory inventory;

    void Awake()
    {
        pausePanel.SetActive(false);

        inventoryPanel.gameObject.SetActive(false);
        inventoryPanel.UpdateUI();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            // Si el inventario está abierto, con Escape lo cierro también
            if (inventory.isOpen)
                Inventory();
            else
                Pause();
        }            
        else if (Input.GetButtonDown("inventory"))
        {
            // Si no está en pausa, abro/cierro el inventario
            if(!pausePanel.activeInHierarchy)
                Inventory();
        }            
    }

    private void Inventory()
    {
        inventory.isOpen = !inventory.isOpen;
        inventoryPanel.gameObject.SetActive(inventory.isOpen);
    }

    private void Pause()
    {
        pausePanel.SetActive(!pausePanel.activeInHierarchy);
    }

    public static void AssignEventSystem(EventSystem eventSystem, Button btn)
    {
        if (btn == null)
            return;

        eventSystem.firstSelectedGameObject = btn.gameObject;
        eventSystem.SetSelectedGameObject(btn.gameObject);

        btn.Select();
        btn.OnSelect(null);
    }

    public static void UnassignEventSystem(EventSystem eventSystem)
    {
        eventSystem.firstSelectedGameObject = null;
        eventSystem.SetSelectedGameObject(null);
    }
}
