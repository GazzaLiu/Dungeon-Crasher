using UnityEngine;
using System.Collections;

public class Stage1_Manager : Entity {

    const int maxWidth = 5; //宣告場地寬
    const int maxHeight = 8; //宣告場地高
    const int boardLayer = 2;
    const int cardBoardLayer = 2;

    public int turn = 0; //宣告回合

    public int[] position = new int[2] { 0, 0 }; //宣告選取位置

    public string[,,] board = new string[maxHeight, maxWidth, boardLayer];
    public Card[,,] cardBoard = new Card[maxHeight, maxWidth, cardBoardLayer];

    public GameObject[] character = new GameObject[4];

    Player[] p = new Player[2];
    Enemy[] e = new Enemy[2];

    void Start () {

        p[0] = character[0].GetComponent<Player>();
        p[1] = character[1].GetComponent<Player>();
        e[0] = character[2].GetComponent<Enemy>();
        e[1] = character[3].GetComponent<Enemy>();

        //初始化棋盤
        for (int i = 0; i < maxHeight; i++) {
            for (int j = 0; j < maxWidth; j++) {
                for (int k = 0; k < boardLayer; k++) {
                    board[i, j, k] = "n";
                }
                for (int k = 0; k < cardBoardLayer; k++) {
                    cardBoard[i, j, k] = new Card(); 
                }
            }
        }

        //初始化玩家與敵人位置
        board[p[0].position[0], p[0].position[1], 0] = "p" + p[0].p_tag.ToString();
        board[p[1].position[0], p[1].position[1], 0] = "p" + p[1].p_tag.ToString();
        board[e[0].position[0], e[0].position[1], 0] = "e" + e[0].e_tag.ToString();
        board[e[1].position[0], e[1].position[1], 0] = "e" + e[1].e_tag.ToString();

    }

	void Update () {

        if (Input.GetKeyDown(KeyCode.UpArrow)) { //按下方向鍵上
            position[0]--;
            position[0] = Mathf.Clamp(position[0], 0, maxHeight - 1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { //按下方向鍵下
            position[0]++;
            position[0] = Mathf.Clamp(position[0], 0, maxHeight - 1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { //按下方向鍵左
            position[1]--;
            position[1] = Mathf.Clamp(position[1], 0, maxWidth - 1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { //按下方向鍵右
            position[1]++;
            position[1] = Mathf.Clamp(position[1], 0, maxWidth - 1);
        }
        if (Input.GetKeyDown(KeyCode.D)) { //顯示棋盤與位置
            print(position[0].ToString() + position[1].ToString());
            DisplayBoard(board, 0);
            DisplayCardBoard(cardBoard, 1);
        }

        ActivatePlayer();

    }

    public void ActivatePlayer () {
        foreach (Player player in p) {
            if (turn % 4 == player.turnNumber) {
                player.isTurn = true;
            }
            else {
                player.isTurn = false;
            }
        }
    }

    public void EndTurn () {
        turn++;
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

        for (int i = 0; i < maxHeight; i++) {
            for (int j = 0; j < maxWidth; j++) {

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
