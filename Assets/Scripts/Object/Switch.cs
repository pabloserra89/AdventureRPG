using UnityEngine;

public class Switch : MonoBehaviour
{
    public Door door;
    public BoolValue active;
    public Sprite activeSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (active.value)
            Active();
    }

    void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.CompareTag("Player"))
            if (!active.value)
                Active();
    }

    private void Active()
    {
        active.value = true;
        spriteRenderer.sprite = activeSprite;
        door.Open();
    }
}
