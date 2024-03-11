using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card_data", menuName = "Cards/Card_data", order = 1)]
public class Card_data : ScriptableObject
{
    //public string card_name;
    //public string description;
    //public int health;
    //public int cost;
    public int value;
    public bool isAce;
    public Sprite sprite;
}
