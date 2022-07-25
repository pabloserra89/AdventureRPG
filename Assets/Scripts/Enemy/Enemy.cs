using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("State")]
    public CharacterState characterState;

    [Header("Attributes")]
    public string enemyName;
    public float speed;

    [Header("Target")]
    public float chaseRadius;
    public float attackRadius;
    public Collider2D boundary;

    [Header("Signal")]
    public SignalSender unlockRoomSignal;

    [Header("Loot")]
    public LootTable myLoot;

    [Header("Components")]
    public Collider2D myHurtBox;
    protected Collider2D myCollider;
    protected Rigidbody2D myRigidbody;
    protected Animator myAnimator;
    protected Transform target;

    private Vector2 homePosition;

    public void Awake()
    {
        homePosition = transform.position;

        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        target = GameController.playerTransform;
    }

    public void FixedUpdate()
    {
        if (characterState.state != State.dead)
            CheckDistanceAndAction();
    }

    protected virtual void OnEnable()
    {
        transform.position = homePosition;

        characterState.state = State.sleeping;
        myHurtBox.enabled = true;
        myCollider.enabled = true;
    }

    protected virtual void CheckDistanceAndAction()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= chaseRadius && distance > attackRadius && boundary.bounds.Contains(target.position))
            PlayerInRangeAction();
        else if (distance < attackRadius && boundary.bounds.Contains(target.position))
            Attack();
        else if (distance > chaseRadius || !boundary.bounds.Contains(target.position))
            PlayerFarAwayAction();
    }

    protected abstract void PlayerInRangeAction();
    protected abstract void Attack();
    protected abstract void PlayerFarAwayAction();

    protected void ChangeAnimation(Vector2 direction)
    {
        Vector2 discreteDirection;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
                discreteDirection = Vector2.right;
            else
                discreteDirection = Vector2.left;
        }
        else
        {
            myAnimator.SetFloat("moveX", 0);

            if (direction.y > 0)
                discreteDirection = Vector2.up;
            else
                discreteDirection = Vector2.down;
        }

        myAnimator.SetFloat("moveX", discreteDirection.x);
        myAnimator.SetFloat("moveY", discreteDirection.y);
    }

    public void Die()
    {
        myHurtBox.enabled = false;
        myCollider.enabled = false;

        characterState.state = State.dead;

        StartCoroutine(DeadCo());

        if (unlockRoomSignal != null)
            unlockRoomSignal.Raise();
    }

    private IEnumerator DeadCo()
    {
        myAnimator.SetBool("dead", true);
        yield return null;

        myAnimator.SetBool("dead", false);
    }

    protected void MoveAndUpdateAnimation(Vector3 targetPosition)
    {
        if(CanWalk())
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);
            ChangeAnimation(newPosition - transform.position);
            myRigidbody.MovePosition(newPosition);
        }
    }

    public bool IsDead()
    {
        return characterState.state == State.dead;
    }

    /* This method is call from the Destroy animation*/
    private void Deactivate()
    {
        if (myLoot != null)
            myLoot.CreateLoot(transform.position);

        this.gameObject.SetActive(false);
    }

    protected bool CanWalk()
    {
        return characterState.state != State.dead
            && characterState.state != State.stagger;
    }

    protected bool CanAttack()
    {
        return characterState.state != State.attack
            && characterState.state != State.dead
            && characterState.state != State.stagger;
    }
}
