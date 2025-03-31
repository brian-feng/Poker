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
		private bool dealt = false;
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
			if(!dealt){
				dealt = true;
				DeckManager.Singleton.Deal();
				increaseBetButton.gameObject.SetActive(false);
				decreaseBetButton.gameObject.SetActive(false);
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
	}
}