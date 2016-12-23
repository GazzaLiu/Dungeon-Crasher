using UnityEngine;
using System.Collections;

public class Card : Entity {

    private int id;
    private int value;
    private string action;
    private string type;

    public Card () {
        id = 0;
        value = 0;
        action = "";
        type = "";
    }

    public Card (int id) {
        this.id = id;

        if (id / 100 == 1) {
            action = "attack";
        }
        else {
            action = "defence";
        }

        if ((id % 100 - id % 10) / 10 == 1) {
            type = "smash";
        }
        else {
            type = "slice";
        }

        value = id % 10;
    }

    public int ID {
        get { return id; }
    }

    public int Value {
        get { return value; }
    }

    public string Action {
        get { return action; }
    }

    public string Type {
        get { return type; }
    }

}
