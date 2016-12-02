using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int e_tag = 0;
    public int HP = 0;
    public int ATK = 0;
    public int range = 2;

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

        this.transform.position = new Vector3(Horizontalposition(position[1]), Verticallposition(position[0]), 0);

        if (HP <= 0) {
            m.board[position[0], position[1], 1] = "n";
            m.board[position[0], position[1], 3] = "dead";
            Destroy(this.gameObject);
        }

        if (m.turn % 4 == (e_tag + 1)) {
            do {
                randRow = Random.Range(0, 7); //隨機指定一列
                randColumn = Random.Range(0, 5); //隨機指定一欄
            } while (m.board[randRow, randColumn, 1] != "n" && Mathf.Abs(randRow - position[0]) - Mathf.Abs(randColumn - position[1]) > range);
            m.board[position[0], position[1], 1] = "n"; //將原始位置改為n
            position[0] = randRow;
            position[1] = randColumn;
            m.board[position[0], position[1], 1] = "e" + e_tag.ToString(); //移動敵人
            m.board[position[0], position[1], 2] = "attack_up";
            m.CheckAggr();
            m.turn++;
        }
    }

    static float Horizontalposition (int i) {
        float x = -58f / 30f + (2f * i) * (203f / 420f);
        return x;
    }

    static float Verticallposition (int j) {
        float y = 106.5f / 30f - (2f * j) * (116f / 240f);
        return y;
    }

}
