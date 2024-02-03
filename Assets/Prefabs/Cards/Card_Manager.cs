using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Manager : MonoBehaviour
{
	public GameObject RandomCard;
	public Transform Deck;
	private List<GameObject> ListOfCards = new List<GameObject>();

	private int CardCount = 0;
	private int MaxCount = 6;
	public void BuyCard()
	{
		if (CardCount < MaxCount)
		{
			Instantiate(RandomCard, Vector3.zero, Quaternion.identity, Deck);
			CardCount++;

			ListOfCards.Clear();
			for (int i = 0; i < Deck.childCount; i++)
			{
				GameObject child = Deck.GetChild(i).gameObject;
				ListOfCards.Add(child);
			}
			Debug.Log(ListOfCards.Count);
		}


	}


	public void SellCard()
	{

	}

	public void PlayCard()
	{

	}
}
