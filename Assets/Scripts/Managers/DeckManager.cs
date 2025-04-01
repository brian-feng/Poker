using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.ExceptionServices;
using UnityEngine.UI;


namespace VideoPoker {
    /// <summary>
    /// The manager for the virtual deck of cards used to play Video Poker. Handles everything to do with dealing, shuffling, and resetting the deck after play. 
    /// </summary>
	public class DeckManager : MonoBehaviour
	{
		private List<Card> _deck;
		[SerializeField]
		private HorizontalLayoutGroup _cardHolder;
		[SerializeField]
		private GameObject _cardPrefab;
		public Hand hand;
		public static DeckManager Singleton { get; private set; }

		void Awake()
		{
			if (Singleton != null && Singleton != this) Destroy(this);
			else Singleton = this;
		}

        void Start()
        {
            // Initialize all 52 cards into the deck
            _deck = new List<Card>();
            if(_deck.Count > 0){
                _deck.Clear();
            }
			for(int i = 1; i <= 13; i++){
				_deck.Add(new Card(i, Suit.Diamonds));
				_deck.Add(new Card(i, Suit.Clubs));
				_deck.Add(new Card(i, Suit.Hearts));
				_deck.Add(new Card(i, Suit.Spades));
			}
        }

        /// <summary>
        /// Deals the top five cards from the deck and begins the game.
        /// </summary>
        public void Deal()
		{
			ShuffleDeck();
			hand = new Hand();
			for(int i = 0; i < 5; i++){
				hand.AddCard(Draw());
            }
            
            hand.SortHand();
            RefreshHandUI();
		}
        /// <summary>
        /// Function to call after player wants to re-deal their cards.
        /// </summary>
        public void Redeal(){
            bool redrawn = false;
            CardUI[] cards = _cardHolder.GetComponentsInChildren<CardUI>();
            foreach(CardUI c in cards){
                if(!c.hold){
                    ReturnCard(c.card);
                    hand.AddCard(Draw());
                    if(!redrawn){
                        AudioManager.Singleton.PlayDealSound();
                        redrawn = true;
                    }
                }
                else{
                    // Reset hold to false
                    c.OnPress();
                }
                Destroy(c.gameObject);
            }
            
            hand.SortHand();
            RefreshHandUI();
        }

        /// <summary>
        /// Function to restart the game.
        /// </summary>
        public void Restart(){
            CardUI[] cards = _cardHolder.GetComponentsInChildren<CardUI>();
            foreach(CardUI c in cards){
                ReturnCard(c.card);
                Destroy(c.gameObject);
            }
        }

        /// <summary>
        /// Shuffles the deck. Moves every card to a random spot further than its current spot. 
        /// </summary>
		public void ShuffleDeck() {
			System.Random rand = new System.Random();
			for(int i = 0; i < _deck.Count; i++){
				int r = i + rand.Next(52 - i);
				Card temp = _deck[r];
				_deck[r] = _deck[i];
				_deck[i] = temp;
			}
		}

        /// <summary>
        /// Removes the top card from the deck and returns it.
        /// </summary>
        public Card Draw(){
			Card temp = _deck[0];
			_deck.RemoveAt(0);
			return temp;
		}

        /// <summary>
        /// Sends a card to the bottom of the deck.
        /// </summary>
        /// <param name="card">The card to be returned to the deck.</param>
		public void ReturnCard(Card card){
			_deck.Add(card);
            hand.DeleteCard(card);
		}

        /// <summary>
        /// Refreshes the cards currently displayed in the UI.
        /// </summary>
        public void RefreshHandUI(){
            foreach(Card c in hand.GetCards()){
                GameObject newCard = Instantiate(_cardPrefab, _cardHolder.transform);
				newCard.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/Cards/img_card_" + c.ToString());
                newCard.GetComponent<CardUI>().card = c;
            } 
        }

        /// <summary>
        /// Prevents every card from being pressed. To be used after re-dealing.
        /// </summary>
        public void DisableCardUI(){
            Button[] cards = _cardHolder.GetComponentsInChildren<Button>();
            foreach(Button b in cards){
                b.enabled = false;
            }
        }
        
        /// <summary>
        /// Prints all cards in the deck, not including cards in the hand (DEBUGGING ONLY).
        /// </summary>
        public void PrintDeck(){
			foreach(Card c in _deck){
				Debug.Log(c.ToString());
			}
		}

        /// <summary>
        /// Returns the hand
        /// </summary>
        public Hand GetHand(){
            return hand;
        }
	}
}
