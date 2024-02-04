using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopMng : MonoBehaviour
{
    //====================
    [SerializeField] private Base_generator generator;
    [SerializeField] private Card_Manager manager;
    [SerializeField] private QueueGenerator queueM;
    [SerializeField] private GameObject blocker;
    //====================

    private Vector2 DragonSpawnTilePos, CastleSpawnTilePos;

    private void Start()
    {
        DragonSpawnTilePos = new Vector2(generator.size - 1, 0);
        CastleSpawnTilePos = new Vector2(0, generator.size - 1);
        blocker.SetActive(false);
    }

    public void StartGameFnc()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        SpawnStartObjects();

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.4f);
            manager.GiveFreeCard();
        }
    }

    private void SpawnStartObjects()
    {
        GameObject DragonTile = manager.GetTileByPosition(DragonSpawnTilePos);
        GameObject CastleTile = manager.GetTileByPosition(CastleSpawnTilePos);

        if (DragonTile != null) DragonTile.GetComponent<TileInfo>().SetBlock(true);
        if (CastleTile != null) CastleTile.GetComponent<TileInfo>().SetBlock(true);
    }

    public void MoveQueue()
    {
        queueM.UpdateGameQueue();

        if(queueM.IsPlayerToMove()) // ruch gracza
        {
            blocker.SetActive(false);
        }
        else // ruch smoka
        {
            blocker.SetActive(true);
        }
    }
}
