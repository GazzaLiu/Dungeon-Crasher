using UnityEngine;
using System.Collections;

public class Stage1_Manager : MonoBehaviour {

    //宣告場地大小
    const int maxWidth = 5;
    const int maxHeight = 7;

    //宣告玩家與敵人位置
    public int[] player1 = new int[2] { 6, 1 };
    public int[] player2 = new int[2] { 6, 3 };
    public int[] enemy1 = new int[2] { 0, 1 };
    public int[] enemy2 = new int[2] { 6, 3 };

    //宣告移動起始位置與結束位置
    int[] startPosition = new int[2];
    int[] endPosition = new int[2];

    bool hold = false; //宣告是否抓住玩家
    int turn = 0; //宣告回合

    //宣告隨機變數
    int randRow = 0;
    int randColumn = 0;

    //宣告棋盤：1：玩家與敵人位置、2：動作、3：道具、4：地形
    string[,,] board = new string[maxHeight, maxWidth, 4]; //宣告棋盤

    PositionController pc; //宣告得到另一文件

    void Start () {

        pc = this.GetComponent<PositionController>(); //得到另一文件當前選取格位置

        //初始化棋盤
        for (int k = 0; k < 4; k++) {
            for (int i = 0; i < maxHeight; i++) {
                for (int j = 0; j < maxWidth; j++) {
                        board[i, j, k] = "n";
                }
            }
        }

        //初始化玩家與敵人位置
        board[player1[0], player1[1], 1] = "p1";
        board[player2[0], player2[1], 1] = "p2";
        board[enemy1[0], enemy1[1], 1] = "e1";
        board[enemy2[0], enemy2[1], 1] = "e2";

        //顯示棋盤
        Display(board);
    }

	void Update () {

        //Player1的回合
        if ((turn % 4 == 0) && (Input.GetKeyDown(KeyCode.Space))) { //按下空白鍵來操作玩家
            if(hold == false && board[pc.position[0], pc.position[1], 1] == "p1") { //若格子有玩家則將其抓起
                startPosition[0] = pc.position[0]; //記下玩家初始位置
                startPosition[1] = pc.position[1];
                hold = true; //抓起
                Display(board);
            }
            if (hold == true && board[pc.position[0], pc.position[1], 1] == "n") { //若格子上無人則將其放下
                endPosition[0] = pc.position[0]; //記下玩家結束位置
                endPosition[1] = pc.position[1];
                board[startPosition[0], startPosition[1], 1] = "n"; //將原始位置改為n
                board[pc.position[0], pc.position[1], 1] = "p1"; //移動玩家
                hold = false; //放下
                Display(board);
                turn++;
            }
        }

        //Player2的回合
        if ((turn % 4 == 1) && (Input.GetKeyDown(KeyCode.Space))) { //按下空白鍵來操作玩家
            if (hold == false && board[pc.position[0], pc.position[1], 1] == "p2") { //若格子有玩家則將其抓起
                startPosition[0] = pc.position[0]; //記下玩家初始位置
                startPosition[1] = pc.position[1];
                hold = true; //抓起
                Display(board);
            }
            if (hold == true && board[pc.position[0], pc.position[1], 1] == "n") { //若格子上無人則將其放下
                endPosition[0] = pc.position[0]; //記下玩家結束位置
                endPosition[1] = pc.position[1];
                board[startPosition[0], startPosition[1], 1] = "n"; //將原始位置改為n
                board[pc.position[0], pc.position[1], 1] = "p2"; //移動玩家
                hold = false; //放下
                Display(board);
                turn++;
            }
        }

        //Enemy1的回合
        if (turn % 4 == 2) {
            do {
                randRow = Random.Range(0, maxHeight); //隨機指定一列
                randColumn = Random.Range(0, maxWidth); //隨機指定一欄
            } while (board[randRow, randColumn, 1] != "n");
            board[enemy1[0], enemy1[1], 1] = "n"; //將原始位置改為n
            enemy1[0] = randRow;
            enemy1[1] = randColumn;
            board[enemy1[0], enemy1[1], 1] = "e1"; //移動敵人
            Display(board);
            turn++;
        }

        //Enemy2的回合
        if (turn % 4 == 3) {
            do {
                randRow = Random.Range(0, maxHeight); //隨機指定一列
                randColumn = Random.Range(0, maxWidth); //隨機指定一欄
            } while (board[randRow, randColumn, 1] != "n");
            board[enemy2[0], enemy2[1], 1] = "n"; //將原始位置改為n
            enemy2[0] = randRow;
            enemy2[1] = randColumn;
            board[enemy2[0], enemy2[1], 1] = "e2"; //移動敵人
            Display(board);
            turn++;
        }

    }

    //顯示棋盤
    static void Display (string[,,] board) {
        string display = "";
        for (int i = 0; i < maxHeight; i++) {
            for (int j = 0; j < maxWidth; j++) {
                display += board[i, j, 1] + " ";
            }
            display += "\n";
        }
        print(display);
    }
}
