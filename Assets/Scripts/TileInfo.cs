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
    public bool clicked = false;

    public void PlaceCardOnTile()
    {
        //if (blockedTile || currentCard != null) return;

		//  Pobraæ sk¹dœ aktualnie wybran¹ kartê
		//  CardsScriptableObject selectedCard
		//  currentCard = selectedCard;
		GameObject selectedCard = Card_M.GetCard();
		if (selectedCard != null)
		{
			CardsScriptableObject selectedCardScript = selectedCard.GetComponent<CardsScriptableObject>();
			if (selectedCardScript != null)
			{
				currentCard = selectedCardScript;
				Image ImageIcon = GetComponent<Image>();
				ImageIcon.sprite = currentCard.GetComponent<CardPreset>().CardIcon;
			}
		}
		else
		{
			Debug.Log("dupa");
		}

	}
}
