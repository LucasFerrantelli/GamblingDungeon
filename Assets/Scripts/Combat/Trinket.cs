using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_newTrinket", menuName = "ScriptableObjects/Trinket")]
public class Trinket : ScriptableObject
{
    public Effects.TrinketEffect effect;
    public Effects.TrinketProke prokeType;
    public Effects.Parameter parameter;
    public int parameterValue;
}
