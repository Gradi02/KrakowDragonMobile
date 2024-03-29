using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameLoopMng : MonoBehaviour
{
    //====================
    [SerializeField] private Base_generator generator;
    [SerializeField] private Card_Manager manager;
    [SerializeField] private QueueGenerator queueM;
    [SerializeField] private GameObject blocker;
    [SerializeField] private Transform goldIncomeSpawner;
    [SerializeField] private GameObject incomePref;
    [SerializeField] private GameObject startCanva;
    [SerializeField] private GameObject overCanva;
    [SerializeField] private TextMeshProUGUI overCanvaTitle;
    [SerializeField] private TextMeshProUGUI overCanvaSubTitle;
    [SerializeField] private TextMeshProUGUI overCanvaNumber;
    [SerializeField] private Animations anim;
    //====================

    public CardsScriptableObject DragonCard, CastleCard;
    private Vector2 DragonSpawnTilePos, CastleSpawnTilePos;
    private GameObject DragonTile;
    private GameObject CastleTile;

    private void Start()
    {
        DragonSpawnTilePos = new Vector2(generator.size - 1, generator.size - 1);
        CastleSpawnTilePos = new Vector2(0, 0);
        blocker.SetActive(true);
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

        blocker.SetActive(false);
        //Animacja startu
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

        anim.ShowTurnAnimation(true);
    }

    [ContextMenu("end")]
    public void GameOver()
    {
        blocker.SetActive(true);

        overCanvaTitle.text = "Game Over!";
        overCanvaSubTitle.text = "Defeated at round:";
        overCanvaNumber.text = queueM.GetTotalTurnsCount().ToString();

        LeanTween.moveLocal(overCanva, new Vector3(0, 0, 0), 2f).setEase(LeanTweenType.easeInOutSine);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayerMoved()
    {
        blocker.SetActive(true);
        queueM.UpdateGameQueue();
        CalculateGoldIncome();
        manager.TowersShot();


        if (!queueM.IsPlayerToMove())
        {
            StartCoroutine(DragonMove());
        }
        else
        {
            anim.ShowTurnAnimation(true);
        }
    }

    public void Win()
    {
        blocker.SetActive(true);

        overCanvaTitle.text = "You Won!";
        overCanvaSubTitle.text = "Krakow saved at round:";
        overCanvaNumber.text = queueM.GetTotalTurnsCount().ToString();

        LeanTween.moveLocal(overCanva, new Vector3(0, 0, 0), 2f).setEase(LeanTweenType.easeInOutSine);
    }
    private IEnumerator DragonMove()
    {
        blocker.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        while (!queueM.IsPlayerToMove())
        {
            anim.ShowTurnAnimation(false);
            yield return new WaitForSeconds(3f);
            DragonTurn();
            yield return new WaitForSeconds(3f);
        }

        blocker.SetActive(false);
    }

    private void DragonTurn()
    {
        GetComponent<DragonAI>().DragonQueueTurn();
        queueM.UpdateGameQueue();
        CalculateGoldIncome();
    }

    private void CalculateGoldIncome()
    {
        int income = GetComponent<Card_Manager>().CalcGoldIncome();

        GameObject inc = Instantiate(incomePref, Vector3.zero, Quaternion.identity, goldIncomeSpawner);
        inc.transform.localPosition = Vector3.zero;
        inc.GetComponent<TextMeshProUGUI>().text = "+" + income;
    }

    public void SetBlocker(bool bin)
    {
        blocker.SetActive(bin);
    }

    public void StartGameButton()
    {
        LeanTween.moveLocal(startCanva, new Vector3(0, 1100, 0), 1f).setEase(LeanTweenType.easeInCirc).destroyOnComplete = true; 
        GetComponent<Base_generator>().StartGame();
    }
}
