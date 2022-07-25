using UnityEngine;

public class SwitchDoor : Door
{
    protected override bool TryToOpen(GameObject player)
    {
        return false;
    }
}
