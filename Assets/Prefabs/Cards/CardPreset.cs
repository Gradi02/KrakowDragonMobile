using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardPreset : MonoBehaviour
{
	public CardsScriptableObject[] Cards;
	private CardsScriptableObject Card;

	public TextMeshProUGUI Name;
	public TextMeshProUGUI Desc;
	public TextMeshProUGUI Value;
	public TextMeshProUGUI Move_value;
	public Image Icon;
	public Image Name_Bg;

	public GameObject Activated;

	public bool clicked = false;

	private void Awake()
	{
		int WhichCard = Random.Range(0, Cards.Length);
		Card = Cards[WhichCard];

		Name.text = Card.card_name;
		Desc.text = Card.card_desc;
		Value.text = Card.value.ToString();
	}

	public void Click()
	{
		if (clicked == true)
		{
			clicked = false;
			Activated.SetActive(false);
		}

		else if (clicked == false)
		{
			clicked = true;
			Activated.SetActive(true);
		}
	}
}
