using UnityEngine;
using System.Collections;

public class Select : Entity {

    public GameObject manager;
    public Stage1_Manager m;

    void Start () {
        m = manager.GetComponent<Stage1_Manager>();
    }

    void Update () {
        this.transform.position = new Vector3(Horizontalposition(m.position[1]), Verticallposition(m.position[0]), 0);
    }

}
