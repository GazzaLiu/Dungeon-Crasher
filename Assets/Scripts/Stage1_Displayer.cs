using UnityEngine;
using System.Collections;

public class Stage1_Displayer : Entity {

    const int MAX_HAND = 3;
    const int MAX_PLAYER = 2;
    const int MAX_ENEMY = 2;

    public GameObject manager;
    public GameObject select;
    public GameObject playerSelect;

    public GameObject[] character = new GameObject[MAX_PLAYER + MAX_ENEMY];
    public GameObject[] hand = new GameObject[2];
    public GameObject[] pass = new GameObject[2];
    public GameObject[] HP = new GameObject[MAX_PLAYER + MAX_ENEMY];
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
                    if (e[i].isAttacked && e[i].pass.ID == cardIndex[j]) {
                        psr[i + MAX_PLAYER].sprite = cardSprite[j];
                        break;
                    }
                    else {
                        psr[i + MAX_PLAYER].sprite = otherSprite[0];
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

    }

}
