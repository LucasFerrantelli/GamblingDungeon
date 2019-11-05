using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_newPersona", menuName = "ScriptableObjects/Persona")]
public class Persona : ScriptableObject
{
    public int baseLife;
    public List<Cards> baseDeck;
    public Trinket baseTrinket;
    public Ladder baseLadder;
}
