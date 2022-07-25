using UnityEngine;

public class Breakable : MonoBehaviour
{
    public LootTable myLoot;
    
    private Animator animator;
    
    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Break()
    {
        animator.SetBool("break", true);
    }

    /* This method is call from the Destroy animation*/
    private void Deactivate()
    {
        if (myLoot != null)
            myLoot.CreateLoot(transform.position);

        gameObject.SetActive(false);
    }
}
