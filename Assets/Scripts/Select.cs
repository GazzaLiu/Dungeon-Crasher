using UnityEngine;
using System.Collections;

public class Select : MonoBehaviour {

    public GameObject manager;
    public Stage1_Manager m;

    void Start () {
        m = manager.GetComponent<Stage1_Manager>();
    }

    void Update () {
        this.transform.position = new Vector3(Horizontalposition(m.position[1]), Verticallposition(m.position[0]), 0);
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
