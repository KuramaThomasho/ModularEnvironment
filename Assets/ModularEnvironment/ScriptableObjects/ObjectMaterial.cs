using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ObjectMaterial", menuName = "Scriptable Objects/ObjectMaterial")]
public class ObjectMaterial : ScriptableObject
{
    [Header("Add the certain different objects into this scriptable object to make sure things are organized")]
    [Space]
    [Header("Make sure the index matches the ENUM theme")]
    [Space]
    public List<Material> material;
};
