using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

//using Random = System.Random;

public class GameManager : MonoBehaviour
{
    private GameObject Canvas;
    public static GameManager gm;
    public List<Card> deck = new List<Card>();
    public List<Card> playerHand = new List<Card>();
    private int playerSum;
    public List<Card> ai_hand = new List<Card>();
    private void Awake()
    {
        if (gm != null && gm != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gm = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
        int playerDraw = 2;
        for (int x = 0; x < playerDraw; x++)
        {
            int drawCard = Random.Range(0, deck.Count);
            Card current = Instantiate(deck[drawCard], new Vector3(200+(x*150), 100, 0), quaternion.identity);
            current.transform.SetParent(Canvas.transform);
            Debug.Log("Card: " + deck[drawCard].value);
            playerHand.Add(deck[drawCard]);
            deck.Remove(deck[drawCard]);
        }
        for (int x = 0; x < playerHand.Count; x++)
        {
            playerSum += playerHand[x].value;
        }
        for (int x = 0; x < playerHand.Count; x++)
        {
            if (playerHand[x].value == 1 && playerSum < 12)
            {
                playerSum += 10;
                break;
            }
        }
        Debug.Log("Sum: " + playerSum);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Deal()
    {

    }

    void Shuffle()
    {

    }

    void AI_Turn()
    {

    }
}