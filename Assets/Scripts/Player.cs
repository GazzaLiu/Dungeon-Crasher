using UnityEngine;
using System.Collections;

public class Player : Entity {

    public bool isTurn = true;
    public bool isActing = false;
    public bool isMoving = false;
    public int p_tag = 0;
    public int HP = 0;
    public int range = 2;

    public int[] position = new int[2] { 0, 0 };
    public int[] card_id = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public GameObject manager;
    public Stage1_Manager m;
    public Card pass = new Card();

    public Card[] hand = new Card[3];

    private bool isAggr = true;
    private bool isPass = true;
    public int step = 0;

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
        pass = new Card();
        deck = new Deck(card_id);
        deck.Shuffle();
        hand[0] = deck.Draw();
        hand[1] = deck.Draw();
        hand[2] = deck.Draw();
    }

    void Update () {

        //display position
        this.transform.position = new Vector3(Horizontalposition(position[1]), Verticallposition(position[0]), 0);
        sr.sortingOrder = position[0];

        //death event
        if (HP <= 0) {
            m.DeathEvent(position, "p" + p_tag.ToString());
            Destroy(this.gameObject);
        }

        //player's turn
        if (isTurn) {

            //select player
            if (isActing == false && Input.GetKeyDown(KeyCode.Z) && m.board[m.position[0], m.position[1], 0] == ("p" + p_tag.ToString())) {
                m.ActivatePlayer("p" + p_tag.ToString());
            }

            //cancel selection
            if (isActing == true && Input.GetKeyDown(KeyCode.X)) {
                if (step == 2) {
                    step = 1;
                }
                isMoving = false;
                isActing = false;
            }

            //recycle passive card
            if (step == 0) {
                if (m.cardBoard[position[0], position[1], 1].ID != 0) {
                    fold.Add(m.cardBoard[position[0], position[1], 1]);
                    m.cardBoard[position[0], position[1], 1].Clear();
                    pass.Clear();
                }
                step++;
            }

            //move player
            if (step == 2 && isActing == true && Input.GetKeyDown(KeyCode.Z) && (m.board[m.position[0], m.position[1], 0] == "n" || m.board[m.position[0], m.position[1], 0] == "p" + p_tag.ToString()) && Mathf.Abs(m.position[0] - startPosition[0]) + Mathf.Abs(m.position[1] - startPosition[1]) <= range) {
                endPosition[0] = m.position[0];
                endPosition[1] = m.position[1];
                m.board[startPosition[0], startPosition[1], 0] = "n";
                m.board[endPosition[0], endPosition[1], 0] = "p" + p_tag.ToString();
                position[0] = endPosition[0];
                position[1] = endPosition[1];
                isMoving = false;
                isAggr = true;
                isPass = true;
                step++;
            }

            //select player for moving
            if (step == 1 && isActing == true && Input.GetKeyDown(KeyCode.Z) && m.board[m.position[0], m.position[1], 0] == ("p" + p_tag.ToString())) {
                //prepare for moving
                startPosition[0] = m.position[0];
                startPosition[1] = m.position[1];
                isMoving = true;
                step++;
            }

            //play card
            if (step == 3 && isActing == true && Input.GetKeyDown(KeyCode.Alpha1) && hand[0].ID != 0) {
                if (hand[0].Action == "attack" && isAggr && ActDirection(position, m.position) != "n") {
                    m.board[position[0], position[1], 1] = ActDirection(position, m.position);
                    m.cardBoard[position[0], position[1], 0] = new Card(hand[0]);
                    fold.Add(hand[0]);
                    hand[0].Clear();
                    m.CheckAggr();
                    ac.Attack();
                    isAggr = false;
                }
                else if (hand[0].Action == "defence" && isPass) {
                    m.cardBoard[position[0], position[1], 1] = new Card(hand[0]);
                    pass = new Card(hand[0]);
                    hand[0].Clear();
                    isPass = false;
                }
            }

            //play card
            if (step == 3 && isActing == true && Input.GetKeyDown(KeyCode.Alpha2) && hand[1].ID != 0) {
                if (hand[1].Action == "attack" && isAggr && ActDirection(position, m.position) != "n") {
                    m.board[position[0], position[1], 1] = ActDirection(position, m.position);
                    m.cardBoard[position[0], position[1], 0] = new Card(hand[1]);
                    fold.Add(hand[1]);
                    hand[1].Clear();
                    m.CheckAggr();
                    ac.Attack();
                    isAggr = false;
                }
                else if (hand[1].Action == "defence" && isPass) {
                    m.cardBoard[position[0], position[1], 1] = new Card(hand[1]);
                    pass = new Card(hand[1]);
                    hand[1].Clear();
                    isPass = false;
                }
            }

            //play card
            if (step == 3 && isActing == true && Input.GetKeyDown(KeyCode.Alpha3) && hand[2].ID != 0) {
                if (hand[2].Action == "attack" && isAggr && ActDirection(position, m.position) != "n") {
                    m.board[position[0], position[1], 1] = ActDirection(position, m.position);
                    m.cardBoard[position[0], position[1], 0] = new Card(hand[2]);
                    fold.Add(hand[2]);
                    hand[2].Clear();
                    m.CheckAggr();
                    ac.Attack();
                    isAggr = false;
                }
                else if (hand[2].Action == "defence" && isPass) {
                    m.cardBoard[position[0], position[1], 1] = new Card(hand[2]);
                    pass = new Card(hand[2]);
                    hand[2].Clear();
                    isPass = false;
                }
            }

            //hand, deck and fold info
            if (Input.GetKeyDown(KeyCode.I)) {
                print("hand1 is " + hand[0].ID);
                print("hand2 is " + hand[1].ID);
                print("hand3 is " + hand[2].ID);
                print("your deck is");
                deck.Show();
                print("your fold is");
                fold.Show();
            }

            //end turn
            if (Input.GetKeyDown(KeyCode.C)) {
                isActing = false;
                step = 0;
                for (int i = 0; i < 3; i++) {
                    if (hand[i].ID == 0) {
                        hand[i] = deck.Draw();
                        if (hand[i].ID == 0) {
                            ResetDeck();
                            hand[i] = deck.Draw();
                        }
                    }
                }
                m.EndTurn("p" + p_tag.ToString());
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

    private void ResetDeck () {
        deck = new Deck(fold);
        deck.Shuffle();
        fold.Clear();
    }

}
