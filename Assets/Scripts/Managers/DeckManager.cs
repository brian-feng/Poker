using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.ExceptionServices;
using UnityEngine.UI;


namespace VideoPoker {
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
            _deck = new List<Card>();
        }

        public void Deal()
		{
			InitializeDeck();
			ShuffleDeck();
			InitializeHand();
		}

        public void Redeal(){
            CardUI[] cards = _cardHolder.GetComponentsInChildren<CardUI>();
            foreach(CardUI c in cards){
                if(!c.hold){
                    ReturnCard(c.card);
                    Destroy(c);
                }
                else{
                    // Reset hold to false
                    c.OnPress();
                }
            }
        }

		public void InitializeDeck() {
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

		public void ShuffleDeck() {
			System.Random rand = new System.Random();
			for(int i = 0; i < _deck.Count; i++){
				int r = i + rand.Next(52 - i);
				Card temp = _deck[r];
				_deck[r] = _deck[i];
				_deck[i] = temp;
			}
		}

		public void InitializeHand(){
			// Deal initial 5 cards
			hand = new Hand();
			for(int i = 0; i < 5; i++){
				hand.AddCard(Draw());
			}

			// Load the sprites
			foreach(Card c in hand.GetCards()){
				GameObject newCard = Instantiate(_cardPrefab, _cardHolder.transform);
				newCard.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/Cards/img_card_" + c.ToString());
                newCard.GetComponent<CardUI>().card = c;
            }
		}

        public Card Draw(){
			Card temp = _deck[0];
			_deck.RemoveAt(0);
			return temp;
		}

		public void ReturnCard(Card card){
			_deck.Add(card);
		}

        public void PrintDeck(){
			foreach(Card c in _deck){
				Debug.Log(c.ToString());
			}
		}
	}
}
