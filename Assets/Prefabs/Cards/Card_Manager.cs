using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Card_Manager : MonoBehaviour
{
	public int currentGoldCount = 1000;
	private int cardCost = 10;
	[SerializeField] private TextMeshProUGUI goldText;

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
		if (CardCount < MaxCount && currentGoldCount > cardCost)
		{
			Instantiate(RandomCard, Vector3.zero, Quaternion.identity, Deck);
			CardCount++;
			currentGoldCount -= cardCost;

			ListOfCards.Clear();
			for (int i = 0; i < Deck.childCount; i++)
			{
				GameObject child = Deck.GetChild(i).gameObject;
				ListOfCards.Add(child);
			}
			//Debug.Log(ListOfCards.Count);
		}
	}

	public void GiveFreeCard()
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
			//Debug.Log(ListOfCards.Count);
		}
	}


	public void SellCard()
	{
		//Potencjalnie koszt sprzeda¿y lub ile dostajesz za sell
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

	public void DeleteSelectedCard()
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

	public GameObject GetTileByPosition(Vector2 pos)
    {
		foreach (GameObject tile in ListOfTiles)
		{
			
			if (tile.GetComponent<TileInfo>().GetTilePosition() == pos)
			{
				Debug.Log(tile.GetComponent<TileInfo>().GetTilePosition());
				return tile;
			}
		}
		return null;
	}

	public void SkipRound()
	{

	}

    private void Update()
    {
		goldText.text = "Gold: " + currentGoldCount;
    }
}
