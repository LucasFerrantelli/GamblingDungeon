using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belligerent : MonoBehaviour
{
    public int life;
    public int power;
    public int modifier;
    public List<Cards> initialDeck;
    public List<InGameCard> allCards;
    public List<InGameCard> actualDeck;
    public List<InGameCard> hand;

    public Belligerent opponent;
    public CombatManager combatManager;

    public int powerCeiling;

    private void Start()
    {
        
    }

    void InitializeCards()
    {
        for (int i = 0; i < initialDeck.Count; i++)
        {
            allCards.Add( Instantiate(combatManager.inGameCard).GetComponent<InGameCard>());
        }
        for (int i = 0; i < initialDeck.Count; i++)
        {
            allCards[i].cost = initialDeck[i].cost;
            allCards[i].descriptions = initialDeck[i].descriptions;
            allCards[i].effects = initialDeck[i].effects;
            allCards[i].gameplayName = initialDeck[i].gameplayName;
            allCards[i].names = initialDeck[i].names;
            allCards[i].nbrOfUtilisation = initialDeck[i].nbrOfUtilisation;
        }
    }

    void EndTurn()
    {
        ResetDeck();
        ResetHand();
    }     

    void ResetDeck()
    {
        allCards = actualDeck;
    }

    private void ResetHand()
    {
        hand.Clear();
    }

    void AddCardToHand(InGameCard card)
    {
        hand.Add(card);
    }

    int CardsLeftInDeck()
    {
        return actualDeck.Count;
    }

    InGameCard GetARandomCardInDeck()
    {
        int _nbrOfCards = CardsLeftInDeck();
        if (_nbrOfCards == 0)
        {
            print("Impossible to Draw, there is no more cards");
            return null;
        }
        else
        {
            return actualDeck[Random.Range(0, _nbrOfCards)];
        }
    }

    void DrawCard()
    {
        InGameCard _card = GetARandomCardInDeck();
        hand.Add(_card);
        AddPower(_card.cost[0]);
    }

    void CheckPower()
    {
        if(power > powerCeiling)
        {
            print("You lost, Destroy all cards, can't draw anymore.");
        }
        else if(power == powerCeiling)
        {
            print("Jackpot ! Reset power and set modifier");
        }
        else if(power < powerCeiling)
        {
            print("nothing happen");
        }
    }

    public void AddPower(int _powerToAdd)
    {
        power += _powerToAdd;
        CheckPower();
    }

}
