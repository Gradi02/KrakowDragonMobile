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

    private void Awake()
    {
		Card_M = GameObject.FindGameObjectWithTag("manager").GetComponent<Card_Manager>();
    }
    public void PlaceCardOnTile()
    {
		//W FUNKCJI AWAKE DODA£EM REFERENCJE DO Card_M POPRZEZ TAG BO KAZDY TILE JEST PREFABEM WIEC NIE PRZYPISZEMY MU NA STA£E REFERENCJI OBIEKTU STA£EGO
		GameObject selectedCard = Card_M.GetCard();


		if (!blockedTile && selectedCard != null)
		{
			currentCard = selectedCard.GetComponent<CardPreset>().Card;															  
			GetComponent<Image>().sprite = currentCard.CardIcon;
			Card_M.DeleteSelectedCard();
		}
		else
		{
			Debug.Log("Pole Zablokowane lub Zajête");
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
