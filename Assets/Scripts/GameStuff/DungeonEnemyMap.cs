using UnityEngine;

public class DungeonEnemyMap : DungeonMap
{
    [Header("Doors")] 
    public Door[] doors;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if(other.CompareTag("Player"))
            CloseDoors();
    }

    public void CheckEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
            if (!enemies[i].IsDead())
                return;
        
        OpenDoors();
    }

    private void OpenDoors()
    {
        for (int i = 0; i < doors.Length; i++)
            doors[i].Open();
    }

    private void CloseDoors()
    {
        for (int i = 0; i < doors.Length; i++)
            doors[i].Close();
    }
}
