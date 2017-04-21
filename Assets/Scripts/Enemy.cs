using UnityEngine;
using System;
using System.Collections;

public class Enemy : Character {

    //public bool isTurn = false;
    public bool test;
    //public bool isActing = true;
    public bool isPass = false;

    //public int e_tag = 0;
    //public int HP = 0;
    //public int range = 2;
    //public string label;

    //public int[] position = new int[2] { 0, 0 };
    //public int[] OldPosition = new int[2] { 0, 0 };
    //public int[] card_id = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public GameObject manager;
    private Stage1_Manager m;
    public Card aggr = new Card();
    //public Card pass = new Card();

    //public Card[] hand = new Card[3];

    /*private int randRow = 0;
    private int randColumn = 0;

    private int[] player1_up = new int[2];
    private int[] player1_down = new int[2];
    private int[] player1_left = new int[2];
    private int[] player1_right = new int[2];

    private int[] player2_up = new int[2];
    private int[] player2_down = new int[2];
    private int[] player2_left = new int[2];
    private int[] player2_right = new int[2];*/

    private SpriteRenderer sr;

    //public Deck deck = new Deck();
    //public Deck fold = new Deck();

    //private int[] Player1Position = new int[2] { 0, 0 };
    //private int[] Player2Position = new int[2] { 0, 0 };

    void Start () {
        label = type + order;
        m = manager.GetComponent<Stage1_Manager>();
        sr = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        aggr = new Card();
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
            //m.DeathEvent(position, "e" + e_tag.ToString());
            //Destroy(this.gameObject);
        }

