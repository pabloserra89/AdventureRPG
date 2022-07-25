using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{
    [Header("Interactable Stuff")]
    public SignalSender contextOn;

    public GameObject dialogBox;
    public Text dialogText;

    protected GameObject player;
    
    public void Start()
    {
        StartCustom();
    }

    protected virtual void StartCustom() { }

    public virtual void Update()
    {
        if (Input.GetButtonDown("interact") && player != null)
        {
            Interact(player);
        }
    }

    protected abstract void Interact(GameObject player);

    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.CompareTag("Player"))
        {
            player = _other.gameObject;
            OnTriggerEnter2DCustom();
        }
    }

    protected virtual void OnTriggerEnter2DCustom()
    {
        contextOn.Raise();
    }

    void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.CompareTag("Player"))
        {
            player = null;
            OnTriggerExit2DCustom();
        }
    }

    protected virtual void OnTriggerExit2DCustom()
    {
        contextOn.Raise();

        if (dialogBox != null && dialogBox.activeInHierarchy)
            dialogBox.SetActive(false);
    }
}
