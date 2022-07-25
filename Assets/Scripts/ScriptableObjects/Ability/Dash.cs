using System.Collections;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Ability/Dash")]
public class Dash : GenericAbility
{
    public float dashForce;
    public float dashTime;

    private Player player;
    private Rigidbody2D rigidbody;

    public override void Execute()
    {
        if(!player)
            player = GameController.player;

        if(!rigidbody)
            rigidbody = player.gameObject.GetComponent<Rigidbody2D>();

        rigidbody.AddForce(player.direction.normalized * dashForce, ForceMode2D.Impulse);
        player.StartCoroutine(DashCo());
    }

    private IEnumerator DashCo()
    {
        yield return new WaitForSeconds(dashTime);
        rigidbody.velocity = Vector2.zero;
        player.characterState.state = State.idle;
    }
}
