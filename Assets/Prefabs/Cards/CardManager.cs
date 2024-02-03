using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{
	public CardsScriptableObject[] Cards;
	private CardsScriptableObject Card;

	public TextMeshProUGUI Name;
	public TextMeshProUGUI Desc;
	public TextMeshProUGUI Value;
	public TextMeshProUGUI Move_value;
	public Image Icon;
	public Image Name_Bg;

	private void Awake()
	{
		int WhichCard = Random.Range(0, Cards.Length);
		Card = Cards[WhichCard];

		Name.text = Card.card_name;
		Desc.text = Card.card_desc;
		Value.text = Card.value.ToString();

	}
}
