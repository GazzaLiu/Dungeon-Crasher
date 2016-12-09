using UnityEngine;
using System.Collections;

public class Player : Entity {

    public int p_tag = 0;
    public int step = 1;
    public int HP = 0;
    public int ATK = 0;
    public int range = 2;

    public int[] position = new int[2] { 0, 0 };

    public GameObject manager;
    public Stage1_Manager m;

    bool hold = false; //宣告是否抓起

    //宣告移動起始位置與結束位置
    int[] startPosition = new int[2];
    int[] endPosition = new int[2];

    AnimationController ac;
    SpriteRenderer sr;

    void Start () {
        m = manager.GetComponent<Stage1_Manager>();
        ac = this.transform.GetChild(0).GetComponent<AnimationController>();
        sr = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update () {

        this.transform.position = new Vector3(Horizontalposition(position[1]), Verticallposition(position[0]), 0);
        sr.sortingOrder = position[0];

        if (HP <= 0) {
            m.board[position[0], position[1], 0] = "n";
            Destroy(this.gameObject);
        }

        //Player的回合
        if (m.turn % 4 == (p_tag - 1)) {
            if (step == 1 && Input.GetKeyDown(KeyCode.Space)) { //按下空白鍵來操作玩家
                if (hold == false && m.board[m.position[0], m.position[1], 0] == ("p" + p_tag.ToString())) { //若格子有玩家則將其抓起
                    startPosition[0] = m.position[0]; //記下玩家初始位置
                    startPosition[1] = m.position[1];
                    hold = true; //抓起
                }
                if (hold == true && m.board[m.position[0], m.position[1], 0] == "n" && Mathf.Abs(m.position[0] - startPosition[0]) - Mathf.Abs(m.position[1] - startPosition[1]) <= range) { //若格子上無人則將其放下
                    endPosition[0] = m.position[0]; //記下玩家結束位置
                    endPosition[1] = m.position[1];
                    hold = false; //放下
                    m.board[startPosition[0], startPosition[1], 0] = "n"; //將原始位置改為n
                    m.board[endPosition[0], endPosition[1], 0] = "p" + p_tag.ToString();
                    position[0] = endPosition[0]; //移動玩家
                    position[1] = endPosition[1];
                    step++;
                }
            }

            if (step == 2 && Input.GetKeyDown(KeyCode.Z)) {
                m.board[position[0], position[1], 1] = ActDirection(position, m.position);
                m.CheckAggr();
                //ac.Attack();
                step = 1;
                m.turn++;
            }
        }

    }

    static string ActDirection (int[] position, int[] selectPosition) {
        if (selectPosition[0] - position[0] == -1 && selectPosition[1] - position[1] == 0)
            return "attack_up";
        else if (selectPosition[0] - position[0] == 1 && selectPosition[1] - position[1] == 0)
            return "attack_down";
        else if (selectPosition[0] - position[0] == 0 && selectPosition[1] - position[1] == 1)
            return "attack_right";
        else if (selectPosition[0] - position[0] == 0 && selectPosition[1] - position[1] == -1)
            return "attack_left";
        else
            return "n";
    }

}
