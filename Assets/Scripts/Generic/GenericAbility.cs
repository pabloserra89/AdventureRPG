using UnityEngine;

public enum AbilityType
{
    beam, 
    movement,
    projectile,    
    other
}

[System.Serializable]
public abstract class GenericAbility : ScriptableObject
{
    public AbilityType abilityType;

    public abstract void Execute();
}
