using System.Collections;
using UnityEngine;

public class Ogre : Enemy
{
    protected override void PlayerInRangeAction()
    {
        if (characterState.state == State.idle || characterState.state == State.sleeping || characterState.state == State.walk)
        {
            characterState.state = State.walk;
            myAnimator.SetBool("walking", true);

            MoveAndUpdateAnimation(target.position);
        }
    }

    protected override void PlayerFarAwayAction()
    {
        myAnimator.SetBool("walking", false);
    }

    protected override void Attack()
    {
        if(CanAttack())
        {
            characterState.state = State.attack;
            StartCoroutine(AttackCo());
        }
    }

    private IEnumerator AttackCo()
    {
        myAnimator.SetBool("attacking", true);
        yield return null; // Esta línea espera un Frame

        myAnimator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.25f);

        characterState.state = State.idle;
    }
}
