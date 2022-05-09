using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int MaxHP;
    public int CurrentHP;
    public int MaxDamage;
    public int MinDamage;

    public void Start()
    {
        CurrentHP = MaxHP;
    }

    public void Update()
    {
        if(CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }

        if(CurrentHP <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
