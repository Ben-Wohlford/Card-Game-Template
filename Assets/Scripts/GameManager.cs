using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    
    private bool p10;
    private bool a10;
    private bool canBJ;

    public Button drawCard;
    public Button endTurn;
    public Button blackjack;
    // private void Awake()
    // {
    //     if (gm != null && gm != this)
    //     {
    //         Destroy(gameObject);
    //     }
    //     else
    //     {
    //         gm = this;
    //         DontDestroyOnLoad(this.gameObject);
    //     }
    // }
    void Start()
    {
        //Debug.Log("Hello!");
        canBJ = true;
        blackjack.interactable = false;
        p10 = false;
        a10 = false;
        Canvas = GameObject.Find("Canvas");
        Deal();
    }
    void Deal()
    {
        //Draw initial cards for player and COM
        int draw = 2;
        for (int x = 0; x < draw; x++)
        {
            int drawCard = Random.Range(0, deck.Count);
            Card current = Instantiate(deck[drawCard], new Vector3(100+(x*150), 100, 0), quaternion.identity);
            current.transform.SetParent(Canvas.transform);
            //Debug.Log("Card: " + deck[drawCard].value);
            playerHand.Add(deck[drawCard]);
            deck.Remove(deck[drawCard]);
            int drawAICard = Random.Range(0, deck.Count);
            //Debug.Log("AI Card: " + deck[drawAICard].value);
            aiHand.Add(deck[drawAICard]);
            deck.Remove(deck[drawAICard]);
        }
        //Spawn cards
        Card first = Instantiate(aiHand[0], new Vector3(100, 550, 0), quaternion.identity);
        first.transform.SetParent(Canvas.transform);
        GameObject second = Instantiate(cardBack, new Vector3(250, 550, 0), quaternion.identity);
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
                p10 = true;
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
        //Debug.Log("Sum: " + playerSum);
        //Debug.Log("AI Sum: " + aiSum);
    }
    void Update()
    {
        if (canBJ)
        {
            //Allow blackjack
            if (playerSum == 21 && playerHand.Count == 2)
            {
                blackjack.interactable = true;
            }
            else
            {
                blackjack.interactable = false;
            }
        }
        else
        {
            blackjack.interactable = false;
        }
    }
    public void PlayerTurn()
    {
        //Draw card
        //Debug.Log("Draw Card");
        int drawCard = Random.Range(0, deck.Count);
        Card current = Instantiate(deck[drawCard], new Vector3(100+(playerHand.Count*150), 100, 0), quaternion.identity);
        current.transform.SetParent(Canvas.transform);
        //Debug.Log("Card: " + deck[drawCard].value);
        playerSum += deck[drawCard].value;
        playerHand.Add(deck[drawCard]);
        deck.Remove(deck[drawCard]);
        //Ace check
        for (int x = 0; x < playerHand.Count; x++)
        {
            if (playerHand[x].value == 1 && playerSum < 12 && p10 == false)
            {
                playerSum += 10;
                p10 = true;
                break;
            }
            if (playerHand[x].value == 1 && playerSum > 21 && p10)
            {
                playerSum -= 10;
                p10 = false;
                break;
            }
        }
        //Debug.Log("playerSum: " + playerSum);
        //Over 21
        if (playerSum > 21)
        {
            EndGame();
        }
    }
    public void AITurn()
    {
        //Draw card
        drawCard.interactable = false;
        if (aiSum < 17)
        {
            //Debug.Log("AI Draws Card");
            int drawCard = Random.Range(0, deck.Count);
            GameObject aicurrent = Instantiate(cardBack, new Vector3(100 + ((aiHand.Count) * 150), 550, 0),
                quaternion.identity);
            aicurrent.transform.SetParent(Canvas.transform);
            //Debug.Log("AI Card: " + deck[drawCard].value);
            aiSum += deck[drawCard].value;
            aiHand.Add(deck[drawCard]);
            deck.Remove(deck[drawCard]);
            //Ace check
            for (int x = 0; x < aiHand.Count; x++)
            {
                if (aiHand[x].value == 1 && aiSum < 12)
                {
                    aiSum += 10;
                    a10 = true;
                    break;
                }
                else if (aiHand[x].value == 1 && aiSum > 21 && a10)
                {
                    aiSum -= 10;
                    a10 = false;
                    break;
                }
            }
            //Debug.Log("aiSum: " + aiSum);
            //Stop drawing
            if (aiSum > 16)
            {
                EndGame();
            }
        }
        else if (aiSum > 26 && a10)
        {
            //Debug.Log("AI Draws Card");
            int drawCard = Random.Range(0, deck.Count);
            GameObject aicurrent = Instantiate(cardBack, new Vector3(100 + ((aiHand.Count) * 150), 550, 0),
                quaternion.identity);
            aicurrent.transform.SetParent(Canvas.transform);
            //Debug.Log("AI Card: " + deck[drawCard].value);
            aiSum += deck[drawCard].value;
            aiHand.Add(deck[drawCard]);
            deck.Remove(deck[drawCard]);
            //Ace check
            for (int x = 0; x < aiHand.Count; x++)
            {
                if (aiHand[x].value == 1 && aiSum < 12)
                {
                    aiSum += 10;
                    a10 = true;
                    break;
                }
                else if (aiHand[x].value == 1 && aiSum > 21 && a10)
                {
                    aiSum -= 10;
                    a10 = false;
                    break;
                }
            }
            //Debug.Log("aiSum: " + aiSum);
            //Stop drawing
            if (aiSum > 16)
            {
                EndGame();
            } 
        }
        else
        {
            EndGame();
        }
    }
    public void EndGame()
    {
        blackjack.interactable = false;
        canBJ = false;
        endTurn.interactable = false;
        drawCard.interactable = false;
        Debug.Log("Player sum: " + playerSum);
        Debug.Log("AI sum: " + aiSum);
        for (int x = 1; x < aiHand.Count; x++)
        {
            Card reveal = Instantiate(aiHand[x], new Vector3(100 + (x * 150), 550, 0), quaternion.identity);
            reveal.transform.SetParent(Canvas.transform);
        }
        if (playerSum > aiSum && playerSum < 22)
        {
            Debug.Log("You win!");
        }
        else if (playerSum < aiSum && aiSum > 21)
        {
            Debug.Log("You win!");
        }
        else
        {
            Debug.Log("You lose");
        }
    }
    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}