using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox_Detection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Destructable")
        {
            other.GetComponent<Destructable>().breakObject();
        }
    }
}
