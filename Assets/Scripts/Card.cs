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
        else if (id / 100 == 2) {
            action = "defence";
        }
        else {
            action = "";
        }

        if ((id % 100 - id % 10) / 10 == 1) {
            type = "smash";
        }
        else if ((id % 100 - id % 10) / 10 == 2) {
            type = "slice";
        }
        else {
            type = "";
        }

        value = id % 10;
    }

    public Card (Card card) {
        id = card.id;
        value = card.value;
        action = card.action;
        type = card.type;
    }

    public void Clear () {
        id = 0;
        value = 0;
        action = "";
        type = "";
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
