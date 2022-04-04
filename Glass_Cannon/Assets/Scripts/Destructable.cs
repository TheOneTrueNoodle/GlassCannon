using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destroyedVersion;
    public int ID;
    public DestructablePositionData Data;

    private void Start()
    {
        gameObject.transform.position = Data.DestructablePositions[ID];
        gameObject.transform.localScale = Data.DestructableScale[ID];
    }

    public void FixedUpdate()
    {
        Data.DestructablePositions[ID] = gameObject.transform.position;
        Data.DestructableScale[ID] = gameObject.transform.localScale;
    }

    public void breakObject()
    {
        GameObject NewObj = Instantiate(destroyedVersion, transform.position, transform.rotation);
        NewObj.transform.localScale = gameObject.transform.localScale;
        Destroy(gameObject);
    }
}
