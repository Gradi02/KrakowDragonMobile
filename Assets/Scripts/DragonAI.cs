using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAI : MonoBehaviour
{
    [Header("References")]
    private List<TileInfo> mapTiles = new List<TileInfo>();
    [SerializeField] private Transform dragon;

    private struct GradedTiles
    {
        public TileInfo info;
        public int grade;
    }




    public void DragonQueueTurn()
    {
        GradedTiles move = PickMove();

        Debug.Log("Wybrano pole: " + move.info.currentCard);
    }

    private GradedTiles PickMove()
    {
        GradedTiles[] allGradedTiles = GradeAllTiles();
        int sum = 0;

        foreach(GradedTiles g in allGradedTiles)
        {
            sum += g.grade;
        }

        int chance = Random.Range(0, sum);

        foreach (GradedTiles g in allGradedTiles)
        {
            if(chance < g.grade)
            {
                return g;
            }
        }

        return allGradedTiles[Random.Range(0, allGradedTiles.Length)];
    }
    
    private GradedTiles[] GradeAllTiles()
    {
        GradedTiles[] gradedTiles = new GradedTiles[mapTiles.Count];

        for(int i=0; i<mapTiles.Count; i++)
        {
            //oceñ wartoœæ danej karty
            if (mapTiles[i].currentCard != null)
            {
                gradedTiles[i].grade = mapTiles[i].currentCard.value;
            }
            else
            {
                gradedTiles[i].grade = 1;
            }

            gradedTiles[i].info = mapTiles[i];

            //oceñ odleg³oœæ danej karty
            //float dist = Vector2.Distance(dragon.position, mapTiles[i].transform.position);


            //oceñ iloœæ danego typu karty
            int count = CountOfCardOnMap(mapTiles[i].currentCard);
            
            if(count > 1)
            {
                gradedTiles[i].grade += count - 1;
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

    public void AddMapTile(TileInfo Ntile)
    {
        mapTiles.Add(Ntile);
    }
}
