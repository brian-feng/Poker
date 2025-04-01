using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.ExceptionServices;
using UnityEngine.UI;


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

        public void SetBet(int newBet){
			bet = newBet;
			credits -= newBet;
			UIManager.Singleton.UpdateCredits(credits);
		}

		public void Payout(){
			int value = DeckManager.Singleton.GetHand().CalculateValue();
			UIManager.Singleton.UpdateWinningText(value * bet);
			credits += value * bet;
			UIManager.Singleton.UpdateCredits(credits);
		}
	}
}