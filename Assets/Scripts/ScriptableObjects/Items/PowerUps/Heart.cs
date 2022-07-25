using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item/PowerUp/Heart")]
public class Heart : Item
{
    public override void ItemPickedBy(GameObject player)
    {
        player.GetComponent<Player>().Heal(1f);
    }
}
