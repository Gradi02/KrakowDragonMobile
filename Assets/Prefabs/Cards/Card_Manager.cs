using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Card_Manager : MonoBehaviour
{
	public GameObject RandomCard;
	public Transform Deck;
	public List<GameObject> ListOfCards = new List<GameObject>();

	public List<GameObject> ListOfTiles = new List<GameObject>();

	private int CardCount = 0;
	private int MaxCount = 6;

	public void Start()
	{
		ListOfTiles.Clear();
		GameObject[] FoundTiles = GameObject.FindGameObjectsWithTag("tile");
		foreach (GameObject obj in FoundTiles)
		{
			ListOfTiles.Add(obj);
		}
		Debug.Log(ListOfTiles.Count);
	}

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
		for (int i = 0; i < Deck.childCount; i++)
		{
			GameObject child = Deck.GetChild(i).gameObject;
			if (child.GetComponent<CardPreset>().clicked)
			{
				ListOfCards.Remove(child);
				Destroy(child);
				CardCount--;
				break;
			}
		}
	}

	public GameObject GetCard()
	{
		
		foreach (GameObject card in ListOfCards)
		{
			if (card.GetComponent<CardPreset>().clicked)
			{
				return card;
			}
		}
		return null;
	}

	public void PlayCard()
	{ 

	}

	public void SkipRound()
	{

	}
}
