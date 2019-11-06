using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    public int life;
    public List<Cards> deck;
    public List<Cards> inventory;
    public Trinket trinket;
    public Ladder ladder;

    public Persona persona;

    public void GiveReferenceToMacroManager()
    {
        MacroManager.player = this;
    }

    void Start()
    {
        GiveReferenceToMacroManager();
        ReadValueFromPersona(persona);
    }

    public void ReadValueFromPersona(Persona _persona)
    {
        life = _persona.baseLife;
        deck = _persona.baseDeck;
        trinket = _persona.baseTrinket;
        ladder = _persona.baseLadder;
    }
}
