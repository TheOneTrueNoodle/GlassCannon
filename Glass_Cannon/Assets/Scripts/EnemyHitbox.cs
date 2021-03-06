using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public int MaxDamage;
    public int MinDamage;

    public bool DealtDamage;

    public int CurrentDamage;
    public bool IsAttacking;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player") && DealtDamage == false && IsAttacking == true)
        {
            DealDamage(col.gameObject);
        }
    }

    public void DealDamage(GameObject other)
    {
        DealtDamage = true;
        CurrentDamage = Random.Range(MinDamage, MaxDamage);
        other.GetComponent<Player>().TakeDamage(CurrentDamage);
    }
}