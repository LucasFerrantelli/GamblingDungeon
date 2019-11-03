using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belligerent : MonoBehaviour
{
    public int life;
    public int power;
    public int modifier;
    public Deck initialDeck;
    public Deck actualDeck;
    public List<Cards> hand;

    public Belligerent opponent;
    public CombatManager combatManager;

    public int powerCeiling;

    void EndTurn()
    {
        ResetDeck();
        ResetHand();
    }

    void ResetDeck()
    {
        actualDeck = initialDeck;
    }

    private void ResetHand()
    {
        hand.Clear();
    }

    void AddCardToHand(Cards card)
    {
        hand.Add(card);
    }

    int CardsLeftInDeck()
    {
        return actualDeck.cards.Count;
    }

    Cards GetARandomCardInDeck()
    {
        int _nbrOfCards = CardsLeftInDeck();
        if (_nbrOfCards == 0)
        {
            print("Impossible to Draw, there is no more cards");
            return null;
        }
        else
        {
            return actualDeck.cards[Random.Range(0, _nbrOfCards)];
        }
    }

    void DrawCard()
    {
        Cards _card = GetARandomCardInDeck();
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
