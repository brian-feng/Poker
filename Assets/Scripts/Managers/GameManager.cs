using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.ExceptionServices;


namespace VideoPoker {
	public class GameManager : MonoBehaviour
	{
		[SerializeField]
		private List<Card> _deck;
		public Hand hand;
		public static GameManager Singleton { get; private set; }

		void Awake()
		{
			if (Singleton != null && Singleton != this) Destroy(this);
			else Singleton = this;
		}


		void Start()
		{
			InitializeDeck();
			ShuffleDeck();
			PrintDeck();
		}
		
		void Update()
		{

		}

		public void InitializeDeck() {
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

		public void PrintDeck(){
			foreach(Card c in _deck){
				Debug.Log(c.toString());
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
	}
}
