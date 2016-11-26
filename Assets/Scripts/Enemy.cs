using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int e_tag = 0;
    public int HP;

    public int[] position = new int[2] { 0, 0 };

    public GameObject manager;
    public Stage1_Manager m;

    //宣告隨機變數
    int randRow = 0;
    int randColumn = 0;

    //宣告移動起始位置與結束位置
    int[] startPosition = new int[2];
    int[] endPosition = new int[2];

    void Start () {
        m = manager.GetComponent<Stage1_Manager>();
    }

    void Update () {
        if (m.turn % 4 == (e_tag + 1)) {
            do {
                randRow = Random.Range(0, 7); //隨機指定一列
                randColumn = Random.Range(0, 5); //隨機指定一欄
            } while (m.board[randRow, randColumn, 1] != "n");
            m.board[position[0], position[1], 1] = "n"; //將原始位置改為n
            position[0] = randRow;
            position[1] = randColumn;
            m.board[position[0], position[1], 1] = "e" + e_tag.ToString(); //移動敵人
            m.turn++;
        }
    }
}
