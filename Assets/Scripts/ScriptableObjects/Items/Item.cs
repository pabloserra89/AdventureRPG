using UnityEngine;

public abstract class Item : ScriptableObject
{
    public ItemPhysical itemPhysical;

    public string itemName;

    [TextArea(3, 20)]
    public string description;

    public Sprite GetSprite()
    {
        return itemPhysical.gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public GameObject GetItem()
    {
        return itemPhysical.gameObject;
    }

    public abstract void ItemPickedBy(GameObject player);
}
