using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Animations : MonoBehaviour
{

	[Header("TURN")]
	public GameObject menu;
	public TextMeshProUGUI turn;
	public UnityEngine.UI.Image background;
	public UnityEngine.UI.Image corner1;
	public UnityEngine.UI.Image corner2;

	UnityEngine.UI.Image c1;
	UnityEngine.UI.Image c2;
	UnityEngine.UI.Image bg;
	public void Start()
	{
		menu.SetActive(false);
		c1 = corner1.gameObject.GetComponent<UnityEngine.UI.Image>();
		c2 = corner2.gameObject.GetComponent<UnityEngine.UI.Image>();
		bg = background.gameObject.GetComponent<UnityEngine.UI.Image>();

		StartCoroutine(TurnPlayer());
	}

	IEnumerator TurnPlayer()
	{
		menu.SetActive(true);
		Show();
		turn.text = "Yours Turn";
		c1.color = new Color(0, 255, 44);
		c2.color = new Color(0, 255, 44);
		yield return new WaitForSeconds(2f);
		Hide();
		yield return new WaitForSeconds(1.1f);
		menu.SetActive(false);
	}
	IEnumerator TurnDragon()
	{
		menu.SetActive(true);
		Show();
		turn.text = "Dragons Turn";
		c1.color = new Color(255, 0, 0);
		c2.color = new Color(255, 0, 0);
		yield return new WaitForSeconds(2f);
		Hide();
		yield return new WaitForSeconds(1.1f);
		menu.SetActive(false);
	}

	public void Show()
	{
		LeanTween.alpha(c1.rectTransform, 1f, 1f);
		LeanTween.alpha(c2.rectTransform, 1f, 1f);
		LeanTween.alpha(bg.rectTransform, 0.3f, 1f);
	}

	public void Hide()
	{
		LeanTween.alpha(c1.rectTransform, 0, 1f);
		LeanTween.alpha(c2.rectTransform, 0, 1f);
		LeanTween.alpha(bg.rectTransform, 0, 1f);
	}
}
