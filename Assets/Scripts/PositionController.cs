using UnityEngine;
using System.Collections;

public class PositionController : MonoBehaviour {

    public int[] position = new int[2] { 0, 0 };

	void Start () {
        print(position[0].ToString() + position[1].ToString()); //顯示當前選取格位置
    }

	void Update () { //主要為控制當前選取格位置
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
    }
}
