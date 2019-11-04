using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public Belligerent[] belligerents;

    public GameObject inGameCard;

    public static CombatManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        belligerents[0].opponent = belligerents[1];
        belligerents[1].opponent = belligerents[0];
        belligerents[0].combatManager = this;
        belligerents[1].combatManager = this;
    }


    public void UseCard(Belligerent owner, InGameCard _cardPlayed)
    {
        //Add Power
        owner.AddPower(_cardPlayed.cost[0]);
        //Read All the effect in order.
        for (int i = 0; i < _cardPlayed.effects.Length; i++)
        {
            if(ReadEffect(_cardPlayed.effects[i], owner))
            {

            }
            else
            {
                break;
            }
        }

        //Remove Card from Hand
        _cardPlayed.nbrOfUtilisation--;
        if(_cardPlayed.nbrOfUtilisation <= 0)
        {
            owner.hand.Remove(_cardPlayed);
        }
        
    }

    public bool ReadEffect(EffectStruct effectStruct, Belligerent owner)
    {
        switch (effectStruct.effect)
        {
            case Effects.Effect.AddPower:
                break;
            case Effects.Effect.DealDamage:
                break;
            case Effects.Effect.Draw:
                break;
            case Effects.Effect.Heal:
                break;
            case Effects.Effect.SetHp:
                break;
            case Effects.Effect.SetPower:
                break;




        }
        return true;
    }

}
