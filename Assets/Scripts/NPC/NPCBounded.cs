using UnityEngine;

public class NPCBounded : Sign
{
    [Header ("NPC Stuff")]
    public float speed;
    public float moveTime;
    public float waitTime;
    public Collider2D boundary;

    private bool isMoving;
    private Vector2 direction;
    private float moveTimeSeconds;
    private float waitTimeSeconds;

    private Rigidbody2D myRigidbody;
    private Animator myAnimator;

    protected override void StartCustom()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        moveTimeSeconds = moveTime;
        waitTimeSeconds = waitTime;

        ChangeDirection();
    }

    public override void Update()
    {
        base.Update();
        DecideMove();
    }

    private void DecideMove()
    {
        if (isMoving)
        {
            moveTimeSeconds -= Time.deltaTime;

            if (moveTimeSeconds < 0)
            {
                isMoving = false;
                myAnimator.SetBool("walking", false);

                moveTimeSeconds = moveTime;
            }
            else
            {
                if (player == null)
                    Move();
                else
                    myAnimator.SetBool("walking", false);
            }   
        }
        else
        {   
            waitTimeSeconds -= Time.deltaTime;

            if (waitTimeSeconds < 0)
            {
                ChangeDirection(); 
                
                isMoving = true;
                waitTimeSeconds = waitTime;
            }
        }
    }

    private void Move()
    {
        Vector2 newPosition = myRigidbody.position + direction.normalized * speed * Time.fixedDeltaTime;

        if (boundary.bounds.Contains(newPosition))
            myRigidbody.MovePosition(newPosition);
        else
            ChangeDirection();

        myAnimator.SetFloat("moveX", direction.x);
        myAnimator.SetFloat("moveY", direction.y);
        myAnimator.SetBool("walking", true);
    }

    private void ChangeDirection()
    {
        Vector2 tempDirection = Vector2.zero;
    
        int random = Random.Range(0, 4);

        switch(random)
        {
            case 0:
                tempDirection = Vector2.right;
                break;
            case 1:
                tempDirection = Vector2.up;
                break;
            case 2:
                tempDirection = Vector2.left;
                break;
            case 3:
                tempDirection = Vector2.down;
                break;
            default:
                ChangeDirection();
                break;
        }

        if (tempDirection == direction)
            ChangeDirection();
        else
            direction = tempDirection;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ChangeDirection();
    }
}
