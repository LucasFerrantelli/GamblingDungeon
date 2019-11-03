using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_newDeck", menuName = "ScriptableObjects/Deck")]
public class Deck : ScriptableObject
{
    public List<Cards> cards;
}
