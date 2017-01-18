using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Stage1_Manager : Entity {

    const int MAX_WIDTH = 5;
    const int MAX_HEIGHT = 8;
    const int BOARD_LAYER = 2;
    const int CARD_BOARD_LAYER = 2;
    const int MAX_PLAYER = 2;
    const int MAX_ENEMY = 2;

    public int enemyTurn = 0;

    public int[] position = new int[2] { 0, 0 };

    public string[,,] board = new string[MAX_HEIGHT, MAX_WIDTH, BOARD_LAYER];
    public Card[,,] cardBoard = new Card[MAX_HEIGHT, MAX_WIDTH, CARD_BOARD_LAYER];

    public GameObject[] character = new GameObject[MAX_PLAYER + MAX_ENEMY];

    private bool playerTurn = true;

    public string[] liveList = new string[MAX_PLAYER + MAX_ENEMY];

    private Player[] p = new Player[MAX_PLAYER];
    private Enemy[] e = new Enemy[MAX_ENEMY];

    void Start () {

        for (int i = 0; i < MAX_PLAYER; i++) {
            p[i] = character[i].GetComponent<Player>();
            e[i] = character[i + MAX_PLAYER].GetComponent<Enemy>();
            liveList[i] = "p" + p[i].p_tag.ToString();
            liveList[i + MAX_PLAYER] = "e" + e[i].e_tag.ToString();
        }

        for (int i = 0; i < MAX_HEIGHT; i++) {
            for (int j = 0; j < MAX_WIDTH; j++) {
                for (int k = 0; k < BOARD_LAYER; k++) {
                    board[i, j, k] = "n";
                }
                for (int k = 0; k < CARD_BOARD_LAYER; k++) {
                    cardBoard[i, j, k] = new Card(); 
                }
            }
        }

        board[p[0].position[0], p[0].position[1], 0] = "p" + p[0].p_tag.ToString();
        board[p[1].position[0], p[1].position[1], 0] = "p" + p[1].p_tag.ToString();
        board[e[0].position[0], e[0].position[1], 0] = "e" + e[0].e_tag.ToString();
        board[e[1].position[0], e[1].position[1], 0] = "e" + e[1].e_tag.ToString();

    }

	void Update () {

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            position[0]--;
            position[0] = Mathf.Clamp(position[0], 0, MAX_HEIGHT - 1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            position[0]++;
            position[0] = Mathf.Clamp(position[0], 0, MAX_HEIGHT - 1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            position[1]--;
            position[1] = Mathf.Clamp(position[1], 0, MAX_WIDTH - 1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            position[1]++;
            position[1] = Mathf.Clamp(position[1], 0, MAX_WIDTH - 1);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            print(position[0].ToString() + position[1].ToString());
            DisplayBoard(board, 0);
            DisplayCardBoard(cardBoard, 1);
        }

        TurnController();

    }

    public void ActivatePlayer (string name) {
        foreach (Player player in p) {
            if (name == "p" + player.p_tag.ToString()) {
                player.isActing = true;
            }
            else {
                player.isActing = false;
            }
        }
    }

    public void TurnController () {
        if (playerTurn) {
            foreach (Player player in p) {
                player.isTurn = true;
            }
            foreach (Enemy enemy in e) {
                enemy.isTurn = false;
            }
        }
        else {
            foreach (Player player in p) {
                player.isTurn = false;
            }
            foreach (Enemy enemy in e) {
                enemy.isTurn = true;
            }
        }
    }

    public void EndTurn (string name) {
        if (name.IndexOf("p") >= 0) {
            playerTurn = false;
            foreach (string str in liveList) {
                if (str.IndexOf("e") >= 0 && str.IndexOf("dead") >= 0) {
                    enemyTurn++;
                }
            }
        }
        else {
            if (name == liveList[liveList.Length - 1]) {
                playerTurn = true;
            }
            enemyTurn += 2;
        }
    }

    public void DeathEvent (int[] position, string name) {
        int count = 0;
        for (int i = 0; i < liveList.Length; i++) {
            if (name == liveList[i]) {
                liveList[i] = name + "_dead";
            }
        }
        board[position[0], position[1], 0] = "n";
        foreach (string str in liveList) {
            if (str.IndexOf("p") >= 0) {
                if (str.IndexOf("dead") >= 0) {
                    count++;
                }
                else {
                    count = 0;
                }
                if (count >= MAX_PLAYER) {
                    SceneManager.LoadScene("EnemyWin");
                }
            }
            else {
                if (str.IndexOf("dead") >= 0) {
                    count++;
                }
                else {
                    count = 0;
                }
                if (count >= MAX_ENEMY) {
                    SceneManager.LoadScene("PlayerWin");
                }
            }
        }
    }

    public int ComputeHP (int HP_2, Card aggr, Card pass) {
        if (aggr.Type == pass.Type)
            return HP_2 - Mathf.Max((aggr.Value - pass.Value), 0);
        else
            return HP_2 - aggr.Value;
    }

    public void CheckAggr () {

        DisplayBoard(board, 1);
        DisplayCardBoard(cardBoard, 0);

        for (int i = 0; i < MAX_HEIGHT; i++) {
            for (int j = 0; j < MAX_WIDTH; j++) {

                if (board[i, j, 1] == "up") {
                    if (board[i, j, 0].IndexOf("p") >= 0 && board[i - 1, j, 0].IndexOf("e") >= 0) {
                        e[int.Parse(board[i - 1, j, 0][1] + "") - 1].HP = ComputeHP(e[int.Parse(board[i - 1, j, 0][1] + "") - 1].HP, cardBoard[i, j, 0], cardBoard[i - 1, j, 1]);
                    }
                    else if (board[i, j, 0].IndexOf("e") >= 0 && board[i - 1, j, 0].IndexOf("p") >= 0) {
                        p[int.Parse(board[i - 1, j, 0][1] + "") - 1].HP = ComputeHP(p[int.Parse(board[i - 1, j, 0][1] + "") - 1].HP, cardBoard[i, j, 0], cardBoard[i - 1, j, 1]);
                    }
                    board[i, j, 1] = "n";
                    cardBoard[i, j, 0].Clear();
                }

                else if (board[i, j, 1] == "down") {
                    if (board[i, j, 0].IndexOf("p") >= 0 && board[i + 1, j, 0].IndexOf("e") >= 0) {
                        e[int.Parse(board[i + 1, j, 0][1] + "") - 1].HP = ComputeHP(e[int.Parse(board[i + 1, j, 0][1] + "") - 1].HP, cardBoard[i, j, 0], cardBoard[i + 1, j, 1]);
                    }
                    else if (board[i, j, 0].IndexOf("e") >= 0 && board[i + 1, j, 0].IndexOf("p") >= 0) {
                        p[int.Parse(board[i + 1, j, 0][1] + "") - 1].HP = ComputeHP(p[int.Parse(board[i + 1, j, 0][1] + "") - 1].HP, cardBoard[i, j, 0], cardBoard[i + 1, j, 1]);
                    }
                    board[i, j, 1] = "n";
                    cardBoard[i, j, 0].Clear();
                }

                else if (board[i, j, 1] == "left") {
                    if (board[i, j, 0].IndexOf("p") >= 0 && board[i, j - 1, 0].IndexOf("e") >= 0) {
                        e[int.Parse(board[i, j - 1, 0][1] + "") - 1].HP = ComputeHP(e[int.Parse(board[i, j - 1, 0][1] + "") - 1].HP, cardBoard[i, j, 0], cardBoard[i, j - 1, 1]);
                    }
                    else if (board[i, j, 0].IndexOf("e") >= 0 && board[i, j - 1, 0].IndexOf("p") >= 0) {
                        p[int.Parse(board[i, j - 1, 0][1] + "") - 1].HP = ComputeHP(p[int.Parse(board[i, j - 1, 0][1] + "") - 1].HP, cardBoard[i, j, 0], cardBoard[i, j - 1, 1]);
                    }
                    board[i, j, 1] = "n";
                    cardBoard[i, j, 0].Clear();
                }

                else if (board[i, j, 1] == "right") {
                    if (board[i, j, 0].IndexOf("p") >= 0 && board[i, j + 1, 0].IndexOf("e") >= 0) {
                        e[int.Parse(board[i, j + 1, 0][1] + "") - 1].HP = ComputeHP(e[int.Parse(board[i, j + 1, 0][1] + "") - 1].HP, cardBoard[i, j, 0], cardBoard[i, j + 1, 1]);
                    }
                    else if (board[i, j, 0].IndexOf("e") >= 0 && board[i, j + 1, 0].IndexOf("p") >= 0) {
                        p[int.Parse(board[i, j + 1, 0][1] + "") - 1].HP = ComputeHP(p[int.Parse(board[i, j + 1, 0][1] + "") - 1].HP, cardBoard[i, j, 0], cardBoard[i, j + 1, 1]);
                    }
                    board[i, j, 1] = "n";
                    cardBoard[i, j, 0].Clear();
                }

            }
        }
    }

}
