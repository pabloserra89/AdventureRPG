using UnityEngine;

public class EnemyDoor : Door
{
    protected override bool TryToOpen(GameObject player)
    {
        return false;
    }
}
