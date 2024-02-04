using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Base_generator : MonoBehaviour
{
	private float size = 7;
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
				Image g = Instantiate(BlockPrefab, Vector3.zero, Quaternion.identity, middle);
				g.transform.localPosition = new Vector3(i * spacing, j * spacing, 1) - centerPosition;

				//dodaj do tablicy mapy w DragonAI
				GetComponent<DragonAI>().AddMapTile(g.GetComponent<TileInfo>());

				//ustaw kordy pola
				g.GetComponent<TileInfo>().SetPositionInfo(i, j);
			}
		}

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


