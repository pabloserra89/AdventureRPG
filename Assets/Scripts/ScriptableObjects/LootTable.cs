using UnityEngine;

[System.Serializable]
public class Loot
{
    public Item item;
    public int probability;
}

[CreateAssetMenu(menuName = "ScriptableObjects/LootTable")]
public class LootTable : ScriptableObject
{
    public Loot[] loots;

    private Item GetLoot()
    {
        int cumProb = 0;
        int random = Random.Range(0, 100);

        for(int i = 0; i < loots.Length; i++)
        {
            cumProb += loots[i].probability;
            if (random <= cumProb)
                return loots[i].item;
        }

        return null;
    }

    public void CreateLoot(Vector3 position)
    {
        Item itemScriptable = GetLoot();

        if (itemScriptable != null)
        {
            ItemPhysical item = Instantiate(itemScriptable.GetItem(), position, Quaternion.identity).GetComponent<ItemPhysical>();
            item.AssignItem(itemScriptable);
        }
    }
}
