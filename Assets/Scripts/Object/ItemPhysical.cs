using UnityEngine;

public class ItemPhysical : MonoBehaviour
{
    [HideInInspector]
    public Item itemScriptable;

    public void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.CompareTag("Player"))
        {
            itemScriptable.ItemPickedBy(_other.gameObject);
            gameObject.SetActive(false);
        }
    }

    public virtual void AssignItem(Item _itemScriptable)
    {
        itemScriptable = _itemScriptable;
    }
}
