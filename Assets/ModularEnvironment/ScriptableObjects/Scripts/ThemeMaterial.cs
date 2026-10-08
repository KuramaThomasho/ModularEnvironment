using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ThemeMaterial", menuName = "Scriptable Objects/ThemeMaterial")]
public class ThemeMaterial : ScriptableObject
{
    [Header("INSPECTOR NOTE")]
    [Space]
    [Header("The Materials need to be indexed by the Theme enum.")]
    [Space]
    public List<Material> wallMaterials;
    public List<Material> floorMaterials;
    public List<Material> ceilingMaterials;
    public List<Material> wallArtMaterials;
    public List<Material> doorFrameMaterials;
    public List<Material> windowFrameMaterials;
}
