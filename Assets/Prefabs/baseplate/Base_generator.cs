using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Base_generator : MonoBehaviour
{
	public int size = 7;
	public Image BlockPrefab;
	public Camera cameraobj;
	public Transform middle;
	public RectTransform grid;
	
	private float spacing = 200;
	private float blockSize = 100;

	void Start()
	{
		StartCoroutine(MapGenerator());
	}

	IEnumerator MapGenerator()
	{
		CalculateSizes();
		// Oblicz œrodek planszy
		Vector3 centerPosition = new Vector3((size - 1) * spacing / 2, (size - 1) * spacing / 2, 1);

		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				yield return new WaitForSeconds(0.05f);
				Image g = Instantiate(BlockPrefab, Vector3.zero, Quaternion.identity, middle);
				g.transform.localPosition = new Vector3(i * spacing, j * spacing, 1) - centerPosition;
				g.transform.localScale = new Vector3(0, 0, 0);
				g.transform.eulerAngles = new Vector3(transform.eulerAngles.x ,transform.eulerAngles.y , transform.eulerAngles.z + 25);
				Vector3 target = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

				LeanTween.scale(g.gameObject, new Vector3(1, 1, 1), 1f).setEase(LeanTweenType.easeOutSine);
				LeanTween.rotate(g.gameObject, target, 0.5f).setEase(LeanTweenType.easeOutSine);

				//dodaj do tablicy mapy w DragonAI
				GetComponent<DragonAI>().AddMapTile(g.GetComponent<TileInfo>());
				GetComponent<Card_Manager>().AddMapTile(g.gameObject);

				//ustaw kordy pola
				g.GetComponent<TileInfo>().SetPositionInfo(i, j);
			}
		}

		GetComponent<GameLoopMng>().StartGameFnc();

		yield return null;
	}

    private void CalculateSizes()
    {
		float availableWidth = grid.rect.width - 10;
		float preSize = (availableWidth - (10 * (size-1))) / size;
		blockSize = preSize * 3;

		BlockPrefab.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(blockSize,blockSize);
	}
}


