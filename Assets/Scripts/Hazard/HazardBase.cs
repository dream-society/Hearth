using UnityEngine;

public class HazardBase : MonoBehaviour, IHazard
{
    protected bool canDamage = true;

    public virtual void Damage()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (canDamage)
            {
                Damage();
            }
        }
    }
}
