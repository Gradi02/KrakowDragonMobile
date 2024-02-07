using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DragonAI : MonoBehaviour
{
    [Header("References")]
    private List<TileInfo> mapTiles = new List<TileInfo>();
    private GameLoopMng mng;
    private Vector2[] moves = new Vector2[4];
    private int moveChance = 5;
    private bool end = false;
    private int movesCounter = 0;
    private int DragonHealth = 100;
    [SerializeField] private Slider hpslider;
    [SerializeField] private TextMeshProUGUI hptext;

    private void Awake()
    {
        mng = GetComponent<GameLoopMng>();
    }

    private struct GradedTiles
    {
        public TileInfo info;
        public int grade;
        public bool move; // 0-att || 1-move
    }


    private void UpdateDragonHpStats()
    {
        hpslider.value = DragonHealth;
        hptext.text = DragonHealth + " / " + hpslider.maxValue;
    }

    public void DragonQueueTurn()
    {
        if (end) return;

        GradedTiles pickedmove = PickMove();
        Debug.Log(pickedmove.move + " Pozycja: " + pickedmove.info.GetTilePosition() + " Ocena: " + pickedmove.grade + " pole: " + pickedmove.info.IfBlocked() + " ruchy: " + movesCounter++);

        if (pickedmove.move)
        {
            if(pickedmove.info.currentCard != null)
            {
                if(pickedmove.info.currentCard.card_name == "Castle")
                {
                    mng.GameOver();
                    end = true;
                    return;
                }
            }


            TileInfo lastDragonCard = GetComponent<Card_Manager>().GetDragonTile();
            lastDragonCard.SetBlock(false);
            pickedmove.info.SetCard(lastDragonCard.currentCard);
            pickedmove.info.SetBlock(true);
            lastDragonCard.RemoveCard();
        }
        else
        {
            pickedmove.info.DragonAttack();
        }
        GetComponent<Card_Manager>().AfterDragon();
    }

    private GradedTiles PickMove()
    {
        //sprawdz czy mo¿e siê ruszyæ
        if(CheckIfCanMove())
        {
            GradedTiles[] gradedMoves = GetGradedMoves();

            int rand = Random.Range(0, 10);
            if(rand < moveChance)
            {
                moveChance = 0;

                int sum_m = 0;
                foreach (var g in gradedMoves)
                {
                    sum_m += g.grade;
                }

                int chanceM = Random.Range(0, sum_m - 1);
                foreach (GradedTiles g in gradedMoves)
                {
                    if (chanceM < g.grade)
                    {
                        if (g.info != null)
                        {
                            return g;
                        }
                    }
                    else
                    {
                        chanceM -= g.grade;
                    }
                }

                Debug.LogError("B³¹d podczas wybierania losowego obiektu.");
                return BestMove(gradedMoves);
            }
            moveChance += 1;
        }

        GradedTiles[] allGradedTiles = GradeAllTiles();
        int sum = 0;

        foreach(GradedTiles g in allGradedTiles)
        {
            sum += g.grade;
        }

        int chance = Random.Range(0, sum - 1);

        foreach (GradedTiles g in allGradedTiles)
        {
            if(chance < g.grade)
            {
                return g;
            }
            else
            {
                chance -= g.grade;
            }
        }

        Debug.LogError("B³¹d podczas wybierania losowego obiektu.");
        return BestMove(allGradedTiles);
    }

    private GradedTiles BestMove(GradedTiles[] array)
    {
        GradedTiles maxValue = array[0];

        for (int i = 1; i < array.Length; i++)
        {
            if (array[i].grade > maxValue.grade)
            {
                if (!array[i].info.IfBlocked())
                {
                    maxValue = array[i];
                }
            }
        }

        return maxValue;
    }
    
    private GradedTiles[] GradeAllTiles()
    {
        GradedTiles[] gradedTiles = new GradedTiles[mapTiles.Count];
        TileInfo dragonTile = GetComponent<Card_Manager>().GetDragonTile();
        Vector2 dragonPos = dragonTile.GetTilePosition();

        for(int i=0; i<mapTiles.Count; i++)
        {
            gradedTiles[i].move = false;
            //oceñ wartoœæ danej karty
            if (mapTiles[i].currentCard != null)
            {
                gradedTiles[i].grade = mapTiles[i].currentCard.value;
            }
            else
            {
                if (!mapTiles[i].IfBlocked())
                {
                    gradedTiles[i].grade = 1;
                }
                else
                {
                    gradedTiles[i].grade = 0;
                }
            }

            gradedTiles[i].info = mapTiles[i];

            //oceñ odleg³oœæ danej karty
            if (mapTiles[i].currentCard != null && !mapTiles[i].IfBlocked() && mapTiles[i].currentCard.card_name != "Dragon")
            {
                Vector2 tilePos = mapTiles[i].GetComponent<TileInfo>().GetTilePosition();
                float dist = Vector2.Distance(tilePos, dragonPos);

                if (dist <= 3)
                {
                    gradedTiles[i].grade += 2;
                }
                else if (dist <= 5)
                {
                    gradedTiles[i].grade += 1;
                }          

                //oceñ iloœæ danego typu karty
            
                int count = CountOfCardOnMap(mapTiles[i].currentCard);

                if (count > 1)
                {
                    gradedTiles[i].grade += count;
                }
            }
        }



        return gradedTiles;
    }

    private int CountOfCardOnMap(CardsScriptableObject card)
    {
        int count = 0;

        foreach(TileInfo t in mapTiles)
        {
            if(t.currentCard == card)
            {
                count++;
            }
        }

        return count;
    }

    private bool CheckIfCanMove()
    {
        Card_Manager manager = GetComponent<Card_Manager>();
        TileInfo dragonTile = manager.GetDragonTile();
        Vector2 dragonPos = dragonTile.GetTilePosition();

        moves[0] = new Vector2(dragonPos.x + 1, dragonPos.y);
        moves[1] = new Vector2(dragonPos.x - 1, dragonPos.y);
        moves[2] = new Vector2(dragonPos.x, dragonPos.y + 1);
        moves[3] = new Vector2(dragonPos.x, dragonPos.y - 1);

        for (int i=0; i<4; i++)
        {
            GameObject mi = manager.GetTileByPosition(moves[i]);
            if (mi != null)
            {
                if (mi.GetComponent<TileInfo>().currentCard == null && !mi.GetComponent<TileInfo>().IfBlocked())
                {
                    return true;
                }
            }
        }

        return false;
    }

    private GradedTiles[] GetGradedMoves()
    {
        GradedTiles[] gradedTiles = new GradedTiles[4];
        Card_Manager manager = GetComponent<Card_Manager>();

        for (int i = 0; i < 4; i++)
        {
            gradedTiles[i].grade = 0;
            gradedTiles[i].move = true;

            GameObject mi = manager.GetTileByPosition(moves[i]);
            if (mi != null)
            {
                gradedTiles[i].info = mi.GetComponent<TileInfo>();

                if (gradedTiles[i].info.currentCard == null && !gradedTiles[i].info.IfBlocked())
                {
                    gradedTiles[i].grade = 2;
                }
                else if(gradedTiles[i].info.currentCard != null)
                {
                    if(gradedTiles[i].info.currentCard.card_name == "Castle")
                    {
                        gradedTiles[i].grade = 10;
                    }
                }
            }
        }

        TileInfo dragonTile = GetComponent<Card_Manager>().GetDragonTile();
        Vector2 dragonPos = dragonTile.GetTilePosition();
        float drg = dragonPos.x + dragonPos.y;

        for (int i = 0; i < 4; i++)
        {
            if (gradedTiles[i].info != null && gradedTiles[i].info.currentCard == null && !gradedTiles[i].info.IfBlocked())
            {
                float g_i = gradedTiles[i].info.GetTilePosition().x + gradedTiles[i].info.GetTilePosition().y;

                if (g_i < drg)
                {
                    gradedTiles[i].grade += 5;
                }
            }
        }

        return gradedTiles;
    }

    public void DamageToDragon(int n)
    {
        DragonHealth -= n;
        UpdateDragonHpStats();
    }

    public void AddMapTile(TileInfo Ntile)
    {
        mapTiles.Add(Ntile);
    }
}
