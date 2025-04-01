using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.ExceptionServices;
using UnityEngine.UI;
using UnityEngine.AI;


namespace VideoPoker {
	public class GameManager : MonoBehaviour
	{
		[SerializeField]
		private int credits = 10;
		private int bet = 0;

		public static GameManager Singleton { get; private set; }

		void Awake()
		{
			if (Singleton != null && Singleton != this) Destroy(this);
			else Singleton = this;
		}

        void Start()
        {
			UIManager.Singleton.UpdateCredits(credits);
        }

		/// <summary>
		/// Updates the bet and updates the game state to reflect it. 
		/// </summary>
		/// <param name="newBet">The new value of bet</param>
        public bool SetBet(int newBet){
			if(newBet > credits){
				return false;
			}
			bet = newBet;
			credits -= newBet;
			UIManager.Singleton.UpdateCredits(credits);
			return true;
		}
		
		/// <summary>
		/// Calculates how much the hand is worth then updates the game state to reflect that. 
		/// </summary>
		public void Payout(){
			int value = DeckManager.Singleton.GetHand().CalculateValue();
			UIManager.Singleton.UpdateWinningText(value * bet);
			credits += value * bet;
			UIManager.Singleton.UpdateCredits(credits);
			if(value > 0){
				AudioManager.Singleton.PlayWinSound();
			}
			if(credits == 0){
				UIManager.Singleton.Loser();
			}
		}
	}
}