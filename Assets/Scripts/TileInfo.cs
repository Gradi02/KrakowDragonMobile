using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TileInfo : MonoBehaviour
{
    public CardsScriptableObject currentCard = null;
	public Card_Manager Card_M;
    private bool blockedTile = false;
    //public bool clicked = false;

    private void Awake()
    {
		Card_M = GameObject.FindGameObjectWithTag("manager").GetComponent<Card_Manager>();
    }
    public void PlaceCardOnTile()
    {
        //if (blockedTile || currentCard != null) return;

		//  Pobraæ sk¹dœ aktualnie wybran¹ kartê
		//  CardsScriptableObject selectedCard
		//  currentCard = selectedCard;


		//W FUNKCJI AWAKE DODA£EM REFERENCJE DO Card_M POPRZEZ TAG BO KAZDY TILE JEST PREFABEM WIEC NIE PRZYPISZEMY MU NA STA£E REFERENCJI OBIEKTU STA£EGO
		GameObject selectedCard = Card_M.GetCard();


		if (blockedTile || selectedCard != null)
		{
			currentCard = selectedCard.GetComponent<CardPreset>().Card;															  
			GetComponent<Image>().sprite = currentCard.CardIcon;
		}
		else
		{
			Debug.Log("dupa");
		}
	}
}
