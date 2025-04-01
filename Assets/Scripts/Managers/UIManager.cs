using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


namespace VideoPoker
{
	/// <summary>
	/// The manager for all UI not directly related to cards. Updates the UI but does not internally save the state of the game elements like bet or balance.
	/// </summary>
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
		private Button decreaseBetButton10 = null;
		[SerializeField]
		private Button increaseBetButton = null;
		[SerializeField]
		private Button increaseBetButton10 = null;
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

		void Start()
		{
			dealButton.onClick.AddListener(OnDealButtonPressed);
			increaseBetButton.onClick.AddListener(OnBetIncreaseButtonPressed);
			decreaseBetButton.onClick.AddListener(OnBetDecreaseButtonPressed);
			increaseBetButton10.onClick.AddListener(OnBetIncreaseButtonPressed10);
			decreaseBetButton10.onClick.AddListener(OnBetDecreaseButtonPressed10);
		}

		private void OnDealButtonPressed()
		{
			if(bet == 0){
				return;
			}
			if(phase == 0){
				if(GameManager.Singleton.SetBet(bet)){
					phase = 1;
					AudioManager.Singleton.PlayDealSound();
					DeckManager.Singleton.Deal();
					increaseBetButton.gameObject.SetActive(false);
					decreaseBetButton.gameObject.SetActive(false);
					increaseBetButton10.gameObject.SetActive(false);
					decreaseBetButton10.gameObject.SetActive(false);
				}
				else{
					return;
				}
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
				increaseBetButton10.gameObject.SetActive(true);
				decreaseBetButton10.gameObject.SetActive(true);
				bet = 0;
				UpdateBet();
				RevertWinningText();
			}
		}

		private void OnBetIncreaseButtonPressed(){
			bet++;
			UpdateBet();
		}

		private void OnBetDecreaseButtonPressed(){
			if(bet <= 0){
				return;
			}
			bet--;
			UpdateBet();
		}

		private void OnBetIncreaseButtonPressed10()
        {
            bet += 10;
            UpdateBet();
        }

        
        private void OnBetDecreaseButtonPressed10(){
			if(bet <= 9){
				return;
			}
			bet-=10;
			UpdateBet();
		}

		private void UpdateBet()
        {
            betText.text = bet.ToString();
        }

		public void UpdateCredits(int newCredits){
			currentBalanceText.text = newCredits.ToString();
		}

		public void UpdateWinningText(int newWinnings){
			winningText.text = "You won " + newWinnings.ToString() + " credits!";
		}

		public void RevertWinningText(){
			winningText.text = "Jacks or Better!";
		}

        public void Loser(){
			winningText.text = "You have no money :(";
		}
    }
}