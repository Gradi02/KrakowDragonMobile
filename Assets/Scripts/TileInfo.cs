using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileInfo : MonoBehaviour
{
    public CardsScriptableObject currentCard = null;
    private bool blockedTile = false;

    public void PlaceCardOnTile()
    {
        if (blockedTile || currentCard != null) return;

        //  Pobra� sk�d� aktualnie wybran� kart�
        //  CardsScriptableObject selectedCard
        //  currentCard = selectedCard;
    }
}
