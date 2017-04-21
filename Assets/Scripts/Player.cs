using UnityEngine;
using System.Collections;

public class Player : Character {

    //public bool isTurn = true;
    //public bool isActing = false;
    public bool isMoving = false;
    //public int HP = 0;
    //public int range = 2;
    //public int stamina = 5;
    //public int stamina_max = 5;
    //public int recovery = 2;
    //public string label;

    //public int[] position = new int[2] { 0, 0 };
    //public int[] card_id = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public GameObject manager;
    private Stage1_Manager m;
    //public Card pass = new Card();

    //public Card[] hand = new Card[3];

    public int step = 0;

    private int[] startPosition = new int[2];
    private int[] endPosition = new int[2];

    private AnimationController ac;
    private SpriteRenderer sr;

    //public Deck deck = new Deck();
    //public Deck fold = new Deck();

    void Start () {
        label = type + order;
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
            m.DeathEvent(this);
            //m.DeathEvent(position, label);
            //Destroy(this.gameObject);
        }

        //player's turn
        if (isTurn) {

            //check status
            if (status == "dead") {
                isActing = false;
            }

            //select player
            if (status != "dead" && !isActing && Input.GetKeyDown(KeyCode.Z) && m.board.GetCharacterLabel(m.position[0], m.position[1]) == label) {
                m.activatePlayer(label);
            }

            //cancel selection
            if (isActing && Input.GetKeyDown(KeyCode.X)) {
                if (step == 2) {
                    step = 1;
                }
                isMoving = false;
                isActing = false;
            }

            //recycle passive card and recover stamina
            if (step == 0) {
                if (pass.ID != 0) {
                    fold.Add(pass);
                    pass.Clear();
                }
                stamina += recovery;
                stamina = Mathf.Clamp(stamina, 0, stamina_max);
                step++;
            }

            //move player
            if (step == 2 && isActing == true && Input.GetKeyDown(KeyCode.Z) && (m.board.GetCharacterLabel(m.position[0], m.position[1]) == "n" || m.board.GetCharacterLabel(m.position[0], m.position[1]) == label) && Mathf.Abs(m.position[0] - startPosition[0]) + Mathf.Abs(m.position[1] - startPosition[1]) <= range) {
                endPosition[0] = m.position[0];
                endPosition[1] = m.position[1];
                m.board.SetCharacterLabel(startPosition[0], startPosition[1], "n");
                m.board.SetCharacterLabel(endPosition[0], endPosition[1], label);
                position[0] = endPosition[0];
                position[1] = endPosition[1];
                isMoving = false;
                step++;
            }

            //select player for moving
            /*if (step == 1 && isActing == true && Input.GetKeyDown(KeyCode.Z) && m.board[m.position[0], m.position[1], 0] == (label)) {
                //prepare for moving
                startPosition[0] = m.position[0];
                startPosition[1] = m.position[1];
                isMoving = true;
                step++;
            }*/
            if (step == 1 && isActing == true && Input.GetKeyDown(KeyCode.Z) && m.board.GetCharacterLabel(m.position[0], m.position[1]) == (label)) {
                //prepare for moving
                startPosition[0] = m.position[0];
                startPosition[1] = m.position[1];
                isMoving = true;
                step++;
            }

            //play card
            if (step == 3 && isActing == true && Input.GetKeyDown(KeyCode.Alpha1) && hand[0].ID != 0) {
                if (m.PlayCard(position[0], position[1], m.position[0], m.position[1], hand[0])) {
                    //ac.Attack();
                    fold.Add(hand[0]);
                    hand[0].Clear();
                }
                /*if (hand[0].Action == "attack" && hand[0].Cost <= stamina && ActDirection(position, m.position) != "n") {
                    //m.board[position[0], position[1], 1] = ActDirection(position, m.position);
                    //m.cardBoard[position[0], position[1], 0] = new Card(hand[0]);
                    stamina -= hand[0].Cost;
                    hand[0].SetTarget(m.GetTarget(position[0], position[1], 1, ActDirection(position, m.position)));
                    m.board.SetAggr(position[0], position[1], hand[0]);
                    m.CheckAggr();
                    ac.Attack();
                    fold.Add(hand[0]);
                    hand[0].Clear();
                }
                else if (hand[0].Action == "defence" && hand[0].Cost <= stamina) {
                    //m.cardBoard[position[0], position[1], 1] = new Card(hand[0]);
                    //m.board.SetPass(position[0], position[1], hand[0]);
                    stamina -= hand[0].Cost;
                    pass = new Card(hand[0]);
                    hand[0].Clear();
                }*/
            }

            if (step == 3 && isActing == true && Input.GetKeyDown(KeyCode.Alpha2) && hand[1].ID != 0) {
                if (m.PlayCard(position[0], position[1], m.position[0], m.position[1], hand[1])) {
                    //ac.Attack();
                    fold.Add(hand[1]);
                    hand[1].Clear();
                }
            }

            if (step == 3 && isActing == true && Input.GetKeyDown(KeyCode.Alpha3) && hand[2].ID != 0) {
                if (m.PlayCard(position[0], position[1], m.position[0], m.position[1], hand[2])) {
                    //ac.Attack();
                    fold.Add(hand[2]);
                    hand[2].Clear();
                }
            }

            //play card
            /*if (step == 3 && isActing == true && Input.GetKeyDown(KeyCode.Alpha2) && hand[1].ID != 0) {
                if (hand[1].Action == "attack" && hand[1].Cost <= stamina && ActDirection(position, m.position) != "n") {
                    //m.board[position[0], position[1], 1] = ActDirection(position, m.position);
                    //m.cardBoard[position[0], position[1], 0] = new Card(hand[1]);
                    stamina -= hand[1].Cost;
                    hand[1].SetTarget(m.GetTarget(position[0], position[1], 1, ActDirection(position, m.position)));
                    m.board.SetAggr(position[0], position[1], hand[1]);
                    m.CheckAggr();
                    ac.Attack();
                    fold.Add(hand[1]);
                    hand[1].Clear();
                }
                else if (hand[1].Action == "defence" && hand[1].Cost <= stamina) {
                    //m.cardBoard[position[0], position[1], 1] = new Card(hand[1]);
                    //m.board.SetPass(position[0], position[1], hand[1]);
                    stamina -= hand[1].Cost;
                    pass = new Card(hand[1]);
                    hand[1].Clear();
                }
            }

            //play card
            if (step == 3 && isActing == true && Input.GetKeyDown(KeyCode.Alpha3) && hand[2].ID != 0) {
                if (hand[2].Action == "attack" && hand[2].Cost <= stamina && ActDirection(position, m.position) != "n") {
                    //m.board[position[0], position[1], 1] = ActDirection(position, m.position);
                    //m.cardBoard[position[0], position[1], 0] = new Card(hand[2]);
                    stamina -= hand[2].Cost;
                    hand[2].SetTarget(m.GetTarget(position[0], position[1], 1, ActDirection(position, m.position)));
                    m.board.SetAggr(position[0], position[1], hand[2]);
                    m.CheckAggr();
                    ac.Attack();
                    fold.Add(hand[2]);
                    hand[2].Clear();
                }
                else if (hand[2].Action == "defence" && hand[2].Cost <= stamina) {
                    //m.cardBoard[position[0], position[1], 1] = new Card(hand[2]);
                    //m.board.SetPass(position[0], position[1], hand[2]);
                    stamina -= hand[2].Cost;
                    pass = new Card(hand[2]);
                    hand[2].Clear();
                }
            }*/

            //hand, deck and fold info
            if (Input.GetKeyDown(KeyCode.I)) {
                print("hand1 is " + hand[0].ID);
                print("hand2 is " + hand[1].ID);
                print("hand3 is " + hand[2].ID);
                print("your deck is");
                for (int i = 0; i < deck.Length(); i++) {
                    print("(" + deck.Card(i).Action + "," + deck.Card(i).Type + "," + deck.Card(i).Value.ToString() + ")");
                }
                print("your fold is");
                for (int i = 0; i < fold.Length(); i++) {
                    print("(" + fold.Card(i).Action + "," + fold.Card(i).Type + "," + fold.Card(i).Value.ToString() + ")");
                }
            }

            //end turn in class Player
            /*if (Input.GetKeyDown(KeyCode.C)) {
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
                m.EndTurn(type);
            }*/

        }

    }



}
