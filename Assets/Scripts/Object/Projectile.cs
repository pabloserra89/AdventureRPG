using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile")]
    public float speed;

    [Header("Mana")]
    public float manaCost;

    private Rigidbody2D myRigidbody;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.SetActive(false);
    }

    public void Launch(Vector2 position, Vector2 direction)
    {
        transform.position = position;
        gameObject.SetActive(true);

        myRigidbody.velocity = direction.normalized * speed;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
    }

    public bool IsAvailable()
    {
        return !gameObject.activeInHierarchy;
    }
}
