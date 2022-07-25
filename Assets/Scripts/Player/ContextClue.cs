using UnityEngine;

public class ContextClue : MonoBehaviour
{
    private bool contextOn = false;

    public void EnableDisableContext()
    {
        if (contextOn)
            contextOn = false;
        else
            contextOn = true;

        this.gameObject.SetActive(contextOn);
    }
}
