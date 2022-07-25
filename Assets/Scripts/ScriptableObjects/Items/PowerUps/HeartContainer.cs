using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item/PowerUp/HeartContainer")]
public class HeartContainer : Item
{
    public override void ItemPickedBy(GameObject player)
    {
        player.GetComponent<Player>().HeartContainerPicked();
    }
}
