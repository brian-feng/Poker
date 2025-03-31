using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

/// <summary>
/// The enum for a card's suit. Ordered from smallest to largest to accomodate sorting and/or comparison of equally valued cards. Diamonds < Clubs < Hearts < Spades.
/// </summary>
public enum Suit {Diamonds, Clubs, Hearts, Spades};

/// <summary>
/// The component class for any card in the backend. Values of 1 represent Aces, 11 represent Jacks, 12 represent Queens, and 13 represent Kings.
/// </summary>
public class Card : MonoBehaviour
{   
    public int value;
    public Suit suit;
    public Card(int v, Suit s){
        value = v;
        suit = s;
    }
}

public class CardComparer : IComparer<Card>
{
    /// <summary>
    /// Implementation of CompareTo for Card. Prioritizes values then suits. However, 1 is considered to be the largest value.
    /// </summary>
    /// <param name="obj">The card to be compared to.</param>
    public int Compare(Card a, Card b){
        // 1 is considered to be larger than 13
        if((a.value - 2) % 13 == (b.value - 2) % 13){
            return a.suit - b.suit;
        }
        return (a.value - 2) % 13 - (b.value - 2) % 13;
    }
}