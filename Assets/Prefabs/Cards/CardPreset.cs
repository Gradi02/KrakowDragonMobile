using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardPreset : MonoBehaviour
{
	public CardsScriptableObject Card;

	public UnityEngine.UI.Image Icon;
	public UnityEngine.UI.Image Name_Bg;
	public Sprite CardIcon;
	public TextMeshProUGUI Name;
	public TextMeshProUGUI Desc;
	public TextMeshProUGUI Value;
	public TextMeshProUGUI Move_value;
	private Card_Manager manager;

	public GameObject Activated;

	public bool clicked = false;

	private void Awake()
	{
		manager = GameObject.FindGameObjectWithTag("manager").GetComponent<Card_Manager>();

		Card = manager.GetRandomCardFromPickedList();

		Name.text = Card.card_name;
		Desc.text = Card.card_desc;
		Value.text = Card.value.ToString();
		Icon.sprite = Card.CardIcon;
	}

	public void Click()
	{
		if (clicked == true)
		{
			clicked = false;
			Activated.SetActive(false);

			//////////
			StopAllCoroutines();
			gameObject.transform.rotation = Quaternion.identity;
			//////////
		}

		else if (clicked == false)
		{
			List<GameObject> l = GameObject.FindGameObjectWithTag("manager").GetComponent<Card_Manager>().ListOfCards;

			foreach (GameObject card in l) {
				card.GetComponent<CardPreset>().clicked = false;
				card.GetComponent<CardPreset>().Activated.SetActive(false);

				///////////
				StopAllCoroutines();
				card.transform.rotation = Quaternion.identity;
				///////////
			}

			clicked = true;
			Activated.SetActive(true);
			//////////////
			StartCoroutine(PickedCard());
		    //////////////
		}
	}

	public IEnumerator PickedCard()
	{
		while (true)
		{
			LeanTween.rotateZ(gameObject.gameObject, 2, 0.2f);
			yield return new WaitForSeconds(0.21f);
			LeanTween.rotateZ(gameObject.gameObject, -2, 0.2f);
			yield return new WaitForSeconds(0.21f);
		}
	}
}
