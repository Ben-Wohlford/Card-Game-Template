using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Card_data data;

    //public string card_name;
    //public string description;
    //public int health;
    //public int cost;
    public int value;
    public bool isAce;
    public Sprite sprite;
    //public TextMeshProUGUI nameText;
    //public TextMeshProUGUI descriptionText;
    //public TextMeshProUGUI healthText;
    //public TextMeshProUGUI costText;
    //public TextMeshProUGUI valueText;
    public Image spriteImage;
        

    // Start is called before the first frame update
    void Start()
    {
        //card_name = data.card_name;
        //description = data.description;
        //health = data.health;
        //cost = data.cost;
        value = data.value;
        isAce = data.isAce;
        sprite = data.sprite;
        //nameText.text = card_name;
        //descriptionText.text = description;
        //healthText.text = health.ToString();
        //costText.text = cost.ToString();
        //valueText.text = value.ToString();
        spriteImage.sprite = sprite;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
