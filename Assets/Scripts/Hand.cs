using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

    public int number = 0;

    public GameObject player;

    public Sprite[] sprite = new Sprite[10];

    private int index = 0;

    private Player p;

    void Start () {
        p = player.GetComponent<Player>();
    }

    void Update () {
        switch (p.hand[number].ID) {
            case 111:
                this.GetComponent<SpriteRenderer>().sprite = sprite[0];
                break;
            case 112:
                this.GetComponent<SpriteRenderer>().sprite = sprite[1];
                break;
            case 113:
                this.GetComponent<SpriteRenderer>().sprite = sprite[2];
                break;
            case 121:
                this.GetComponent<SpriteRenderer>().sprite = sprite[3];
                break;
            case 122:
                this.GetComponent<SpriteRenderer>().sprite = sprite[4];
                break;
            case 123:
                this.GetComponent<SpriteRenderer>().sprite = sprite[5];
                break;
            case 211:
                this.GetComponent<SpriteRenderer>().sprite = sprite[6];
                break;
            case 212:
                this.GetComponent<SpriteRenderer>().sprite = sprite[7];
                break;
            case 221:
                this.GetComponent<SpriteRenderer>().sprite = sprite[8];
                break;
            case 222:
                this.GetComponent<SpriteRenderer>().sprite = sprite[9];
                break;
            default:
                break;
        }


    }

}
