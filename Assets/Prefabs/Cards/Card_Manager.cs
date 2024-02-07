using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card_Manager : MonoBehaviour
{
	public int currentGoldCount = 10;
	private int cardCost = 10;
	private int mineIncomeForRound = 2;
	private int defaultIncome = 5;
	[SerializeField] private TextMeshProUGUI goldText;
	public QueueGenerator queueM;

	public GameObject RandomCard;
	public Transform Deck;
	public List<GameObject> ListOfCards = new List<GameObject>();

	public List<GameObject> ListOfTiles = new List<GameObject>();

	private int CardCount = 0;
	private int MaxCount = 6;

	public Color defaultColor;
	public Color moveColor;
	public Color attackColor;

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
		if (CardCount < MaxCount && currentGoldCount >= cardCost)
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
				
				return tile;
			}
		}
		return null;
	}

	public void ShowMovesForPosition(Vector2 pickedPosition)
	{
		Vector2[] moves = new Vector2[4];

		moves[0] = new Vector2(pickedPosition.x + 1, pickedPosition.y);
		moves[1] = new Vector2(pickedPosition.x - 1, pickedPosition.y);
		moves[2] = new Vector2(pickedPosition.x, pickedPosition.y + 1);
		moves[3] = new Vector2(pickedPosition.x, pickedPosition.y - 1);

		for (int i = 0; i < 4; i++)
		{
			GameObject m = GetTileByPosition(moves[i]);

			if (m != null)
			{
				if (m.GetComponent<TileInfo>().currentCard == null)
				{
					m.GetComponent<Image>().color = moveColor;
					m.GetComponent<TileInfo>().tileToMove = GetTileByPosition(pickedPosition);
				}
				else if(m.GetComponent<TileInfo>().currentCard.card_name == "Dragon")
                {
					m.GetComponent<Image>().color = attackColor;
					m.GetComponent<TileInfo>().tileToMove = GetTileByPosition(pickedPosition);
				}
			}
		}
	}

	public void HideMoves()
    {
		foreach(GameObject t in ListOfTiles)
        {
			t.GetComponent<TileInfo>().tileToMove = null;
			t.GetComponent<Image>().color = defaultColor;
        }
    }

	public TileInfo GetDragonTile()
    {
		foreach (GameObject tile in ListOfTiles)
		{
			if (tile.GetComponent<TileInfo>().currentCard != null)
			{
				if (tile.GetComponent<TileInfo>().currentCard.card_name == "Dragon")
				{
					return tile.GetComponent<TileInfo>();
				}
			}
		}
		return null;
	}

	public void SkipRound()
	{

	}

	public int CalcGoldIncome()
	{
		int num = 0;
		foreach(GameObject tile in ListOfTiles)
        {
			CardsScriptableObject c = tile.GetComponent<TileInfo>().currentCard;

			if (c != null)
            {
				if(c.card_name == "mine")
                {
					num++;
                }
            }
        }

		int income = num * mineIncomeForRound + defaultIncome;
		//currentGoldCount += income;
		StartCoroutine(GoldAnimation(income));
		return income;
	}

	private IEnumerator GoldAnimation(int income)
    {
		yield return new WaitForSeconds(0.5f);
		int added = 0;
		while(added < income)
        {
			currentGoldCount++;
			added++;
			yield return new WaitForSeconds(1f/income);

			if(income - added <= (1/5 * income))
            {
				float time = 1f - ((float)(income - added) / 10f);
				yield return new WaitForSeconds(time);
			}
        }
    }

	private void Update()
	{
		goldText.text = "Gold: " + currentGoldCount;
	}

	public void AddMapTile(GameObject Ntile)
	{
		ListOfTiles.Add(Ntile);
	}

	public void AfterMove()
	{
		foreach (GameObject tile in ListOfTiles)
		{
			tile.GetComponent<TileInfo>().DisableCountDown();
			if(tile.GetComponent<TileInfo>().tileToMove != null)
            {
				tile.GetComponent<Image>().color = defaultColor;
				tile.GetComponent<TileInfo>().tileToMove = null;
			}
		}
		GetComponent<GameLoopMng>().PlayerMoved();
	}

	public void AfterDragon()
	{
		foreach (GameObject tile in ListOfTiles)
		{
			tile.GetComponent<TileInfo>().DisableCountDown();
		}
	}
}
