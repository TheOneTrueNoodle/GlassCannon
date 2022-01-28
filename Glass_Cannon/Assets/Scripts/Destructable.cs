using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destroyedVersion;

    public void breakObject()
    {
        GameObject NewObj = Instantiate(destroyedVersion, transform.position, transform.rotation);
        NewObj.transform.localScale = gameObject.transform.localScale;
        Destroy(gameObject);
    }
}
