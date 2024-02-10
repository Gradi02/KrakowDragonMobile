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
	public Sprite flame;
	private bool blockedTile = false;
	private int disabledTime = 0;

	private int posX=0, posY=0;
	public GameObject tileToMove = null;

	//Piggy bank
	private int collectedGold = 4;
	private int incomePerRound = 2;
	private int maxGold = 20;
	private int bonus = 10;

	public bool UpdatePiggyBank()
	{
		collectedGold += incomePerRound;

		if (collectedGold >= maxGold)
		{
			collectedGold += bonus;
			return true;
		}
		return false;
	}

	public int GetGoldCount()
	{
		return collectedGold;
	}

	private void Awake()
    {
		Card_M = GameObject.FindGameObjectWithTag("manager").GetComponent<Card_Manager>();
    }
    public void PlaceCardOnTile()
    {
		//Najpierw sprawdzam czy pole nie jest opcj¹ przesuniêcia
		if (tileToMove != null)
        {
			Card_M.currentGoldCount -= tileToMove.GetComponent<TileInfo>().currentCard.move_price;

			if (currentCard != null)
            {
				if(currentCard.card_name == "Dragon")
                {
					GameObject.FindGameObjectWithTag("manager").GetComponent<DragonAI>().DamageToDragon(
						tileToMove.GetComponent<TileInfo>().currentCard.attack_power);
					Card_M.HideMoves();
					Card_M.AfterMove();
					return;
                }
            }

			currentCard = tileToMove.GetComponent<TileInfo>().currentCard;
			GetComponent<Image>().sprite = currentCard.CardIcon;

			tileToMove.GetComponent<TileInfo>().currentCard = null;
			tileToMove.GetComponent<Image>().sprite = null;

			Card_M.AfterMove();
			return;
		}

		//Potem czy karta na tym polu mo¿e siê ruszyæ
		if (currentCard != null)
        {
			int currentGold = Card_M.currentGoldCount;
			if(currentCard.move_price > 0 && currentGold > currentCard.move_price)
            {
				Card_M.HideMoves();
				Card_M.ShowMovesForPosition(new Vector2(posX,posY));
				return;
            }
			else
            {
				//potencjalnie usuwanie kart z plansze tu mo¿na daæ
				return;
            }
        }


		//Jeœli powy¿sze siê nie wykona³y to sprawdzam czy jest jakas karta wybrana do postawienia
		GameObject selectedCard = Card_M.GetCard();

		if (!blockedTile && selectedCard != null)
		{
			currentCard = selectedCard.GetComponent<CardPreset>().Card;															  
			GetComponent<Image>().sprite = currentCard.CardIcon;
			Card_M.DeleteSelectedCard();
			Card_M.AfterMove();
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

	public void SetCard(CardsScriptableObject cardIn)
    {
		currentCard = cardIn;
		GetComponent<Image>().sprite = currentCard.CardIcon;
		GetComponent<Animator>().enabled = false;
	}

	public void RemoveCard()
    {
		currentCard = null;
		GetComponent<Image>().sprite = null;
		tileToMove = null;
		blockedTile = false;
	}

	public void DragonAttack()
    {
		CheckForPiggyBank();
		RemoveCard();
		blockedTile = true;
		GetComponent<Image>().sprite = flame;
		GetComponent<Animator>().enabled = true;
		GetComponent<Animator>().SetBool("fire", true);
		disabledTime = 5;
	}

	public void DisableCountDown()
    {
		if(disabledTime > 0)
        {
			disabledTime--;
			
			if(disabledTime == 0)
            {
				RemoveCard();
				blockedTile = false;
				GetComponent<Animator>().SetBool("fire", false);
				GetComponent<Animator>().enabled = false;
			}
        }
    }

	private void CheckForPiggyBank()
    {
		if (currentCard != null)
		{
			if (currentCard.card_name == "piggy bank")
			{
				Debug.Log("smok zajeba³ piggy - " + GetGoldCount());
				Card_M.AddGold(GetGoldCount(), this);
			}
		}
    }

	public bool IfBlocked()
    {
		return blockedTile;
    }
}
