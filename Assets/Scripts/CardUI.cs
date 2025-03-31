using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
