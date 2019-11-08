using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public enum GameState { Player1Playing, Player2Playing, Other}

    public GameState gameState;
    public Belligerent[] belligerents;

    public GameObject inGameCard;
    public CardDisposition[] cardDispositions;

    public PlayerDisplay playerDisplay;

    public static CombatManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        InitializeCombatParameters();
        playerDisplay.InitializeBar();
        belligerents[0].StartTurn();
    }
    //Set Instance, the opponents, create all belligerents cards
    public void InitializeCombatParameters()
    {
        Instance = this;
        belligerents[0].opponent = belligerents[1];
        belligerents[1].opponent = belligerents[0];
        belligerents[0].combatManager = this;
        belligerents[1].combatManager = this;

        belligerents[0].controlledByPlayer = true;
        belligerents[0].life = MacroManager.player.life;
        belligerents[0].trinket = MacroManager.player.trinket;
        belligerents[0].ladder = MacroManager.player.ladder;
        belligerents[0].initialDeck = MacroManager.player.deck;

        belligerents[1].controlledByPlayer = false;
        belligerents[1].life = MacroManager.opponent.baseLife;
        belligerents[1].trinket = MacroManager.opponent.baseTrinket;
        belligerents[1].ladder = MacroManager.opponent.baseLadder;
        belligerents[1].initialDeck = MacroManager.opponent.baseDeck;

        for (int i = 0; i < belligerents.Length; i++)
        {
            belligerents[i].InitializeCards();
        }
    }

    //End the turn of the belligerent, start the turn of the other one.
    public void EndTurn(int _belligerent)
    {
        gameState = GameState.Other;
        if (_belligerent == 0)
        {
            belligerents[_belligerent].EndTurn();
            StartTurn(1);
        }
        else
        {
            belligerents[_belligerent].EndTurn();
            StartTurn(0);
        }
    }

    public void StartTurn(int _belligerent)
    {
        
        if (_belligerent == 0)
        {
            gameState = GameState.Player1Playing;
            belligerents[_belligerent].StartTurn();
        }
        else
        {
            gameState = GameState.Player2Playing;
            belligerents[_belligerent].StartTurn();
        }

    }

    //Play a full card
    public void UseCard(Belligerent owner, InGameCard _cardPlayed)
    {
        
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
            owner.ResetCardPosition(_cardPlayed.gameObject);
            owner.hand.Remove(_cardPlayed);
        }
        owner.UseCard();
    }

    //Play the effectStruct
    public bool ReadEffect(EffectStruct effectStruct, Belligerent owner)
    {
        //Determine the value
        int _finalValue = 0;
        if(effectStruct.parameters.Length > 0)
        {
            int _intermediateValue = 0;
            for (int i = 0; i < effectStruct.parameters.Length; i++)
            {
                _intermediateValue = ParameterValue(effectStruct, i, owner);
                _finalValue = CombineValues(effectStruct.parameters[i].combinaisonType, _finalValue, _intermediateValue);
            }
        }
        //Determine the target
        List<Belligerent> targets = new List<Belligerent>();
        if(effectStruct.target == Effects.Target.Both)
        {
            targets.Add(belligerents[0]);
            targets.Add(belligerents[1]);
        }
        else
        {
            targets.Add(BelligerentDetermination(effectStruct.target, owner));
        }

        //Play the effect
        switch (effectStruct.effect)
        {
            case Effects.Effect.AddPower:
                for (int i = 0; i < targets.Count; i++)
                {
                    targets[i].AddPower(_finalValue, true);
                }
                break;
            case Effects.Effect.DealDamage:
                for (int i = 0; i < targets.Count; i++)
                {
                    targets[i].life -= _finalValue;
                }
                break;
            case Effects.Effect.Draw:
                for (int i = 0; i < targets.Count; i++)
                {
                    for (int j = 0; j < _finalValue; j++)
                    {
                        targets[i].DrawCard(false);
                    }
                }
                break;
            case Effects.Effect.Heal:
                for (int i = 0; i < targets.Count; i++)
                {
                    targets[i].life += _finalValue;
                }
                break;
            case Effects.Effect.SetHp:
                for (int i = 0; i < targets.Count; i++)
                {
                    targets[i].life = _finalValue;
                }
                break;
            case Effects.Effect.SetPower:
                for (int i = 0; i < targets.Count; i++)
                {
                    targets[i].power = _finalValue;
                }
                break;




        }
        return true;
    }

    //Find the determinate belligerent, depending on the owner and his target.
    Belligerent BelligerentDetermination(Effects.Target target, Belligerent owner)
    {
        Belligerent _belligerent = belligerents[0];
        if (target == Effects.Target.Himself)
        {
            if (owner == _belligerent)
            {

            }
            else
            {
                _belligerent = belligerents[1];
            }
        }
        else
        {
            if (owner == _belligerent)
            {
                _belligerent = belligerents[1];
            }
            else
            {

            }
        }
        return _belligerent;
    }

    //Get the paramValue of one parameter in one effectStruct
    int ParameterValue(EffectStruct effectStruct, int index, Belligerent owner)
    {
        //Assign the good paramOwner
        Belligerent _paramOwner = BelligerentDetermination(effectStruct.parameters[index].parametersOwner, owner);
        
        //Determine the Param Value
        switch (effectStruct.parameters[index].parameter)
        {
            case Effects.Parameter.Int:
                return effectStruct.parameters[index].parameterValue;
            case Effects.Parameter.Modifier:
                return _paramOwner.modifier;
            case Effects.Parameter.Power:
                return _paramOwner.power;
            case Effects.Parameter.CardsOnBoard:
                return _paramOwner.hand.Count;
            case Effects.Parameter.ActualHP:
                return _paramOwner.life;
            default:
                return 1;
        }
    }

    int CombineValues(Effects.CombinaisonType combinaisonType, int firstValue, int secondValue)
    {
        switch (combinaisonType)
        {
            case Effects.CombinaisonType.additive:
                return firstValue + secondValue;
            case Effects.CombinaisonType.soustractive:
                return firstValue - secondValue;
            case Effects.CombinaisonType.multiply:
                return firstValue * secondValue;
            case Effects.CombinaisonType.divide:
                return firstValue / secondValue;
            default:
                return firstValue;
        }
    }

}

