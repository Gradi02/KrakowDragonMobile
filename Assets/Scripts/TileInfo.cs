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

	private int posX=0, posY=0;
	public GameObject tileToMove = null;

	private void Awake()
    {
		Card_M = GameObject.FindGameObjectWithTag("manager").GetComponent<Card_Manager>();
    }
    public void PlaceCardOnTile()
    {
		//Najpierw sprawdzam czy pole nie jest opcj� przesuni�cia
		if (tileToMove != null)
        {
			currentCard = tileToMove.GetComponent<TileInfo>().currentCard;
			GetComponent<Image>().sprite = currentCard.CardIcon;

			tileToMove.GetComponent<TileInfo>().currentCard = null;
			tileToMove.GetComponent<Image>().sprite = null;

			Card_M.AfterMove();
			return;
		}

		//Potem czy karta na tym polu mo�e si� ruszy�
		if (currentCard != null)
        {
			int currentGold = Card_M.currentGoldCount;
			if(currentCard.move_price > 0 && currentGold > currentCard.move_price)
            {
				Card_M.currentGoldCount -= currentCard.move_price;
				Card_M.ShowMovesForPosition(new Vector2(posX,posY));
            }
			else
            {
				//potencjalnie usuwanie kart z plansze tu mo�na da�
				return;
            }
        }


		//Je�li powy�sze si� nie wykona�y to sprawdzam czy jest jakas karta wybrana do postawienia
		GameObject selectedCard = Card_M.GetCard();

		if (!blockedTile && selectedCard != null)
		{
			currentCard = selectedCard.GetComponent<CardPreset>().Card;															  
			GetComponent<Image>().sprite = currentCard.CardIcon;
			Card_M.DeleteSelectedCard();
		}
		else
		{
			Debug.Log("Pole Zablokowane lub Zaj�te");
		}
	}

	public void SetPositionInfo(int xin, int yin)
    {
		posX = xin;
		posY = yin;
    }

	public Vector2 GetTilePosition()
    {
		return new Vector2(posX, posY);
    }

	public void SetBlock(bool st)
    {
		blockedTile = st;
    }
}
