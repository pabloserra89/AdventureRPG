using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")] 
    public CharacterState characterState;
    public float speed;
    public BoolValue isMage;

    public Vector2Value playerStoragePosition;

    [Header("Health System")]
    public PlayerHealth playerHealth;

    [Header("Mana Stuff")]
    public FloatValue maxManaPosible;
    public FloatValue maxMana;
    public FloatValue currentMana;
    public SignalSender manaSignal;

    [Header("Inventory")]
    public Inventory inventory;
    public SpriteRenderer receivedItemSprite;

    [Header("Projectile")]
    public Projectile projectilePrefab;
    public float fireDelay;
    private Projectile[] projectiles;

    [Header("Invulnerability Stuff")]
    public Collider2D myHurtBox; 
    public Color flashColor;
    public float flashDuration;
    public int numberOfFlashes;

    [Header("Ability")]
    public GenericAbility ability;

    private Rigidbody2D myRigidbody;    
    private SpriteRenderer mySpriteRenderer;
    private Animator myAnimator;
    public Vector2 direction;

    private bool canFire = true;

    public void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();

        // Esto es porque el personaje siempre arranca mirando hacia abajo
        characterState.state = State.idle;
        myAnimator.SetFloat("moveX", 0f);
        myAnimator.SetFloat("moveY", -1f);

        // Seteo la posición del jugador con la posición guardaba en memoria
        transform.position = playerStoragePosition.value;

        projectiles = new Projectile[2];
        projectiles[0] = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectiles[1] = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
    }

    public void Start()
    {
        //healthSignal.Raise();
    }

    public void Update()
    {
        if (characterState.state == State.interact)
        {
            
        }
        else if (Input.GetButtonDown("attack") && CanAttack())
        {
            Attack();
        }
        else if (Input.GetButtonDown("ability") && CanShoot())
        {
            characterState.state = State.ability;
            ability.Execute();
        }
        else if (Input.GetButtonDown("magic") && CanShoot())
        {
            //characterState.state = State.;
            StartCoroutine(ShootCo());
        }
        else if (CanWalk())
        {
            direction = Vector2.zero;

            direction.x = Input.GetAxisRaw("Horizontal");
            direction.y = Input.GetAxisRaw("Vertical");

            if (direction != Vector2.zero)
                characterState.state = State.walk;
            else
                characterState.state = State.idle;
        }
    }

    public void FixedUpdate()
    {
        MoveAndUpdateAnimation();
    }

    private void MoveAndUpdateAnimation()
    {
        if (characterState.state == State.walk)
        {
            MovePlayer();
            myAnimator.SetFloat("moveX", Mathf.Round(direction.x));
            myAnimator.SetFloat("moveY", Mathf.Round(direction.y));
            myAnimator.SetBool("walking", true);
        }
        else
        {
            myAnimator.SetBool("walking", false);
        }
    }

    private void MovePlayer()
    {
        playerStoragePosition.value = myRigidbody.position + direction.normalized * speed * Time.fixedDeltaTime;
        myRigidbody.MovePosition(playerStoragePosition.value);
    }

    private void Attack()
    {
        characterState.state = State.attack;
        StartCoroutine(AttackCo());
    }

    private IEnumerator AttackCo()
    {
        myAnimator.SetBool("attacking", true);
        yield return null; // Esta línea espera un Frame

        myAnimator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.25f);

        characterState.state = State.idle;
    }

    private IEnumerator ShootCo()
    {
        if (currentMana.value >= projectilePrefab.manaCost)
        {
            canFire = false;

            currentMana.value -= projectilePrefab.manaCost;
            manaSignal.Raise();

            for (int i = 0; i < projectiles.Length; i++)
            {
                if (projectiles[i].IsAvailable())
                {
                    projectiles[i].Launch(transform.position, new Vector2(myAnimator.GetFloat("moveX"), myAnimator.GetFloat("moveY")));
                    break;
                }
            }

            yield return new WaitForSeconds(fireDelay);
            canFire = true;
        }
    }

    private IEnumerator FlashCo()
    {
        int numberOfFlashesTemp = 0;
        myHurtBox.enabled = false;

        Color regularColor = mySpriteRenderer.color;

        while (numberOfFlashesTemp < numberOfFlashes)
        {
            mySpriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            
            mySpriteRenderer.color = regularColor;
            yield return new WaitForSeconds(flashDuration);

            numberOfFlashesTemp++;
        }

        myHurtBox.enabled = true;
    }

    public void Heal(float hearts)
    {
        playerHealth.Heal(hearts);
    }

    public void HeartContainerPicked()
    {
        /*maxHealth.value++;

        if (maxHealth.value > maxHealthPosible.value)
            maxHealth.value = maxHealthPosible.value;*/

        //healthSignal.Raise();
    }

    public void RecoverMana(float mana)
    {
        currentMana.value += mana;

        if (currentMana.value > maxMana.value)
            currentMana.value = maxMana.value;

        manaSignal.Raise();
    }

    public void ManaCrystalPicked()
    {
        maxMana.value += 10;

        if (maxMana.value > maxManaPosible.value)
            maxMana.value = maxManaPosible.value;

        manaSignal.Raise();
    }

    public void CoinPicked(int coins)
    {
        inventory.CoinPicked(coins);
    }

    public void ReceiveItem(ItemInventory item)
    {
        characterState.state = State.interact;
        myAnimator.SetBool("receiveItem", true);

        if (item != null)
        {
            inventory.NewItem(item);
            receivedItemSprite.sprite = item.GetSprite();
        }
    }

    public void ReceiveItemDone()
    {
        if (characterState.state != State.interact)
            return;

        characterState.state = State.idle;
        myAnimator.SetBool("receiveItem", false);

        receivedItemSprite.sprite = null;
    }

    private bool CanAttack()
    {
        return characterState.state == State.idle
            || characterState.state == State.sleeping
            || characterState.state == State.walk;
    }

    private bool CanShoot()
    {
        return CanAttack() && canFire && isMage.value;
    }

    private bool CanWalk()
    {
        return characterState.state == State.idle || characterState.state == State.walk;
    }

    public void InTransition()
    {
        characterState.state = State.inTransition;
    }

    internal void ItemPicked(ItemInventory item)
    {
        inventory.NewItem(item);
    }

    public bool UseKey()
    {
        return inventory.UseKey();
    }

    public void Die()
    {
        Debug.Log("Game Over");
    }
}