        //enemy's turn
        if (isTurn) {
            if (status == "dead") {
                isActing = false;
                //print(label + "dead");
                m.enemyController(label, "end");
            }
            if (isActing) {
                StartCoroutine(Action());
                isActing = false;
            }
            //if (isActing && isTurn && m.enemyTurn % 4 == (e_tag * 2 - 2)) {

        }
    }

    private IEnumerator Action () {

        //yield return new WaitForSeconds(1.2f);

        //recycle passive card
        /*if (m.cardBoard[position[0], position[1], 1].ID != 0) {
            fold.Add(m.cardBoard[position[0], position[1], 1]);
            m.cardBoard[position[0], position[1], 1].Clear();
            pass.Clear();
        }
        isPass = false;*/

        //recycle passive card and recover stamina
        if (pass.ID != 0) {
            fold.Add(pass);
            pass.Clear();
        }
        stamina += recovery;
        stamina = Mathf.Clamp(stamina, 0, stamina_max);
        isPass = false;

        if (test) {
            m.board.SetCharacterLabel(0, 1, "n");
            position[0] = 6;
            position[1] = 1;
            m.board.SetCharacterLabel(position[0], position[1], label);
            m.PlayCard(position[0], position[1], 7, 1, new Card(113));
        }

        //yield return new WaitForSeconds(1.2f);

        //move + attack
        //PlayerPosition();
        //AssignAttackBlockPlayer1();
        //AssignAttackBlockPlayer2();
        //OldPosition[0] = position[0];
        //OldPosition[1] = position[1];
        /*if (IfCanGoToPlayer(player2_right, Player2Position, position) && m.board[player2_right[0], player2_right[1], 0] == "n" && m.liveList[1] != "p2_dead") {
            m.board[position[0], position[1], 0] = "n";
            position[0] = player2_right[0];
            position[1] = player2_right[1];
            m.board[position[0], position[1], 0] = "e" + e_tag.ToString();
            yield return new WaitForSeconds(1.2f);
            if (GetMaxCard(hand, "attack") != -1) {
                m.board[position[0], position[1], 1] = ActDirection(position, Player2Position);
                m.cardBoard[position[0], position[1], 0] = new Card(hand[GetMaxCard(hand, "attack")]);
                aggr = new Card(hand[GetMaxCard(hand, "attack")]);
                fold.Add(hand[GetMaxCard(hand, "attack")]);
                hand[GetMaxCard(hand, "attack")].Clear();
            }
        }
        else if (IfCanGoToPlayer(player1_up, Player1Position, position) && m.board[player1_up[0], player1_up[1], 0] == "n" && m.liveList[0] != "p1_dead") {
            m.board[position[0], position[1], 0] = "n";
            position[0] = player1_up[0];
            position[1] = player1_up[1];
            m.board[position[0], position[1], 0] = "e" + e_tag.ToString();
            yield return new WaitForSeconds(1.2f);
            if (GetMaxCard(hand, "attack") != -1) {
                m.board[position[0], position[1], 1] = ActDirection(position, Player1Position);
                m.cardBoard[position[0], position[1], 0] = new Card(hand[GetMaxCard(hand, "attack")]);
                aggr = new Card(hand[GetMaxCard(hand, "attack")]);
                fold.Add(hand[GetMaxCard(hand, "attack")]);
                hand[GetMaxCard(hand, "attack")].Clear();
            }

        }
        else if (IfCanGoToPlayer(player1_down, Player1Position, position) && m.board[player1_down[0], player1_down[1], 0] == "n" && m.liveList[0] != "p1_dead") {
            m.board[position[0], position[1], 0] = "n";
            position[0] = player1_down[0];
            position[1] = player1_down[1];
            m.board[position[0], position[1], 0] = "e" + e_tag.ToString();
            yield return new WaitForSeconds(1.2f);
            if (GetMaxCard(hand, "attack") != -1) {
                m.board[position[0], position[1], 1] = ActDirection(position, Player1Position);
                m.cardBoard[position[0], position[1], 0] = new Card(hand[GetMaxCard(hand, "attack")]);
                aggr = new Card(hand[GetMaxCard(hand, "attack")]);
                fold.Add(hand[GetMaxCard(hand, "attack")]);
                hand[GetMaxCard(hand, "attack")].Clear();
            }
        }
        else if (IfCanGoToPlayer(player1_left, Player1Position, position) && m.board[player1_left[0], player1_left[1], 0] == "n" && m.liveList[0] != "p1_dead") {
            m.board[position[0], position[1], 0] = "n";
            position[0] = player1_left[0];
            position[1] = player1_left[1];
            m.board[position[0], position[1], 0] = "e" + e_tag.ToString();
            yield return new WaitForSeconds(1.2f);
            if (GetMaxCard(hand, "attack") != -1) {
                m.board[position[0], position[1], 1] = ActDirection(position, Player1Position);
                m.cardBoard[position[0], position[1], 0] = new Card(hand[GetMaxCard(hand, "attack")]);
                aggr = new Card(hand[GetMaxCard(hand, "attack")]);
                fold.Add(hand[GetMaxCard(hand, "attack")]);
                hand[GetMaxCard(hand, "attack")].Clear();
            }
        }
        else if (IfCanGoToPlayer(player2_down, Player2Position, position) && m.board[player2_down[0], player2_down[1], 0] == "n" && m.liveList[1] != "p2_dead") {
            m.board[position[0], position[1], 0] = "n";
            position[0] = player2_down[0];
            position[1] = player2_down[1];
            m.board[position[0], position[1], 0] = "e" + e_tag.ToString();
            yield return new WaitForSeconds(1.2f);
            if (GetMaxCard(hand, "attack") != -1) {
                m.board[position[0], position[1], 1] = ActDirection(position, Player2Position);
                m.cardBoard[position[0], position[1], 0] = new Card(hand[GetMaxCard(hand, "attack")]);
                aggr = new Card(hand[GetMaxCard(hand, "attack")]);
                fold.Add(hand[GetMaxCard(hand, "attack")]);
                hand[GetMaxCard(hand, "attack")].Clear();
            }
        }
        else if (IfCanGoToPlayer(player1_right, Player1Position, position) && m.board[player1_right[0], player1_right[1], 0] == "n" && m.liveList[0] != "p1_dead") {
            m.board[position[0], position[1], 0] = "n";
            position[0] = player1_right[0];
            position[1] = player1_right[1];
            m.board[position[0], position[1], 0] = "e" + e_tag.ToString();
            yield return new WaitForSeconds(1.2f);
            if (GetMaxCard(hand, "attack") != -1) {
                m.board[position[0], position[1], 1] = ActDirection(position, Player1Position);
                m.cardBoard[position[0], position[1], 0] = new Card(hand[GetMaxCard(hand, "attack")]);
                aggr = new Card(hand[GetMaxCard(hand, "attack")]);
                fold.Add(hand[GetMaxCard(hand, "attack")]);
                hand[GetMaxCard(hand, "attack")].Clear();
            }
        }
        else if (IfCanGoToPlayer(player2_up, Player2Position, position) && m.board[player2_up[0], player2_up[1], 0] == "n" && m.liveList[1] != "p2_dead") {
            m.board[position[0], position[1], 0] = "n";
            position[0] = player2_up[0];
            position[1] = player2_up[1];
            m.board[position[0], position[1], 0] = "e" + e_tag.ToString();
            yield return new WaitForSeconds(1.2f);
            if (GetMaxCard(hand, "attack") != -1) {
                m.board[position[0], position[1], 1] = ActDirection(position, Player2Position);
                m.cardBoard[position[0], position[1], 0] = new Card(hand[GetMaxCard(hand, "attack")]);
                aggr = new Card(hand[GetMaxCard(hand, "attack")]);
                fold.Add(hand[GetMaxCard(hand, "attack")]);
                hand[GetMaxCard(hand, "attack")].Clear();
            }
        }
        else if (IfCanGoToPlayer(player2_left, Player2Position, position) && m.board[player2_left[0], player2_left[1], 0] == "n" && m.liveList[1] != "p2_dead") {
            m.board[position[0], position[1], 0] = "n";
            position[0] = player2_left[0];
            position[1] = player2_left[1];
            m.board[position[0], position[1], 0] = "e" + e_tag.ToString();
            yield return new WaitForSeconds(1.2f);
            if (GetMaxCard(hand, "attack") != -1) {
                m.board[position[0], position[1], 1] = ActDirection(position, Player2Position);
                m.cardBoard[position[0], position[1], 0] = new Card(hand[GetMaxCard(hand, "attack")]);
                aggr = new Card(hand[GetMaxCard(hand, "attack")]);
                fold.Add(hand[GetMaxCard(hand, "attack")]);
                hand[GetMaxCard(hand, "attack")].Clear();
            }
        }
        else {
            do {
                randRow = UnityEngine.Random.Range(0, 8); //隨機指定一列
                randColumn = UnityEngine.Random.Range(0, 5); //隨機指定一欄 
                //print("I'll get you next round!!");
            } while ((m.board[randRow, randColumn, 0] != "n") || Mathf.Abs(position[0] - randRow) + Mathf.Abs(position[1] - randColumn) > range);
            m.board[position[0], position[1], 0] = "n"; //將原始位置改為n
            position[0] = randRow;
            position[1] = randColumn;
            m.board[position[0], position[1], 0] = "e" + e_tag.ToString(); //移動敵人
        }*/
        //m.CheckAggr();
        //yield return new WaitForSeconds(1.2f);
        aggr.Clear();
        //yield return new WaitForSeconds(1.2f);

        //defence
        /*if (GetMaxCard(hand, "defence") != -1) {
            //m.cardBoard[position[0], position[1], 1] = new Card(hand[GetMaxCard(hand, "defence")]);
            //m.board.SetPass(position[0], position[1], hand[GetMaxCard(hand, "defence")]);
            pass = new Card(hand[GetMaxCard(hand, "defence")]);
            hand[GetMaxCard(hand, "defence")].Clear();
        }*/

        //yield return new WaitForSeconds(1.2f);

        //draw
        for (int i = 0; i < 3; i++) {
            if (hand[i].ID == 0) {
                hand[i] = deck.Draw();
                if (hand[i].ID == 0) {
                    ResetDeck();
                    hand[i] = deck.Draw();
                }
            }
        }

        //yield return new WaitForSeconds(0.5f);
        //yield return new WaitForSeconds(0.5f);
        //yield return new WaitForSeconds(2.0f);

        //end turn
        print(label + "done");
        //m.enemyDone(label);
        m.enemyController(label, "end");
        //m.EndTurn(type);

        yield break;
    }

    /*private void PlayerPosition () {*/
        /*for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 5; j++) {
                if (m.board[i, j, 0] == "p1") {
                    Player1Position[0] = i;
                    Player1Position[1] = j;
                }
            }
        }
        for (int o = 0; o < 8; o++) {
            for (int p = 0; p < 5; p++) {
                if (m.board[o, p, 0] == "p2") {
                    Player2Position[0] = o;
                    Player2Position[1] = p;
                }
            }
        }*/
        /*for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 5; j++) {
                if (m.board.GetCharacterLabel(i, j) == "p1") {
                    Player1Position[0] = i;
                    Player1Position[1] = j;
                }
            }
        }
        for (int o = 0; o < 8; o++) {
            for (int p = 0; p < 5; p++) {
                if (m.board.GetCharacterLabel(o, p) == "p2") {
                    Player2Position[0] = o;
                    Player2Position[1] = p;
                }
            }
        }
    }*/
    
    /*private void AssignAttackBlockPlayer1 () {
        Array.Copy(Player1Position, player1_up, 2);
        Array.Copy(Player1Position, player1_down, 2);
        Array.Copy(Player1Position, player1_left, 2);
        Array.Copy(Player1Position, player1_right, 2);
        player1_up[0]--;
        player1_down[0]++;
        player1_left[1]--;
        player1_right[1]++;
    }

    private void AssignAttackBlockPlayer2 () {
        Array.Copy(Player2Position, player2_up, 2);
        Array.Copy(Player2Position, player2_down, 2);
        Array.Copy(Player2Position, player2_left, 2);
        Array.Copy(Player2Position, player2_right, 2);
        player2_up[0]--;
        player2_down[0]++;
        player2_left[1]--;
        player2_right[1]++;
    }*/

    private bool IfCanGoToPlayer (int[] PlayersAttackBlock, int[] PlayersPosition, int[] MonstersPosition) {  //如果玩家上下左右在棋盤裡,且在怪獸打擊範圍內
        if (PlayersAttackBlock[0] <= 7 && PlayersAttackBlock[0] >= 0 && Mathf.Abs(PlayersAttackBlock[0] - MonstersPosition[0]) + Mathf.Abs(PlayersAttackBlock[1] - MonstersPosition[1]) <= range && PlayersAttackBlock[1] >= 0 && PlayersAttackBlock[1] <= 4) { return true; }
        else { return false; }
    }

    /*static string ActDirection (int[] monstersposition, int[] playersposition) {
        if (playersposition[0] - monstersposition[0] == -1 && playersposition[1] - monstersposition[1] == 0)
            return "up";
        else if (playersposition[0] - monstersposition[0] == 1 && playersposition[1] - monstersposition[1] == 0)
            return "down";
        else if (playersposition[0] - monstersposition[0] == 0 && playersposition[1] - monstersposition[1] == 1)
            return "right";
        else if (playersposition[0] - monstersposition[0] == 0 && playersposition[1] - monstersposition[1] == -1)
            return "left";
        else
            return "n";
    }*/

    private void PrintthePosition (int[] passedposition) {
        foreach (int i in passedposition) {
            print(i);
        }
    }

    public int GetHandNumber () {
        int number = 0;
        foreach (Card card in hand) {
            if (card.ID != 0) {
                number++;
            }
        }
        return number;
    }

    private int GetMaxCard (Card[] card, string action) {
        Card tempMaxCard = new Card();
        int number = 0;
        for (int i = 0; i < card.Length; i++) {
            if (card[i].Action == action && card[i].Value >= tempMaxCard.Value) {
                tempMaxCard = new Card(card[i]);
            }
        }
        for (number = 0; number < card.Length; number++) {
            if (card[number].ID == tempMaxCard.ID) {
                return number;
            }
        }
        return -1;
    }

    /*private void ResetDeck () {
        deck = new Deck(fold);
        deck.Shuffle();
        fold.Clear();
    }*/

}
