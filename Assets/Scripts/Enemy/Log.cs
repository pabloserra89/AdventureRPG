using UnityEngine;

public class Log : Enemy
{
    protected override void PlayerInRangeAction()
    {
        if (characterState.state == State.idle || characterState.state == State.sleeping || characterState.state == State.walk)
        {
            characterState.state = State.walk;
            myAnimator.SetBool("wakeUp", true);

            MoveAndUpdateAnimation(target.position);
        }
    }

    protected override void Attack() {}

    protected override void PlayerFarAwayAction()
    {
        myAnimator.SetBool("wakeUp", false);
    }
}
