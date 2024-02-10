using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TestAnim : MonoBehaviour
{
    public GameObject bullet;
    public GameObject tile;
    public void FireToTile()
    {
		Vector3 TilePos = new Vector3(tile.transform.position.x, tile.transform.position.y, 0);
		LeanTween.moveLocal(bullet, TilePos, 3f).setEase(LeanTweenType.easeInSine); 
    }

}
