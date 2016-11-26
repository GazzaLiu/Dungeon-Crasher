using UnityEngine;
using System.Collections;

public class Stage1_Manager : MonoBehaviour {

    const int maxWidth = 5; //宣告場地寬
    const int maxHeight = 7; //宣告場地高

    public int turn = 0; //宣告回合

    //宣告選取位置
    public int[] position = new int[2] { 0, 0 };

    //宣告棋盤：1：玩家與敵人位置、2：動作、3：道具、4：地形
    public string[,,] board = new string[maxHeight, maxWidth, 4]; //宣告棋盤

    public GameObject player1;
    public GameObject player2;
    public GameObject enemy1;
    public GameObject enemy2;

    Player[] p = new Player[2];
    Enemy[] e = new Enemy[2];

    void Start () {

        p[0] = player1.GetComponent<Player>();
        p[1] = player2.GetComponent<Player>();
        e[0] = enemy1.GetComponent<Enemy>();
        e[1] = enemy2.GetComponent<Enemy>();

        //初始化棋盤
        for (int k = 0; k < 4; k++) {
            for (int i = 0; i < maxHeight; i++) {
                for (int j = 0; j < maxWidth; j++) {
                        board[i, j, k] = "n";
                }
            }
        }

        //初始化玩家與敵人位置
        board[p[0].position[0], p[0].position[1], 1] = "p" + p[0].p_tag.ToString();
        board[p[1].position[0], p[1].position[1], 1] = "p" + p[1].p_tag.ToString();
        board[e[0].position[0], e[0].position[1], 1] = "e" + e[0].e_tag.ToString();
        board[e[1].position[0], e[1].position[1], 1] = "e" + e[1].e_tag.ToString();

        print(position[0].ToString() + position[1].ToString()); //顯示位置
    }

	void Update () {

        if (Input.GetKeyDown(KeyCode.RightArrow)) { //按下方向鍵右
            position[1]++;
            print(position[0].ToString() + position[1].ToString());
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { //按下方向鍵左
            position[1]--;
            print(position[0].ToString() + position[1].ToString());
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) { //按下方向鍵上
            position[0]--;
            print(position[0].ToString() + position[1].ToString());
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { //按下方向鍵下
            position[0]++;
            print(position[0].ToString() + position[1].ToString());
        }
        if (Input.GetKeyDown(KeyCode.D)) { //顯示棋盤
            Display(board, 1);
        }
        if (Input.GetKeyDown(KeyCode.F)) { //顯示棋盤
            Display(board, 2);
        }

    }

    public void CheckAggr () { //很冗
        for (int i = 0; i < maxHeight; i++) {
            for (int j = 0; j < maxWidth; j++) {
                if(board[i, j, 2].IndexOf("attack") >= 0) {
                    if(board[i, j, 1] == "p1") {
                        if (board[i - 1, j, 1] == "e1")
                            e[0].HP -= p[0].ATK;
                        else
                            e[1].HP -= p[0].ATK;
                    }
                    else {
                        if (board[i - 1, j, 1] == "e1")
                            e[0].HP -= p[1].ATK;
                        else
                            e[1].HP -= p[1].ATK;
                    }
                    board[i, j, 2] = "n";
                }
            }
        }
    }

    //顯示棋盤
    static void Display (string[,,] board, int floor) {
        string display = "";
        for (int i = 0; i < maxHeight; i++) {
            for (int j = 0; j < maxWidth; j++) {
                display += board[i, j, floor] + " ";
            }
            display += "\n";
        }
        print(display);
    }
}
