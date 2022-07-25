using UnityEngine;

public class KeyDoor : Door
{
    protected override bool TryToOpen(GameObject player)
    {
        if (player.GetComponent<Player>().UseKey())
        {
            Open();
            return true;
        }
        else
            return false;
    }
}
