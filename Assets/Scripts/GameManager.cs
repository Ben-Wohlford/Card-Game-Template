using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

//using Random = System.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    private GameObject Canvas;
    public static GameManager gm;
    public List<Card> deck = new List<Card>();
    public List<Card> playerHand = new List<Card>();
    private int playerSum;
    public List<Card> aiHand = new List<Card>();
    private int aiSum;

    public DrawButtonScript drawButton;
    public bool drawCard;
    private bool isPlayerTurn;
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
        Deal();
        isPlayerTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerTurn)
        {
            PlayerTurn();
        }
        else
        {
            AITurn();
        }
    }

    void Deal()
    {
        int draw = 2;
        for (int x = 0; x < draw; x++)
        {
            //Draw initial cards for player and COM
            int drawCard = Random.Range(0, deck.Count);
            Card current = Instantiate(deck[drawCard], new Vector3(200+(x*150), 100, 0), quaternion.identity);
            current.transform.SetParent(Canvas.transform);
            Debug.Log("Card: " + deck[drawCard].value);
            playerHand.Add(deck[drawCard]);
            deck.Remove(deck[drawCard]);
            int drawAICard = Random.Range(0, deck.Count);
            Debug.Log("AI Card: " + deck[drawAICard].value);
            aiHand.Add(deck[drawAICard]);
            deck.Remove(deck[drawAICard]);
        }
        //Spawn cards
        Card first = Instantiate(aiHand[0], new Vector3(200, 550, 0), quaternion.identity);
        first.transform.SetParent(Canvas.transform);
        GameObject second = Instantiate(cardBack, new Vector3(350, 550, 0), quaternion.identity);
        second.transform.SetParent(Canvas.transform);
        //Find inital hand values
        for (int x = 0; x < playerHand.Count; x++)
        {
            playerSum += playerHand[x].value;
        }
        for (int x = 0; x < aiHand.Count; x++)
        {
            aiSum += aiHand[x].value;
        }
        for (int x = 0; x < playerHand.Count; x++)
        {
            if (playerHand[x].value == 1 && playerSum < 12)
            {
                playerSum += 10;
                break;
            }
        }
        for (int x = 0; x < aiHand.Count; x++)
        {
            if (aiHand[x].value == 1 && aiSum < 12)
            {
                aiSum += 10;
                break;
            }
        }
        Debug.Log("Sum: " + playerSum);
        Debug.Log("AI Sum: " + aiSum);
    }

    void PlayerTurn()
    {
        drawCard = drawButton.isClicked;
        if (drawCard)
        {
            int drawCard = Random.Range(0, deck.Count);
            Card current = Instantiate(deck[drawCard], new Vector3(200+(playerHand.Count*150), 100, 0), quaternion.identity);
            current.transform.SetParent(Canvas.transform);
            Debug.Log("Card: " + deck[drawCard].value);
            playerHand.Add(deck[drawCard]);
            deck.Remove(deck[drawCard]);
        }
    }
    
    void AITurn()
    {

    }
}