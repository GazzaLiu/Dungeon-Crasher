using UnityEngine;
using System.Collections;

public class Stage1_Manager : Entity {

    const int maxWidth = 5; //宣告場地寬
    const int maxHeight = 8; //宣告場地高
    const int layer = 2; //宣告場地層

    public int turn = 0; //宣告回合

    //宣告選取位置
    public int[] position = new int[2] { 0, 0 };

    //宣告棋盤：1：玩家與敵人位置、2：動作
    public string[,,] board = new string[maxHeight, maxWidth, layer]; //宣告棋盤

    public GameObject[] character = new GameObject[4];

    Player[] p = new Player[2];
    Enemy[] e = new Enemy[2];

    void Start () {

        p[0] = character[0].GetComponent<Player>();
        p[1] = character[1].GetComponent<Player>();
        e[0] = character[2].GetComponent<Enemy>();
        e[1] = character[3].GetComponent<Enemy>();

        //初始化棋盤
        for (int k = 0; k < layer; k++) {
            for (int i = 0; i < maxHeight; i++) {
                for (int j = 0; j < maxWidth; j++) {
                        board[i, j, k] = "n";
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
            Display(board, 0);
            Display(board, 1);
        }

    }

    public void CheckAggr () {
        Display(board, 1);
        for (int i = 0; i < maxHeight; i++) {
            for (int j = 0; j < maxWidth; j++) {
                if (board[i, j, 1].IndexOf("attack") >= 0 && board[i, j, 1].IndexOf("up") >= 0) {
                    if (board[i, j, 0].IndexOf("p") >= 0 && board[i - 1, j, 0].IndexOf("e") >= 0) {
                        e[int.Parse(board[i - 1, j, 0][1] + "") - 1].HP -= p[int.Parse(board[i, j, 0][1] + "") - 1].ATK;
                    }
                    else if (board[i, j, 0].IndexOf("e") >= 0 && board[i - 1, j, 0].IndexOf("p") >= 0) {
                        p[int.Parse(board[i - 1, j, 0][1] + "") - 1].HP -= e[int.Parse(board[i, j, 0][1] + "") - 1].ATK;
                    }
                    board[i, j, 1] = "n";
                }
                else if (board[i, j, 1].IndexOf("attack") >= 0 && board[i, j, 1].IndexOf("down") >= 0) {
                    if (board[i, j, 0].IndexOf("p") >= 0 && board[i + 1, j, 0].IndexOf("e") >= 0) {
                        e[int.Parse(board[i + 1, j, 0][1] + "") - 1].HP -= p[int.Parse(board[i, j, 0][1] + "") - 1].ATK;
                    }
                    else if (board[i, j, 0].IndexOf("e") >= 0 && board[i + 1, j, 0].IndexOf("p") >= 0) {
                        p[int.Parse(board[i + 1, j, 0][1] + "") - 1].HP -= e[int.Parse(board[i, j, 0][1] + "") - 1].ATK;
                    }
                    board[i, j, 1] = "n";
                }
                else if (board[i, j, 1].IndexOf("attack") >= 0 && board[i, j, 1].IndexOf("left") >= 0) {
                    if (board[i, j, 0].IndexOf("p") >= 0 && board[i, j - 1, 0].IndexOf("e") >= 0) {
                        e[int.Parse(board[i, j - 1, 0][1] + "") - 1].HP -= p[int.Parse(board[i, j, 0][1] + "") - 1].ATK;
                    }
                    else if (board[i, j, 0].IndexOf("e") >= 0 && board[i, j - 1, 0].IndexOf("p") >= 0) {
                        p[int.Parse(board[i, j - 1, 0][1] + "") - 1].HP -= e[int.Parse(board[i, j, 0][1] + "") - 1].ATK;
                    }
                    board[i, j, 1] = "n";
                }
                else if (board[i, j, 1].IndexOf("attack") >= 0 && board[i, j, 1].IndexOf("right") >= 0) {
                    if (board[i, j, 0].IndexOf("p") >= 0 && board[i, j + 1, 0].IndexOf("e") >= 0) {
                        e[int.Parse(board[i, j + 1, 0][1] + "") - 1].HP -= p[int.Parse(board[i, j, 0][1] + "") - 1].ATK;
                    }
                    else if (board[i, j, 0].IndexOf("e") >= 0 && board[i, j + 1, 0].IndexOf("p") >= 0) {
                        p[int.Parse(board[i, j + 1, 0][1] + "") - 1].HP -= e[int.Parse(board[i, j, 0][1] + "") - 1].ATK;
                    }
                    board[i, j, 1] = "n";
                }
            }
        }
    }

}
