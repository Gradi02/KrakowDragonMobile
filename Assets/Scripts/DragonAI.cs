using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAI : MonoBehaviour
{
    [Header("References")]
    private List<GameObject> mapTiles = new List<GameObject>();
    //przekazaæ tablice ze skryptu tworz¹cego mape

    private struct GradedTiles
    {
        //mapTile script
        public int grade;
    }
    
    private GradedTiles[] GradeAllTiles()
    {
        GradedTiles[] gradedTiles = new GradedTiles[mapTiles.Count];

        for(int i=0; i<mapTiles.Count; i++)
        {
            //gradedTiles[i].grade = mapTiles[i].currentCard.value;
        }

        return gradedTiles;
    }
}
