using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Card_data data;

    public int value;
    public bool isAce;
    public Sprite sprite;
    public Image spriteImage;
        

    // Start is called before the first frame update
    void Start()
    {
        value = data.value;
        isAce = data.isAce;
        sprite = data.sprite;
        spriteImage.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
