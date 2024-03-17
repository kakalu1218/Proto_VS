using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePawnController : BaseController
{
    private float _pushForce = 10.0f;

    public int Hp { get; protected set; }
    public float Speed { get; protected set; }

    public void OnDamaged(GameObject attacker, int damage)
    {
        if (Hp <= 0)
        { 
            return;
        }

        Vector2 pushDirection = (transform.position - attacker.transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(pushDirection * _pushForce, ForceMode2D.Impulse);

        Hp -= damage;

        if (Hp <= 0)
        {
            OnDead();
        }
    }

    public virtual void OnDead()
    { 
    
    }
}
