using UnityEngine;
using System.Collections;

public class Card {

    //variable
    private int id;
    private int cost;
    private int value;
    private int range;
    //private int targetRow;
    //private int targetColumn;
    private string action;
    private string type;

    //default constructor
    public Card () {
        id = 0;
        cost = 0;
        value = 0;
        range = 0;
        //targetRow = -1;
        //targetColumn = -1;
        action = "";
        type = "";
    }

    //regular constructor
    public Card (int id) {
        this.id = id;
        cost = id % 10;
        value = id % 10;
        range = 1;
        //targetRow = -1;
        //targetColumn = -1;
        if (id / 100 == 1)
            action = "attack";
        else if (id / 100 == 2)
            action = "defense";
        else
            action = "";
        if ((id % 100 - id % 10) / 10 == 1)
            type = "smash";
        else if ((id % 100 - id % 10) / 10 == 2)
            type = "slice";
        else
            type = "";
    }

    //copy constructor
    public Card (Card card) {
        id = card.id;
        cost = card.cost;
        value = card.value;
        range = card.range;
        //targetRow = card.targetRow;
        //targetColumn = card.targetColumn;
        action = card.action;
        type = card.type;
    }

    //attributes
    public int ID {
        get { return id; }
    }

    public int Cost {
        get { return cost; }
    }

    public int Value {
        get { return value; }
    }

    public int Range {
        get { return range; }
    }

    /*public int TargetRow {
        get { return targetRow; }
    }

    public int TargetColumn {
        get { return targetColumn; }
    }*/

    public string Action {
        get { return action; }
    }

    public string Type {
        get { return type; }
    }

    public void Clear () {
        id = 0;
        cost = 0;
        value = 0;
        range = 0;
        //targetRow = -1;
        //targetColumn = -1;
        action = "";
        type = "";  
    }

    //member function
    /*public void SetTarget (int[] targetPosition) {
        targetRow = targetPosition[0];
        targetColumn = targetPosition[1];
    }

    public string CheckTarget () {
        if (targetRow == -1 && targetColumn == -1)
            return "null";
        else
            return "exist";
    }*/

}
