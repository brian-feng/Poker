using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Hand {
    public List<Card> cards;
    public int value;
    public Hand(List<Card> newCards){
        cards = newCards;
    }
    public Hand(){
        cards = new List<Card>();
    }

    public int countOf(int target){
        int count = 0;
        foreach (Card card in cards){
            if(card.value == target){
                count++;
            }
        }
        return count;
    }

    public bool contains(int target){
        foreach (Card card in cards){
            if(card.value == target){
                return true;
            }
        }
        return false;
    }
}