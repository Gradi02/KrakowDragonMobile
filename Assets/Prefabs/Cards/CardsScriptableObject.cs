using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName ="New Card", menuName = "Card")]
public class CardsScriptableObject : ScriptableObject
{
    public string card_name;
    public string card_desc;
    public int value;
    public int move_price;
    public Sprite name_background;
    public Sprite Icon;
}
