using UnityEngine;
using System.Collections;

public class Select : MonoBehaviour {

    public GameObject manager;
    public Stage1_Manager m;

    void Start () {
        m = manager.GetComponent<Stage1_Manager>();
    }

    void Update () {
        if(Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.DownArrow)) {
            //this.transform.position = new Vector3(0) m.position[0] m.position[1] 座標系轉換
        }
    }
}
