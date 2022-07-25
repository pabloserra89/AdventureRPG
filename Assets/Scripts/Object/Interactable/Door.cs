using UnityEngine;

public abstract class Door : Interactable
{
    public string dialog;    

    private bool open;

    protected override void Interact(GameObject player)
    {
        if (open)
            return;
        
        if(dialogBox.activeInHierarchy)
        {
            dialogBox.SetActive(false);
            return;
        }

        if(!TryToOpen(player))
        {
            dialogText.text = dialog;

            if (!dialogBox.activeInHierarchy)
                dialogBox.SetActive(true);
        }
    }

    protected abstract bool TryToOpen(GameObject player);

    public void Open()
    {
        open = true;
        gameObject.SetActive(false);
    }

    public void Close()
    {
        open = false;
        gameObject.SetActive(true);
    }
}
