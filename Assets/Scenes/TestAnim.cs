using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TestAnim : MonoBehaviour
{
    private GameObject bullet;
    public GameObject tile;

    private void Awake()
    {
        bullet = gameObject;
    }
    public void FireToTile()
    {
		Vector3 TilePos = new Vector3(tile.transform.localPosition.x, tile.transform.localPosition.y, 0);
		LeanTween.moveLocal(bullet, TilePos, 1.5f);
        Destroy(bullet, 1.5f);
    }

}
