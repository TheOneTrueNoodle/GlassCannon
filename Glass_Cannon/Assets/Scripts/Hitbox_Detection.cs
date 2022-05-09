using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox_Detection : MonoBehaviour
{
    public int MaxDamage;
    public int MinDamage;

    public bool DealtDamage;

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Hit");
        if (col.gameObject.GetComponent<EnemyStats>())
        {
            Debug.Log("OUCH");
            int damage = Random.Range(MinDamage, MaxDamage);
            col.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
        }
        else if (col.gameObject.CompareTag("Destructable"))
        {
            col.gameObject.GetComponent<Destructable>().breakObject();
        }
    }

    public void DealDamage(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            DealtDamage = true;
            Debug.Log("OUCH");
            int damage = Random.Range(MinDamage, MaxDamage);
            other.GetComponent<EnemyStats>().TakeDamage(damage);
        }
        else if (other.CompareTag("Destructable"))
        {
            other.GetComponent<Destructable>().breakObject();
        }
    }
}
