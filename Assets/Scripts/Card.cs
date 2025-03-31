using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

/// <summary>
/// The enum for a card's suite. Ordered from smallest to largest to accomodate sorting and/or comparison of equally valued cards. Diamonds < Clubs < Hearts < Spades.
/// </summary>
public enum Suite {Diamonds, Clubs, Hearts, Spades};

/// <summary>
/// The component class for any card in the backend. Values of 1 represent Aces, 11 represent Jacks, 12 represent Queens, and 13 represent Kings.
/// </summary>
public class Card : MonoBehaviour, IComparable
{   
    public int value;
    public Suite suite;

    /// <summary>
    /// Implementation of CompareTo for Card. Prioritizes values then suites. However, 1 is considered to be the largest value.
    /// </summary>
    /// <param name="obj">The card to be compared to.</param>
    public int CompareTo(object obj){
        if(obj == null){
            return 1;
        }
        Card otherCard = obj as Card;

        // If obj is a valid Card
        if(otherCard != null){
            // 1 is considered to be larger than 13
            if((value - 2) % 13 == (otherCard.value - 2) % 13){
                return suite - otherCard.suite;
            }
            return (value - 2) % 13 - (otherCard.value - 2) % 13;
        }
        else{
            throw new ArgumentException("Object is not a Card");
        }
    }
}