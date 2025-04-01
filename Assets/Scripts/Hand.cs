using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

/// <summary>
/// The backend hand class. This class prevents cards from exceeding a count of 5 and will log an error when attempting to add to a full hand.
/// </summary>
public class Hand {

    private List<Card> _cards;
    private CardComparer comp = new CardComparer();

    // ----- Constructors ------

    public Hand(List<Card> newCards){
        _cards = new List<Card>();
        foreach(Card card in newCards){
            AddCard(card);
        }
    }
    public Hand(){
        _cards = new List<Card>();
    }

    // ----- Accessors ------

    /// <summary>
    /// Returns the number of cards currently in the hand.
    /// </summary>
    public int Count(){
        return _cards.Count;
    }

    /// <summary>
    /// Returns the list of cards currently in the hand (DEBUGGING ONLY).
    /// </summary>
    public List<Card> GetCards(){
        return _cards;
    }

    // ----- Mutators ------

    /// <summary>
    /// Removes all cards from the hand.
    /// </summary>
    public void DeleteHand(){
        _cards.Clear();
    }

    /// <summary>
    /// Removes the target card from the hand once.
    /// </summary>
    /// <param name="target">The card to be removed.</param>
    public void DeleteCard(Card target){
        foreach(Card card in _cards){
            if(comp.Compare(card, target) == 0){
                _cards.Remove(card);
                return;
            }
        }
    }

    /// <summary>
    /// Removes the cards in the parameter from the hand.
    /// </summary>
    /// <param name="targets">The cards to be removed.</param>
    public void DeleteCards(List<Card> targets){
        foreach(Card target in targets){
            DeleteCard(target);
        }
    }

    /// <summary>
    /// Adds a single card to the hand. Will log an error when attempting to add a card to a full hand.
    /// </summary>
    /// <param name="newCard">The card to be added.</param>
    public void AddCard(Card newCard){
        if(_cards.Count >= 5){
            Debug.LogWarning("Attempted to add more than 5 cards to hand!");
            return;
        }
        _cards.Add(newCard);
    }

    /// <summary>
    /// Adds all elements from parameter into the hand. Will log an error when attempting to add a card to a full hand.
    /// </summary>
    /// <param name="newCards">The list of cards to be added.</param>
    public void AddCards(List<Card> newCards){
        foreach(Card card in newCards){
            if(_cards.Count >= 5){
                Debug.LogWarning("Attempted to add more than 5 cards to hand!");
                return;
            }
            _cards.Add(card);
        }
    }

    /// <summary>
    /// Empties current hand and adds all elements of newCards to the hand. Will log an error when attempting to add a card to a full hand.
    /// </summary>
    /// <param name="newCards">The of cards to replace the old hand.</param>
    public void RefreshHand(List<Card> newCards){
        DeleteHand();
        foreach(Card card in newCards){
            if(_cards.Count >= 5){
                Debug.LogWarning("Attempted to add more than 5 cards to hand!");
                return;
            }
            _cards.Add(card);
        }
    }

    /// <summary>
    /// Sorts the hand from from smallest card at index 0 to largest card.
    /// </summary>
    public void SortHand(){
        _cards.Sort(comp);
    }

    /// <summary>
    /// Prints all cards in the hand (DEBUGGING ONLY).
    /// </summary>
    public void PrintHand(){
        foreach(Card c in _cards){
            Debug.Log(c.ToString());
        }
    }

    public int CalculateValue(){
        if(HasRoyalFlush()){
            return 800;
        }
        else if(HasStraightFlush()){
            return 50;
        }
        else if(HasFourOfAKind()){
            return 25;
        }
        else if(HasFullHouse()){
            return 9;
        }
        else if(HasFlush()){
            return 6;
        }
        else if(HasStraight()){
            return 4;
        }
        else if(HasThreeOfAKind()){
            return 3;
        }
        else if(HasTwoPair()){
            return 2;
        }
        else if(HasJacksOrBetter()){
            return 1;
        }
        else{
            return 0;
        }
    }

    // ----- Hand calculation functions ------

    private int CountOf(int target){
        int count = 0;
        foreach (Card card in _cards){
            if(card.value == target){
                count++;
            }
        }
        return count;
    }

    private bool Contains(int target){
        foreach (Card card in _cards){
            if(card.value == target){
                return true;
            }
        }
        return false;
    }

    private bool HasRoyalFlush(){
        return HasStraightFlush() && Contains(13) && Contains(1);
    }

    private bool HasStraightFlush(){
        return HasStraight() && HasFlush();
    }

    private bool HasFourOfAKind(){
        foreach(Card c in _cards){
            if(CountOf(c.value) == 4){
                return true;
            }
        }
        return false;
    }

    private bool HasFullHouse(){
        int threes = 0;
        int twos = 0;
        foreach(Card c in _cards){
            if(CountOf(c.value) == 3){
                threes++;
            }
            else if(CountOf(c.value) == 2){
                twos++;
            }
        }
        return threes == 1 && twos == 1;
    }
    private bool HasFlush(){
        for(int i = 0; i < 4; i++){
            if(_cards[i].suit != _cards[i+1].suit){
                return false;
            }
        }
        return true;
    }

    private bool HasStraight(){
        for(int i = 0; i < 4; i++){
            if(comp.Compare(_cards[i], _cards[i+1]) != -1){
                return false;
            }
        }
        return true;
    }
    private bool HasThreeOfAKind(){
        foreach(Card c in _cards){
            if(CountOf(c.value) == 3){
                return true;
            }
        }
        return false;
    }

    private bool HasTwoPair(){
        int count = 0;
        int found = 0;
        foreach(Card c in _cards){
            if(CountOf(c.value) == 2 && c.value != found){
                count++;
                found = c.value;
            }
        }
        return count == 2;
    }
    private bool HasJacksOrBetter(){
        return CountOf(11) == 2 || CountOf(12) == 2 || CountOf(13) == 2 | CountOf(1) == 2;
    }
}