using UnityEngine;
using System.Collections;

public class Player : Entity {

    public bool isTurn = false;
    public int p_tag = 0;
    public int turnNumber = 0;
    public int HP = 0;
    public int range = 2;

    public int[] position = new int[2] { 0, 0 };
    public int[] card_id = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public GameObject manager;
    public Stage1_Manager m;

    public Card[] hand = new Card[3];

    private bool hold = false;
    private bool isAggr = true;
    private bool isPass = true;
    private int step = 1;

    private int[] startPosition = new int[2];
    private int[] endPosition = new int[2];

    private AnimationController ac;
    private SpriteRenderer sr;

    private Deck deck = new Deck();
    private Deck fold = new Deck();

    void Start () {
        m = manager.GetComponent<Stage1_Manager>();
        ac = this.transform.GetChild(0).GetComponent<AnimationController>();
        sr = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        deck = new Deck(card_id);
        deck.Shuffle();
        hand[0] = deck.Draw();
        hand[1] = deck.Draw();
        hand[2] = deck.Draw();
    }

    void Update () {

        //位置呈現
        this.transform.position = new Vector3(Horizontalposition(position[1]), Verticallposition(position[0]), 0);
        sr.sortingOrder = position[0];

        //死亡事件
        if (HP <= 0) {
            m.board[position[0], position[1], 0] = "n";
            Destroy(this.gameObject);
        }

        //回合行動
        if (isTurn) {

            if (step == 1 && Input.GetKeyDown(KeyCode.Space)) { //按下空白鍵來操作玩家

                if (m.cardBoard[position[0], position[1], 1].ID != 0) {
                    fold.Add(m.cardBoard[position[0], position[1], 1]);
                    m.cardBoard[position[0], position[1], 1].Clear();
                }

                if (hold == false && m.board[m.position[0], m.position[1], 0] == ("p" + p_tag.ToString())) { //若格子有玩家則將其抓起
                    startPosition[0] = m.position[0]; //記下玩家初始位置
                    startPosition[1] = m.position[1];
                    hold = true; //抓起
                }
                if (hold == true && m.board[m.position[0], m.position[1], 0] == "n" && Mathf.Abs(m.position[0] - startPosition[0]) + Mathf.Abs(m.position[1] - startPosition[1]) <= range) { //若格子上無人則將其放下
                    endPosition[0] = m.position[0]; //記下玩家結束位置
                    endPosition[1] = m.position[1];
                    hold = false; //放下
                    m.board[startPosition[0], startPosition[1], 0] = "n"; //將原始位置改為n
                    m.board[endPosition[0], endPosition[1], 0] = "p" + p_tag.ToString();
                    position[0] = endPosition[0]; //移動玩家
                    position[1] = endPosition[1];
                    isAggr = true;
                    isPass = true;
                    step++;
                }
            }

            if (step == 2 && Input.GetKeyDown(KeyCode.Alpha1) && hand[0].ID != 0) {
                if (hand[0].Action == "attack" && isAggr && ActDirection(position, m.position) != "n") {
                    m.board[position[0], position[1], 1] = ActDirection(position, m.position);
                    m.cardBoard[position[0], position[1], 0] = new Card(hand[0]);
                    fold.Add(hand[0]);
                    hand[0].Clear();
                    m.CheckAggr();
                    //ac.Attack();
                    isAggr = false;
                }
                else if (hand[0].Action == "defence" && isPass) {
                    m.cardBoard[position[0], position[1], 1] = new Card(hand[0]);
                    hand[0].Clear();
                    isPass = false;
                }
            }

            if (step == 2 && Input.GetKeyDown(KeyCode.Alpha2) && hand[1].ID != 0) {
                if (hand[1].Action == "attack" && isAggr && ActDirection(position, m.position) != "n") {
                    m.board[position[0], position[1], 1] = ActDirection(position, m.position);
                    m.cardBoard[position[0], position[1], 0] = new Card(hand[1]);
                    fold.Add(hand[1]);
                    hand[1].Clear();
                    m.CheckAggr();
                    //ac.Attack();
                    isAggr = false;
                }
                else if (hand[1].Action == "defence" && isPass) {
                    m.cardBoard[position[0], position[1], 1] = new Card(hand[1]);
                    hand[1].Clear();
                    isPass = false;
                }
            }

            if (step == 2 && Input.GetKeyDown(KeyCode.Alpha3) && hand[2].ID != 0) {
                if (hand[2].Action == "attack" && isAggr && ActDirection(position, m.position) != "n") {
                    m.board[position[0], position[1], 1] = ActDirection(position, m.position);
                    m.cardBoard[position[0], position[1], 0] = new Card(hand[2]);
                    fold.Add(hand[2]);
                    hand[2].Clear();
                    m.CheckAggr();
                    //ac.Attack();
                    isAggr = false;
                }
                else if (hand[2].Action == "defence" && isPass) {
                    m.cardBoard[position[0], position[1], 1] = new Card(hand[2]);
                    hand[2].Clear();
                    isPass = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Z)) {
                print("hand[0] is " + hand[0].ID);
                print("hand[1] is " + hand[1].ID);
                print("hand[2] is " + hand[2].ID);
                print("your deck is");
                deck.Show();
                print("your fold is");
                fold.Show();
            }

            if (Input.GetKeyDown(KeyCode.X)) {
                step = 1;
                for (int i = 0; i < 3; i++) {
                    if (hand[i].ID == 0) {
                        hand[i] = deck.Draw();
                    }
                }
                m.EndTurn();
            }
        }

    }

    static string ActDirection (int[] position, int[] selectPosition) {
        if (selectPosition[0] - position[0] == -1 && selectPosition[1] - position[1] == 0)
            return "up";
        else if (selectPosition[0] - position[0] == 1 && selectPosition[1] - position[1] == 0)
            return "down";
        else if (selectPosition[0] - position[0] == 0 && selectPosition[1] - position[1] == 1)
            return "right";
        else if (selectPosition[0] - position[0] == 0 && selectPosition[1] - position[1] == -1)
            return "left";
        else
            return "n";
    }

    private void ResetDeck (ref Deck deck, ref Deck fold) {
        deck = new Deck(fold);
        deck.Shuffle();
        fold.Clear();
    }

}
