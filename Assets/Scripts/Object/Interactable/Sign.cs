using UnityEngine;

public class Sign : Interactable
{
    [Header ("Sign Stuff")]
    public string dialog;

    protected override void Interact(GameObject player)
    {
        dialogText.text = dialog;
        
        if (dialogBox.activeInHierarchy)
            dialogBox.SetActive(false);
        else
            dialogBox.SetActive(true);
    }
}
