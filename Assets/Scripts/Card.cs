using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

/// <summary>
/// The enum for Suites in the backend. Ordered from smallest to largest to accomodate sorting and/or comparison of equally valued cards. Diamonds < Clubs < Hearts < Spades.
/// </summary>
public enum Suite {Diamonds, Clubs, Hearts, Spades};

/// <summary>
/// The base class for any card in the backend. Values of 1 represent Aces, 11 represent Jacks, 12 represent Queens, and 13 represent Kings.
/// </summary>
public class Card : MonoBehaviour
{   
    public int value;
    public Suite suite;
}