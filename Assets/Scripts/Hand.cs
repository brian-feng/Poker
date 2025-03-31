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
    private int _value = 0;
    public Hand(List<Card> newCards){
        foreach(Card card in newCards){
            AddCard(card);
        }
    }
    public Hand(){
        _cards = new List<Card>();
    }

    // ----- Hand manipulation utility functions ------

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
            if(card.CompareTo(target) == 0){
                _cards.Remove(card);
                return;
            }
        }
    }

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

    // ----- Hand calculation functions ------

    public int countOf(int target){
        int count = 0;
        foreach (Card card in _cards){
            if(card.value == target){
                count++;
            }
        }
        return count;
    }

    public bool contains(int target){
        foreach (Card card in _cards){
            if(card.value == target){
                return true;
            }
        }
        return false;
    }

    public bool isFlush(){
        
    }
}