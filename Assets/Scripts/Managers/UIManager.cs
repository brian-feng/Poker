using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace VideoPoker
{
	//-//////////////////////////////////////////////////////////////////////
	///
	/// Manages UI including button events and updates to text fields
	/// 
	public class UIManager : MonoBehaviour
	{
		[SerializeField]
		private Text currentBalanceText = null;

		[SerializeField]
		private Text winningText = null;
		[SerializeField]
		private Text betText = null;

		[SerializeField]
		private Button decreaseBetButton = null;
		[SerializeField]
		private Button increaseBetButton = null;
		[SerializeField]
		private Button dealButton = null;
		private int bet = 0;

		// 0 - Choosing bets
		// 1 - Initial Deal
		// 2 - Payout
		private int phase = 0;
		public static UIManager Singleton { get; private set; }

		void Awake()
		{
			if (Singleton != null && Singleton != this) Destroy(this);
			else Singleton = this;
		}

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Start()
		{
			dealButton.onClick.AddListener(OnDealButtonPressed);
			increaseBetButton.onClick.AddListener(OnBetIncreaseButtonPressed);
			decreaseBetButton.onClick.AddListener(OnBetDecreaseButtonPressed);
		}

		//-//////////////////////////////////////////////////////////////////////
		///
		/// Event that triggers when bet button is pressed
		/// 
		private void OnDealButtonPressed()
		{
			if(bet == 0){
				return;
			}
			if(phase == 0){
				phase = 1;
				GameManager.Singleton.SetBet(bet);
				DeckManager.Singleton.Deal();
				increaseBetButton.gameObject.SetActive(false);
				decreaseBetButton.gameObject.SetActive(false);
			}
			else if(phase == 1){
				phase = 2;
				DeckManager.Singleton.Redeal();
				GameManager.Singleton.Payout();
				DeckManager.Singleton.DisableCardUI();
			}
			else{
				phase = 0;
				DeckManager.Singleton.Restart();
				increaseBetButton.gameObject.SetActive(true);
				decreaseBetButton.gameObject.SetActive(true);
				bet = 0;
				betText.text = "Bet: " + bet.ToString() + " Credits";
			}
		}

		private void OnBetIncreaseButtonPressed(){
			bet++;
			betText.text = "Bet: " + bet.ToString() + " Credits";
		}

		private void OnBetDecreaseButtonPressed(){
			if(bet <= 0){
				return;
			}
			bet--;
			betText.text = "Bet: " + bet.ToString() + " Credits";
		}

		public void UpdateCredits(int newCredits){
			currentBalanceText.text = "Balance: " + newCredits.ToString() + " Credits";
		}

		public void UpdateWinningText(int newWinnings){
			winningText.text = "Jacks or Better! You won " + newWinnings + " credits.";
		}
	}
}