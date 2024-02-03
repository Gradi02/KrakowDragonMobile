using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Base_generator : MonoBehaviour
{
	private float size = 5;
	public Image BlockPrefab;
	public Camera cameraobj;
	public Transform middle;
	public int spacing = 150;

	void Start()
	{
		StartCoroutine(MapGenerator());
	}

	IEnumerator MapGenerator()
	{
		// Oblicz œrodek planszy
		Vector3 centerPosition = new Vector3((size - 1) * spacing / 2, (size - 1) * spacing / 2, 1);

		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				//yield return new WaitForSeconds(0.2f);
				Image g = Instantiate(BlockPrefab, Vector3.zero, Quaternion.identity, middle);
				g.transform.localPosition = new Vector3(i * spacing, j * spacing, 1) - centerPosition;

				//dodaj do tablicy mapy w DragonAI
				GetComponent<DragonAI>().AddMapTile(g.GetComponent<TileInfo>());
			}
		}

		yield return null;
	}
}


