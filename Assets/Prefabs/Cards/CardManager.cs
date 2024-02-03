using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{
	public CardsScriptableObject[] Cards;
	private CardsScriptableObject Card;

	public TextMeshPro Name;
	public TextMeshPro Desc;
	public TextMeshPro Value;
	public TextMeshPro Move_value;
	public GameObject Icon;
	public GameObject Name_Bg;

	private void Awake()
	{
		int WhichCard = Random.Range(0, Cards.Length);
		Card = Cards[WhichCard];

		Name.text = Card.card_name;
		Desc.text = Card.card_desc;
		Value.text = Card.value.ToString();

	}
}
