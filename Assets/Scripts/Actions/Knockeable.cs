using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Knockeable : MonoBehaviour
{
    public CharacterState characterState;
    private Rigidbody2D myRigidbody;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    public void Knocked(float thrust, float knockTime, Vector3 knockerPosition)
    {
        if(characterState)
            characterState.state = State.stagger;

        Vector2 difference = transform.position - knockerPosition;
        myRigidbody.AddForce(difference.normalized * thrust, ForceMode2D.Impulse);

        if (gameObject.activeInHierarchy)
            StartCoroutine(KnockCo(knockTime));

        //StartCoroutine(FlashCo());
    }

    private IEnumerator KnockCo(float knockTime)
    {
        yield return new WaitForSeconds(knockTime);
        myRigidbody.velocity = Vector2.zero;

        if (characterState && characterState.state != State.dead)
            characterState.state = State.idle;
    }
}
