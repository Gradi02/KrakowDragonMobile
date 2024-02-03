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

        //  Pobraæ sk¹dœ aktualnie wybran¹ kartê
        //  CardsScriptableObject selectedCard
        //  currentCard = selectedCard;
    }
}
