using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public int FallDamage = 3;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.transform.position = new Vector3(4.75f, -2f, 8.75f);
            col.gameObject.GetComponent<Player>().Fall(FallDamage);
        }
    }
}
