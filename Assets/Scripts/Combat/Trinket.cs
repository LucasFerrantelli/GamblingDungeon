using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_newTrinket", menuName = "ScriptableObjects/Trinket")]
public class Trinket : ScriptableObject
{
    public Effects.Trinket trinket;
    public Effects.Parameter parameter;
    public int parameterValue;
}
