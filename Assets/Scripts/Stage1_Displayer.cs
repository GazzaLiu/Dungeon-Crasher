using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stage1_Displayer : Entity {

    const int MAX_HAND = 3;
    const int MAX_PLAYER = 2;
    const int MAX_ENEMY = 2;

    public GameObject manager;
    public GameObject select;
    public GameObject playerSelect;
    public Text cardData;

    public GameObject[] character = new GameObject[MAX_PLAYER + MAX_ENEMY];
    public GameObject[] hand = new GameObject[2];
    public GameObject[] pass = new GameObject[2];
    public GameObject[] HP = new GameObject[MAX_PLAYER + MAX_ENEMY];
    public GameObject[] range = new GameObject[MAX_PLAYER];
    public GameObject[] aggr = new GameObject[MAX_ENEMY];
    public Sprite[] cardSprite = new Sprite[11];
    public Sprite[] enemyCardSprite = new Sprite[3];
    public Sprite[] numberSprite = new Sprite[10];
    public Sprite[] otherSprite;

    private int pointer = 0;

    private int[] cardIndex = new int[10] { 111, 112, 113, 121, 122, 123, 211, 212, 221, 222 };

    private Stage1_Manager m;

    private Player[] p = new Player[MAX_PLAYER];
    private Enemy[] e = new Enemy[MAX_ENEMY];
    private SpriteRenderer[] phsr = new SpriteRenderer[MAX_HAND];
    private SpriteRenderer[] ehsr = new SpriteRenderer[MAX_ENEMY];
    private SpriteRenderer[] psr = new SpriteRenderer[MAX_PLAYER + MAX_ENEMY];
    private SpriteRenderer[] asr = new SpriteRenderer[MAX_ENEMY];
    private SpriteRenderer[] hpsr = new SpriteRenderer[2 * (MAX_PLAYER + MAX_ENEMY)];

    void Start () {
        m = manager.GetComponent<Stage1_Manager>();

        for (int i = 0; i < MAX_PLAYER; i++) {
            p[i] = character[i].GetComponent<Player>();
            psr[i] = pass[0].transform.GetChild(i).GetComponent<SpriteRenderer>();
            hpsr[2 * i] = HP[i].transform.GetChild(0).GetComponent<SpriteRenderer>();
            hpsr[2 * i + 1] = HP[i].transform.GetChild(1).GetComponent<SpriteRenderer>();
        }

        for (int i = 0; i < MAX_ENEMY; i++) {
            e[i] = character[i + MAX_PLAYER].GetComponent<Enemy>();
            ehsr[i] = hand[1].transform.GetChild(i).GetComponent<SpriteRenderer>();
            psr[i + MAX_PLAYER] = pass[1].transform.GetChild(i).GetComponent<SpriteRenderer>();
            hpsr[2 * (i + MAX_PLAYER)] = HP[i + MAX_PLAYER].transform.GetChild(0).GetComponent<SpriteRenderer>();
            hpsr[2 * (i + MAX_PLAYER) + 1] = HP[i + MAX_PLAYER].transform.GetChild(1).GetComponent<SpriteRenderer>();
            asr[i] = aggr[i].GetComponent<SpriteRenderer>();
        }

        for (int i = 0; i < MAX_HAND; i++) {
            phsr[i] = hand[0].transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
    }

    void Update () {

        //display selection
        select.transform.position = new Vector3(Horizontalposition(m.position[1]), Verticallposition(m.position[0]), 0);

        //control displaying player's hand
        if (Input.GetKeyDown(KeyCode.Q)) {
            pointer = 0;
        }
        else if (Input.GetKeyDown(KeyCode.W)) {
            pointer = 1;
        }

        //display player's hand
        for (int i = 0; i < MAX_HAND; i++) {
            for (int j = 0; j < cardIndex.Length; j++) {
                if (p[pointer].hand[i].ID == cardIndex[j]) {
                    phsr[i].sprite = cardSprite[j];
                    break;
                }
                else {
                    phsr[i].sprite = cardSprite[cardSprite.Length - 1];
                }
            }
        }

        for (int i = 0; i < MAX_PLAYER; i++) {
            //display player's passive card
            if (p[i].pass.ID == 0) {
                psr[i].sprite = cardSprite[cardSprite.Length - 1];
            }
            else {
                for (int j = 0; j < cardIndex.Length; j++) {
                    if (p[i].pass.ID == cardIndex[j]) {
                        psr[i].sprite = cardSprite[j];
                        break;
                    }
                }
            }
            //display player's HP
            if (p[i].HP > 0) {
                hpsr[2 * i].sprite = numberSprite[p[i].HP % 10];
                hpsr[2 * i + 1].sprite = numberSprite[(p[i].HP - p[i].HP % 10) / 10];
            }
            else {
                HP[i].GetComponent<SpriteRenderer>().sprite = otherSprite[1];
                hpsr[2 * i].sprite = otherSprite[otherSprite.Length - 1];
                hpsr[2 * i + 1].sprite = otherSprite[otherSprite.Length - 1];
            }
        }

        for (int i = 0; i < MAX_ENEMY; i++) {
            //display enemy's hand
            ehsr[i].sprite = enemyCardSprite[e[i].GetHandNumber() - 1];
            //display enemy's passive card
            if (e[i].pass.ID == 0) {
                psr[i + MAX_PLAYER].sprite = cardSprite[cardSprite.Length - 1];
            }
            else {
                for (int j = 0; j < cardIndex.Length; j++) {
                    if (e[i].isPass && e[i].pass.ID == cardIndex[j]) {
                        psr[i + MAX_PLAYER].sprite = cardSprite[j];
                        break;
                    }
                    else {
                        psr[i + MAX_PLAYER].sprite = otherSprite[0];
                    }
                }
            }
            //display enemy's passive card
            if (e[i].aggr.ID == 0) {
                asr[i].sprite = cardSprite[cardSprite.Length - 1];
            }
            else {
                for (int j = 0; j < cardIndex.Length; j++) {
                    if (e[i].aggr.ID == cardIndex[j]) {
                        asr[i].sprite = cardSprite[j];
                        break;
                    }
                }
            }
            //display enemy's HP
            if (e[i].HP > 0) {
                hpsr[2 * (i + MAX_PLAYER)].sprite = numberSprite[e[i].HP % 10];
                hpsr[2 * (i + MAX_PLAYER) + 1].sprite = numberSprite[(e[i].HP - e[i].HP % 10) / 10];
            }
            else {
                HP[i + MAX_PLAYER].GetComponent<SpriteRenderer>().sprite = otherSprite[1];
                hpsr[2 * (i + MAX_PLAYER)].sprite = otherSprite[otherSprite.Length - 1];
                hpsr[2 * (i + MAX_PLAYER) + 1].sprite = otherSprite[otherSprite.Length - 1];
            }
        }

        //display player's selecion and control position
        if (pointer == 0) {
            playerSelect.transform.position = new Vector3(5.15f, -1.28f, 0);
        }
        else if (pointer == 1) {
            playerSelect.transform.position = new Vector3(8.251f, -1.28f, 0);
        }

        //display player's walk range
        for (int i = 0; i < p.Length; i++) {
            if (p[i].isMoving && p[i].isActing) {
                range[i].transform.position = p[i].transform.position;
            }
            else {
                range[i].transform.position = new Vector3(50, 50, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.F6)) {
            //player1's data
            cardData.text += "Player1\n" + "hand:";
            for (int i = 0; i < p[0].hand.Length; i++) {
                cardData.text += "(" + p[0].hand[i].Action + "," + p[0].hand[i].Type + "," + p[0].hand[i].Value.ToString() + ")";
            }
            cardData.text += "\ndeck:";
            for (int i = 0; i < p[0].deck.Length(); i++) {
                cardData.text += "(" + p[0].deck.Card(i).Action + "," + p[0].deck.Card(i).Type + "," + p[0].deck.Card(i).Value.ToString() + ")";
            }
            cardData.text += "\nfold:";
            for (int i = 0; i < p[0].fold.Length(); i++) {
                cardData.text += "(" + p[0].fold.Card(i).Action + "," + p[0].fold.Card(i).Type + "," + p[0].fold.Card(i).Value.ToString() + ")";
            }
            //player2's data
            cardData.text += "\nPlayer2\n" + "hand:";
            for (int i = 0; i < p[1].hand.Length; i++) {
                cardData.text += "(" + p[1].hand[i].Action + "," + p[1].hand[i].Type + "," + p[1].hand[i].Value.ToString() + ")";
            }
            cardData.text += "\ndeck:";
            for (int i = 0; i < p[1].deck.Length(); i++) {
                cardData.text += "(" + p[1].deck.Card(i).Action + "," + p[1].deck.Card(i).Type + "," + p[1].deck.Card(i).Value.ToString() + ")";
            }
            cardData.text += "\nfold:";
            for (int i = 0; i < p[1].fold.Length(); i++) {
                cardData.text += "(" + p[1].fold.Card(i).Action + "," + p[1].fold.Card(i).Type + "," + p[1].fold.Card(i).Value.ToString() + ")";
            }
            //enemy1's data
            cardData.text += "\nEnemy1\n" + "hand:";
            for (int i = 0; i < e[0].hand.Length; i++) {
                cardData.text += "(" + e[0].hand[i].Action + "," + e[0].hand[i].Type + "," + e[0].hand[i].Value.ToString() + ")";
            }
            cardData.text += "\ndeck:";
            for (int i = 0; i < e[0].deck.Length(); i++) {
                cardData.text += "(" + e[0].deck.Card(i).Action + "," + e[0].deck.Card(i).Type + "," + e[0].deck.Card(i).Value.ToString() + ")";
            }
            cardData.text += "\nfold:";
            for (int i = 0; i < e[0].fold.Length(); i++) {
                cardData.text += "(" + e[0].fold.Card(i).Action + "," + e[0].fold.Card(i).Type + "," + e[0].fold.Card(i).Value.ToString() + ")";
            }
            //enemy2's data
            cardData.text += "\nEnemy2\n" + "hand:";
            for (int i = 0; i < e[1].hand.Length; i++) {
                cardData.text += "(" + e[1].hand[i].Action + "," + e[1].hand[i].Type + "," + e[1].hand[i].Value.ToString() + ")";
            }
            cardData.text += "\ndeck:";
            for (int i = 0; i < e[1].deck.Length(); i++) {
                cardData.text += "(" + e[1].deck.Card(i).Action + "," + e[1].deck.Card(i).Type + "," + e[1].deck.Card(i).Value.ToString() + ")";
            }
            cardData.text += "\nfold:";
            for (int i = 0; i < e[1].fold.Length(); i++) {
                cardData.text += "(" + e[1].fold.Card(i).Action + "," + e[1].fold.Card(i).Type + "," + e[1].fold.Card(i).Value.ToString() + ")";
            }
        }
        if (Input.GetKeyDown(KeyCode.F7)) {
            cardData.text = "";
        }

    }

}
