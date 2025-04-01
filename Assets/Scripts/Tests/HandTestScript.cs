using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.TestTools;

public class HandTestScript
{
    [Test]
    public void GeneralFunctionalityTest(){
        List<Card> cards = new List<Card>
        {
            new Card(2, Suit.Hearts),
            new Card(9, Suit.Spades),
            new Card(5, Suit.Hearts),
            new Card(6, Suit.Clubs),
            new Card(10, Suit.Hearts)
        };

        Hand hand1 = new Hand();

        hand1.AddCards(cards);
        Assert.IsTrue(hand1.Count() == 5);

        hand1.DeleteHand();
        Assert.IsTrue(hand1.Count() == 0);

        for(int i = 0; i < 5; i++){
            hand1.AddCard(cards[i]);
            Assert.IsTrue(hand1.Count() == i+1);
        }

        for(int i = 0; i < 5; i++){
            Assert.IsTrue(cards[i].value == hand1.GetCards()[i].value);
        }

        // Test for correct ordering
        hand1.SortHand();
        Assert.IsTrue(hand1.GetCards()[0].value == cards[0].value && hand1.GetCards()[0].suit == cards[0].suit);
        Assert.IsTrue(hand1.GetCards()[1].value == cards[2].value && hand1.GetCards()[1].suit == cards[2].suit);
        Assert.IsTrue(hand1.GetCards()[2].value == cards[3].value && hand1.GetCards()[2].suit == cards[3].suit);
        Assert.IsTrue(hand1.GetCards()[3].value == cards[1].value && hand1.GetCards()[3].suit == cards[1].suit);
        Assert.IsTrue(hand1.GetCards()[4].value == cards[4].value && hand1.GetCards()[4].suit == cards[4].suit);
    }

    [Test]
    public void WorthlessHandTest()
    {
        List<Card> cards1 = new List<Card>
        {
            new Card(2, Suit.Hearts),
            new Card(9, Suit.Spades),
            new Card(5, Suit.Hearts),
            new Card(6, Suit.Clubs),
            new Card(10, Suit.Hearts)
        };
        Hand hand1 = new Hand(cards1);
        Assert.IsTrue(hand1.CalculateValue() == 0);
    }

    [Test]
    public void PairThatIsntJacksOrBetterTest()
    {
        List<Card> cards2 = new List<Card>
        {
            new Card(2, Suit.Hearts),
            new Card(2, Suit.Spades),
            new Card(3, Suit.Hearts),
            new Card(4, Suit.Clubs),
            new Card(10, Suit.Hearts)
        };
        Hand hand2 = new Hand(cards2);
        Assert.IsTrue(hand2.CalculateValue() == 0);
    }

    [Test]
    public void JacksOrBetterPairTest()
    {
        List<Card> cards3 = new List<Card>
        {
            new Card(11, Suit.Hearts),
            new Card(11, Suit.Spades),
            new Card(3, Suit.Hearts),
            new Card(4, Suit.Clubs),
            new Card(10, Suit.Hearts)
        };
        Hand hand3 = new Hand(cards3);
        Assert.IsTrue(hand3.CalculateValue() == 1);
    }

    [Test]
    public void AcePairIsJacksOrBetterTest()
    {
        List<Card> cards4 = new List<Card>
        {
            new Card(1, Suit.Hearts),
            new Card(1, Suit.Spades),
            new Card(3, Suit.Hearts),
            new Card(4, Suit.Clubs),
            new Card(10, Suit.Hearts)
        };
        Hand hand4 = new Hand(cards4);
        Assert.IsTrue(hand4.CalculateValue() == 1);
    }

    [Test]
    public void TwoPairTest()
    {
        List<Card> cards6 = new List<Card>
        {
            new Card(2, Suit.Hearts),
            new Card(2, Suit.Spades),
            new Card(4, Suit.Clubs),
            new Card(4, Suit.Diamonds),
            new Card(10, Suit.Hearts)
        };
        Hand hand6 = new Hand(cards6);
        Assert.IsTrue(hand6.CalculateValue() == 2);
    }

    [Test]
    public void ThreeOfAKindTest()
    {
        List<Card> cards7 = new List<Card>
        {
            new Card(3, Suit.Hearts),
            new Card(3, Suit.Spades),
            new Card(3, Suit.Clubs),
            new Card(6, Suit.Diamonds),
            new Card(10, Suit.Hearts)
        };
        Hand hand7 = new Hand(cards7);
        Assert.IsTrue(hand7.CalculateValue() == 3);
    }

    [Test]
    public void FlushTest()
    {
        List<Card> cards8 = new List<Card>
        {
            new Card(2, Suit.Hearts),
            new Card(5, Suit.Hearts),
            new Card(9, Suit.Hearts),
            new Card(6, Suit.Hearts),
            new Card(10, Suit.Hearts)
        };
        Hand hand8 = new Hand(cards8);
        Assert.IsTrue(hand8.CalculateValue() == 6);
    }

    [Test]
    public void FourOfAKindTest()
    {
        List<Card> cards9 = new List<Card>
        {
            new Card(7, Suit.Hearts),
            new Card(7, Suit.Spades),
            new Card(7, Suit.Clubs),
            new Card(7, Suit.Diamonds),
            new Card(10, Suit.Hearts)
        };
        Hand hand9 = new Hand(cards9);
        Assert.IsTrue(hand9.CalculateValue() == 25);
    }

    [Test]
    public void StraightTest()
    {
        List<Card> cards10 = new List<Card>
        {
            new Card(5, Suit.Hearts),
            new Card(6, Suit.Spades),
            new Card(7, Suit.Clubs),
            new Card(8, Suit.Diamonds),
            new Card(9, Suit.Hearts)
        };
        Hand hand10 = new Hand(cards10);
        hand10.SortHand();
        Assert.IsTrue(hand10.CalculateValue() == 4);
    }

    [Test]
    public void StraightFlushTest()
    {
        List<Card> cards11 = new List<Card>
        {
            new Card(5, Suit.Hearts),
            new Card(6, Suit.Hearts),
            new Card(7, Suit.Hearts),
            new Card(8, Suit.Hearts),
            new Card(9, Suit.Hearts)
        };
        Hand hand11 = new Hand(cards11);
        hand11.SortHand();
        Assert.IsTrue(hand11.CalculateValue() == 50);
    }

    [Test]
    public void RoyalFlushTest()
    {
        List<Card> cards12 = new List<Card>
        {
            new Card(10, Suit.Spades),
            new Card(11, Suit.Spades),
            new Card(12, Suit.Spades),
            new Card(13, Suit.Spades),
            new Card(1, Suit.Spades)
        };
        Hand hand12 = new Hand(cards12);
        hand12.SortHand();
        hand12.PrintHand();
        Assert.IsTrue(hand12.CalculateValue() == 800);
    }
}