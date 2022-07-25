using System.Collections;
using UnityEngine;

public class TurretLog : Log
{
    [Header("Turret")]
    public GameObject projectilePrefab;
    public float fireDelay;

    private Projectile[] projectiles;
    private bool canFire = true;

    protected override void Start()
    {
        base.Start();

        projectiles = new Projectile[2];
        projectiles[0] = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectiles[1] = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
    }    
    
    protected override void CheckDistanceAndAction()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance < attackRadius)
            PlayerInRangeAction();
        else if (distance > attackRadius)
            PlayerFarAwayAction();
    }

    protected override void PlayerInRangeAction()
    {
        if (characterState.state == State.idle || characterState.state == State.sleeping || characterState.state == State.attack)
        {
            characterState.state = State.attack;
            myAnimator.SetBool("wakeUp", true);

            if(canFire)
            {
                Vector3 distance = target.transform.position - transform.position;
                StartCoroutine(ShootCo(distance));
            }
        }
    }

    private IEnumerator ShootCo(Vector3 distance)
    {
        canFire = false;

        for (int i = 0; i < projectiles.Length; i++)
        {
            if (projectiles[i].IsAvailable())
            {
                projectiles[i].Launch(transform.position, distance);
                break;
            }
        }

        yield return new WaitForSeconds(fireDelay);
        canFire = true;
    }
}
