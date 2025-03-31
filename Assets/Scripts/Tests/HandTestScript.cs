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
}
