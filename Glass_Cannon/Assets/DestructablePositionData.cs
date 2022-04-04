using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Destructable Positions Data")]
public class DestructablePositionData : ScriptableObject
{
    public List<Vector3> DestructablePositions;
    public List<Vector3> DestructableScale;
    public List<int> DestructableID;
}
