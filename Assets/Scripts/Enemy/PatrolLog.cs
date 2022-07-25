using UnityEngine;

public class PatrolLog : Log
{
    public Transform[] path;
    private int currentPoint;
    
    private readonly float deltaDistance = 0.1f;

    protected override void OnEnable()
    {
        base.OnEnable();

        characterState.state = State.walk;
        myAnimator.SetBool("wakeUp", true);
    }

    protected override void PlayerFarAwayAction()
    {
        if (Vector3.Distance(transform.position, path[currentPoint].position) < deltaDistance)
            ChangePoint();
        
        MoveAndUpdateAnimation(path[currentPoint].position);
    }

    private void ChangePoint()
    {
        if (currentPoint == path.Length - 1)
            currentPoint = 0;
        else
            currentPoint++;
    }
}
