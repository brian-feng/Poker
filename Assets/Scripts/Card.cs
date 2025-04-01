using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The enum for a card's suit. Ordered from smallest to largest to accomodate sorting and/or comparison of equally valued cards. Diamonds < Clubs < Hearts < Spades.
/// </summary>
public enum Suit {Diamonds, Clubs, Hearts, Spades};

/// <summary>
/// The component class for any card in the backend. Values of 1 represent Aces, 11 represent Jacks, 12 represent Queens, and 13 represent Kings.
/// </summary>
public class Card : System.Object
{   
    public int value;
    public Suit suit;
    public Card(int v, Suit s){
        value = v;
        suit = s;
    }

    /// <summary>
    /// Returns the string representation of the card in the format "[suite] [number]" where suite is d for diamonds, c for clubs, h for hearts, or s for spades, and number always has two digits.
    /// </summary>
    public override String ToString(){
        if(suit == Suit.Diamonds){
            return "d" + value.ToString("D2");
        }
        else if(suit == Suit.Clubs){
            return "c" + value.ToString("D2");
        }
        else if(suit == Suit.Hearts){
            return "h" + value.ToString("D2");
        }
        else{
            return "s" + value.ToString("D2");
        }
    }
}

/// <summary>
/// Implementation of a Comparer for Cards. Prioritizes values then suits. However, 1 is considered to be the largest value.
/// </summary>
public class CardComparer : IComparer<Card>
{
    /// <summary>
    /// Compares two cards. Prioritizes values then suits. However, 1 is considered to be the largest value.
    /// </summary>
    /// <param name="obj">The card to be compared to.</param>
    public int Compare(Card a, Card b){
        // 1 is considered to be larger than 13
        if(modulus(a.value - 2, 13) == modulus(b.value - 2, 13)){
            return a.suit - b.suit;
        }
        return modulus(a.value - 2, 13) - modulus(b.value - 2, 13);
    }

    // Because C# has no built in modulus function (% is remainder) I had to implement this one myself
    private int modulus(int a, int b){
        if(a < 0){
            a += b;
        }
        return a % b;
    }
}