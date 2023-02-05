using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int health;

    protected abstract void Attack();

    protected void Hurt(int damage)
    {
        health -= damage;
    }

    
}
