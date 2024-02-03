using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAI : MonoBehaviour
{
    [Header("References")]
    private List<TileInfo> mapTiles = new List<TileInfo>();
    //przekazaæ tablice ze skryptu tworz¹cego mape

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
            if (mapTiles[i].currentCard != null)
            {
                gradedTiles[i].grade = mapTiles[i].currentCard.value;
            }
            else
            {
                gradedTiles[i].grade = 1;
            }

            gradedTiles[i].info = mapTiles[i];
        }

        return gradedTiles;
    }

    public void AddMapTile(TileInfo Ntile)
    {
        mapTiles.Add(Ntile);
    }
}
