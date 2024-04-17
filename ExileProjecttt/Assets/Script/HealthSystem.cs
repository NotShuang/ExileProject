using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int health;
    private int healthMax;

    public HealthSystem(int healthMAx)
    {
        this.healthMax = healthMAx;
        health = healthMAx;
    }

    public int GetHealth()
    {
        return health;
    }
    public float GetHealthPercent ()
    {
        return health / healthMax;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health < 0) health = healthMax;
    }
}


