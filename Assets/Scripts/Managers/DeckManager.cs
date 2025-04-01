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

        public void Deal()
		{
			ShuffleDeck();
			hand = new Hand();
			for(int i = 0; i < 5; i++){
				hand.AddCard(Draw());
            }
            
            hand.SortHand();
            RefreshHand();
		}

        public void Redeal(){
            CardUI[] cards = _cardHolder.GetComponentsInChildren<CardUI>();
            foreach(CardUI c in cards){
                if(!c.hold){
                    ReturnCard(c.card);
                    hand.AddCard(Draw());
                }
                else{
                    // Reset hold to false
                    c.OnPress();
                }
                Destroy(c.gameObject);
            }
            
            hand.SortHand();
            RefreshHand();
        }

        public void Restart(){
            CardUI[] cards = _cardHolder.GetComponentsInChildren<CardUI>();
            foreach(CardUI c in cards){
                ReturnCard(c.card);
                Destroy(c.gameObject);
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

        public Card Draw(){
			Card temp = _deck[0];
			_deck.RemoveAt(0);
			return temp;
		}

		public void ReturnCard(Card card){
			_deck.Add(card);
            hand.DeleteCard(card);
		}

        public void RefreshHand(){
            foreach(Card c in hand.GetCards()){
                GameObject newCard = Instantiate(_cardPrefab, _cardHolder.transform);
				newCard.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/Cards/img_card_" + c.ToString());
                newCard.GetComponent<CardUI>().card = c;
            } 
        }

        public void DisableCardUI(){
            Button[] cards = _cardHolder.GetComponentsInChildren<Button>();
            foreach(Button b in cards){
                b.enabled = false;
            }
        }

        public void PrintDeck(){
			foreach(Card c in _deck){
				Debug.Log(c.ToString());
			}
		}

        public Hand GetHand(){
            return hand;
        }
	}
}
