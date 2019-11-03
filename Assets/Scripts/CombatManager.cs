using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public Belligerent[] belligerents;


    // Start is called before the first frame update
    void Start()
    {
        belligerents[0].opponent = belligerents[1];
        belligerents[1].opponent = belligerents[0];
        belligerents[0].combatManager = this;
        belligerents[1].combatManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseCard(Belligerent owner, int cardNumber)
    {
        Cards _cardPlayed = owner.hand[cardNumber];
        //Add Power
        owner.AddPower(_cardPlayed.cost[0]);
        //Read All the effect in order.
        for (int i = 0; i < _cardPlayed.effects.Length; i++)
        {
            ReadEffect(_cardPlayed.effects[i]);
        }

        //Remove Card from Hand
        if(_cardPlayed.nbrOfUtilisation == 0)
        {
            owner.hand.Remove(_cardPlayed);
        }
        
    }

    public void ReadEffect(EffectStruct effectStruct)
    {

    }

}
