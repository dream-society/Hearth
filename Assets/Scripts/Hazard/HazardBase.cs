using UnityEngine;
using Hearth.Player;

public class HazardBase : MonoBehaviour, IHazard
{
    protected bool canDamage = true;
    public int damage = 1;

    public virtual void Damage(CharacterController2D player)
    {
        player.GetDamaged(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (canDamage)
            {
                CharacterController2D player = collision.GetComponent<CharacterController2D>();
                Damage(player);
            }
        }
    }
}
