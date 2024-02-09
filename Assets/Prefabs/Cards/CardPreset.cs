using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

	public void Start()
	{
		this.gameObject.transform.localScale = new Vector3(0,0,0);
		LeanTween.scale(this.gameObject, new Vector3(10, 14, 1), 0.3f);
	}

	public void Click()
	{
		if (clicked == true)
		{
			clicked = false;
			Activated.SetActive(false);
		}

		//AKTYWACJA
		else if (clicked == false)
		{
			List<GameObject> l = GameObject.FindGameObjectWithTag("manager").GetComponent<Card_Manager>().ListOfCards;

			foreach (GameObject card in l)
			{
				card.GetComponent<CardPreset>().clicked = false;
				//card.GetComponent<CardPreset>().Activated.SetActive(false);
 
			}
			clicked = true;
			StartCoroutine(PickedCard());

		}
	}
	public IEnumerator PickedCard()
	{
		while (clicked == true)
		{
			LeanTween.rotateZ(gameObject, 2, 0.1f);
			yield return new WaitForSeconds(0.11f);

			LeanTween.rotateZ(gameObject, 0, 0.1f);
			yield return new WaitForSeconds(0.11f);

			LeanTween.rotateZ(gameObject, -2, 0.1f);
			yield return new WaitForSeconds(0.11f);

			LeanTween.rotateZ(gameObject, 0, 0.1f);
			yield return new WaitForSeconds(0.11f);
		}
		
	}
}
