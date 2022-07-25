using UnityEngine;

public class Chest : Interactable
{
    [Header("Chest Stuff")]
    public ItemInventory item;
    private bool isOpen;

    private Animator myAnimator;
    
    protected override void StartCustom()
    {
        myAnimator = GetComponent<Animator>();
        myAnimator.SetBool("opened", isOpen);
    }

    protected override void Interact(GameObject player)
    {
        if(!isOpen)
        {
            isOpen = true;
            myAnimator.SetBool("open", true);

            dialogText.text = item.description;
            dialogBox.SetActive(true);

            player.GetComponent<Player>().ReceiveItem(item);
            contextOn.Raise();

            item = null;
        }
        else
        {
            dialogBox.SetActive(false);
            player.GetComponent<Player>().ReceiveItemDone();
        }        
    }

    protected override void OnTriggerEnter2DCustom()
    {
        if(!isOpen)
            contextOn.Raise();
    }

    protected override void OnTriggerExit2DCustom()
    {
        if (!isOpen) 
            contextOn.Raise();

        if(dialogBox.activeInHierarchy)
            dialogBox.SetActive(false);
    }
}
