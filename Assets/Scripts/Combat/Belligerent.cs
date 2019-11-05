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

    public Trinket trinket;
    public Ladder ladder;

    public Belligerent opponent;
    public CombatManager combatManager;

    public bool controlledByPlayer;
    public bool isPlaying;
    public int id;

    public int powerCeiling = 21;


    public void InitializeCards()
    {
        //Instantiate cards
        for (int i = 0; i < initialDeck.Count; i++)
        {
            allCards.Add( Instantiate(combatManager.inGameCard).GetComponent<InGameCard>());
        }
        //assign to each card the parameters
        for (int i = 0; i < initialDeck.Count; i++)
        {
            allCards[i].cost = initialDeck[i].cost;
            allCards[i].descriptions = initialDeck[i].descriptions;
            allCards[i].effects = initialDeck[i].effects;
            allCards[i].gameplayName = initialDeck[i].gameplayName;
            allCards[i].names = initialDeck[i].names;
            allCards[i].nbrOfUtilisation = initialDeck[i].nbrOfUtilisation;

            allCards[i].owner = this;
            //If controlled by player, show the "use button"
            if(controlledByPlayer)
            {
                
            }
            //Visualization of the parameters
            allCards[i].UpdateInfos();
        }
    }

    void EndTurn()
    {
        ResetDeck();
        ResetHand();
        combatManager.EndTurn(id);
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

    //Return a random card in deck
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

    //Pick a random card in deck, remove it from deck, add it to the hand + (optional : AddPower) 
    public void DrawCard(bool cost)
    {
        InGameCard _card = GetARandomCardInDeck();
        actualDeck.Remove(_card);
        hand.Add(_card);
        if(cost)
            AddPower(_card.cost[0], true);
    }

    //Check actual power, can end turn, play jackpot, or nothing happen.
    void CheckPower()
    {
        if(power > powerCeiling)
        {
            print("You lost, Destroy all cards, can't draw anymore.");
        }
        else
        {
            modifier = ladder.ladder[power];

            if (power == powerCeiling)
            {
                print("Jackpot ! Reset power and set modifier");
            }
            else if (power < powerCeiling)
            {
                print("nothing happen");
            }
        }
        
    }

    //Add X power + (optional : CheckPower)
    public void AddPower(int _powerToAdd, bool checkPower)
    {
        power += _powerToAdd;
        if(checkPower)
            CheckPower();
    }

}
