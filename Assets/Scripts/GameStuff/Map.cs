using UnityEngine;

public class Map : MonoBehaviour
{
    [Header("Enemies")]
    public Enemy[] enemies;

    [Header("Breakable")]
    public Breakable[] breakables;

    [Header("Virtual Camera")]
    public GameObject virtualCamera;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateAll(true);
            virtualCamera.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateAll(false);
            virtualCamera.SetActive(false);
        }
    }

    private void ActivateAll(bool activate)
    {
        for (int i = 0; i < enemies.Length; i++)
            enemies[i].gameObject.SetActive(activate);

        for (int i = 0; i < breakables.Length; i++)
            breakables[i].gameObject.SetActive(activate);
    }
}
