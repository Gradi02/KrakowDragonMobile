
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

	private float time = 0.2f;
	public void Awake()
	{
		menu.SetActive(false);
		c1 = corner1.gameObject.GetComponent<UnityEngine.UI.Image>();
		c2 = corner2.gameObject.GetComponent<UnityEngine.UI.Image>();
		bg = background.gameObject.GetComponent<UnityEngine.UI.Image>();
		turn.gameObject.transform.localScale = new Vector3(0, 0, 0);
	}
	//tura gracza anim
	private IEnumerator TurnPlayerEnum()
	{
		menu.SetActive(true);
		turn.text = "Yours Turn";
		turn.color = new Color(0, 255, 0);
		c1.color = new Color(0, 255, 0);
		c2.color = new Color(0, 255, 0);
		Show();
		yield return new WaitForSeconds(2f);
		Hide();
		yield return new WaitForSeconds(time+0.05f);
		menu.SetActive(false);
		GetComponent<GameLoopMng>().SetBlocker(false);
	}

	//tura smoka anim
	private IEnumerator TurnDragonEnum()
	{
		menu.SetActive(true);
		turn.text = "Dragons Turn";
		turn.color = new Color(255, 0, 0);
		c1.color = new Color(255, 0, 0);
		c2.color = new Color(255, 0, 0);
		Show();
		yield return new WaitForSeconds(2f);
		Hide();
		yield return new WaitForSeconds(time+0.05f);
		menu.SetActive(false);
	}

	public void ShowTurnAnimation(bool IsPlayerTurn)
    {
		if (IsPlayerTurn) StartCoroutine(TurnPlayerEnum());
		else StartCoroutine(TurnDragonEnum());
	}

	//pokazywanie
	public void Show()
	{
		LeanTween.alpha(c1.rectTransform, 1f, time);
		LeanTween.alpha(c2.rectTransform, 1f, time);
		LeanTween.alpha(bg.rectTransform, 0.6f, time);
		LeanTween.scale(turn.gameObject, new Vector3(1f, 1f, 1f), time).setEase(LeanTweenType.easeInCubic);
	}

	//chowanie
	public void Hide()
	{
		LeanTween.alpha(c1.rectTransform, 0, time);
		LeanTween.alpha(c2.rectTransform, 0, time);
		LeanTween.alpha(bg.rectTransform, 0, time);
		LeanTween.scale(turn.gameObject, new Vector3(0f, 0f, 0f), time).setEase(LeanTweenType.easeInCubic);
	}


	public void PlayerTurn()
	{
		StartCoroutine(TurnPlayerEnum());
	}

	public void DragonTurn()
	{
		StartCoroutine(TurnDragonEnum());
	}
}
