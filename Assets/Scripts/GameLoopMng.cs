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

    public CardsScriptableObject DragonCard, CastleCard;
    private Vector2 DragonSpawnTilePos, CastleSpawnTilePos;
    private GameObject DragonTile;
    private GameObject CastleTile;

    private void Start()
    {
        DragonSpawnTilePos = new Vector2(generator.size - 1, generator.size - 1);
        CastleSpawnTilePos = new Vector2(0, 0);
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

    //Spawn smoka i zamku
    private void SpawnStartObjects()
    {
        DragonTile = manager.GetTileByPosition(DragonSpawnTilePos);
        CastleTile = manager.GetTileByPosition(CastleSpawnTilePos);

        if (DragonTile != null)
        {
            DragonTile.GetComponent<TileInfo>().SetBlock(true);
            DragonTile.GetComponent<TileInfo>().SetCard(DragonCard);
        }
        if (CastleTile != null)
        {
            CastleTile.GetComponent<TileInfo>().SetBlock(true);
            CastleTile.GetComponent<TileInfo>().SetCard(CastleCard);
        }
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
