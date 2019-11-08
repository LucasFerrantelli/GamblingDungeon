using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    public int id;

    [Header ("InGame Variable")]
    public int powerCeiling = 21;
    public bool isPlaying;
    [Tooltip ("True if an effect disabling drawing is on, like if the powerCeiling is reached. Reset each turn")]
    public bool disableDrawing;
    [Tooltip("Check if the Belligerent can actually draw")]
    public bool canDraw;
    [Tooltip("When stunned, a belligerent cant draw or play card")]
    public bool stunned;


    [Header ("Assignations")]
    public GameObject handPivot;
    public GameObject cardInitPosition;

    public void InitializeCards()
    {
        List<InGameCard> _allCards = new List<InGameCard>();
        //Instantiate cards
        for (int i = 0; i < initialDeck.Count; i++)
        {
            allCards.Add(Instantiate(combatManager.inGameCard, handPivot.transform).GetComponent<InGameCard>());
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
            allCards[i].gotADrawEffect = initialDeck[i].gotADrawEffect;
            allCards[i].soToReferTo = initialDeck[i];

            ResetCardPosition(allCards[i].gameObject);

            allCards[i].owner = this;
            //If controlled by player, show the "use button"
            if(controlledByPlayer)
            {
                allCards[i].useButton.gameObject.SetActive(true);
            }
            else
            {
                allCards[i].useButton.gameObject.SetActive(false);
            }
            //Visualization of the parameters
            allCards[i].UpdateInfos();
        }

        ResetDeck();
    }

    public void ResetCardPosition(GameObject _card)
    {
        _card.transform.position = cardInitPosition.transform.position;
    }

    #region TurnManagement

    public void AskForEndTurn()
    {
        combatManager.EndTurn(id);
    }
    public void EndTurn()
    {
        SetPower(0, true);
        ResetDeck();
        ResetHand();
        stunned = false;
        isPlaying = false;
    }
    
    public void StartTurn() 
    {
        isPlaying = true;
        disableDrawing = false;
        UpdateAllCardsStatut();
        UpdateDrawStatut();
    }

    #endregion

    #region Resets
    void ResetDeck()
    {
        actualDeck.Clear();
        for (int i = 0; i < allCards.Count; i++)
        {
            actualDeck.Add(allCards[i]);
        }
        
    }

    private void ResetHand()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            ResetCardPosition(hand[i].gameObject);
        }
        hand.Clear();
    }

    #endregion


    #region Draw and card access
    void ReOrderHand()
    {
        int _nbrOfCardInHand = hand.Count;
        for (int i = 0; i < _nbrOfCardInHand; i++)
        {
            hand[i].transform.DOMoveX(handPivot.transform.position.x + combatManager.cardDispositions[_nbrOfCardInHand - 1].cardsPlacement[i], 1);
        }
    }
    void AddCardToHand(InGameCard card)
    {
        hand.Add(card);
        UpdateDrawStatut();
        ReOrderHand();
        
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

    public void RemoveCardFromDeck(InGameCard _card)
    {
        actualDeck.Remove(_card);
        UpdateDrawStatut();
    }

    public void BanishCardFromCombat(InGameCard _card)
    {

    }

    //Pick a random card in deck, remove it from deck, add it to the hand + (optional : AddPower) 
    public void DrawCard(bool cost)
    {
        InGameCard _card = GetARandomCardInDeck();
        RemoveCardFromDeck(_card);
        AddCardToHand(_card);
        if (cost)
            AddPower(_card.cost[0], true);

    }

    //check if the player can draw a card, either with the draw button or a draw effect
    public void UpdateDrawStatut()
    {
        //if the deck still got card and the hand isnt full
        if(actualDeck.Count > 0 && hand.Count <6)
        {
            for(int i = 0; i < hand.Count; i++)
            {
                if (hand[i].gotADrawEffect)
                {
                    hand[i].cantUseForDrawEffect = false;
                    hand[i].UpdateCardStatut();
                }
            }
            //if the target isnt stunned and got no drawing interdiction
            if ( disableDrawing == false && stunned == false)
            {
                canDraw = true;
                

            }
            //else, cant draw
            else
            {
                canDraw = false;

            }
        }
        //if there is no more card to draw or if the hand is full
        else
        {
            canDraw = false;
            //Each car with a draw effect wont be played
            for (int i = 0; i < hand.Count; i++)
            {
                if(hand[i].gotADrawEffect)
                {
                    hand[i].cantUseForDrawEffect = true;
                    hand[i].UpdateCardStatut();
                }
            }

        }

    }

    #endregion


    #region Power
    //Check actual power, can end turn, play jackpot, or nothing happen.
    void CheckPower()
    {
        if(power > powerCeiling)
        {
            print("You lost, Destroy all cards, can't draw anymore.");
            stunned = true;
            UpdateDrawStatut();
            UpdateAllCardsStatut();
            if (controlledByPlayer)
            {
                combatManager.playerDisplay.ColorBars(21);
            }
        }
        else
        {
            if (controlledByPlayer)
            {
                combatManager.playerDisplay.ColorBars(power);
            }
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

    public void SetPower(int _newPower, bool checkPower)
    {
        power = _newPower;
        if (checkPower)
            CheckPower();
    }

    #endregion

    #region CardUse
    //Check if card is playable
    void UpdateAllCardsStatut()
    {
        for (int i = 0; i < allCards.Count; i++)
        {
            allCards[i].UpdateCardStatut();
        }
    }
    //this function is played when a card is played. Use it to play function
    public void UseCard()
    {
        UpdateDrawStatut();
    }
    #endregion
}
