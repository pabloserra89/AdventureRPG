using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item/PowerUp/Coin")]
public class Coin : Item
{
    public int coins;
    
    public override void ItemPickedBy(GameObject player)
    {
        player.GetComponent<Player>().CoinPicked(coins);
    }
}
