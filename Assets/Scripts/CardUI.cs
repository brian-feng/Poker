using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The component class for any card that is displayed in the UI. Hold represents whether the player has clicked on the card and plans to hold it.
/// </summary>
public class CardUI : MonoBehaviour
{
    public Card card;
    public bool hold = false;
    [SerializeField]
    private GameObject HoldText;

    public void OnPress(){
        hold = !hold;
        HoldText.SetActive(hold);
    }

}
