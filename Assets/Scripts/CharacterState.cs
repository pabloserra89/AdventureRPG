using UnityEngine;

public enum State
{
    ability,
    attack,
    dead,
    idle,
    interact,
    inTransition,
    sleeping,
    stagger,
    walk
}

public class CharacterState : MonoBehaviour
{
    public State state;
}
