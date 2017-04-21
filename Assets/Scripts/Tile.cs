using UnityEngine;
using System.Collections;

public class Tile {

    //variable
    private const string DEFAULT_CHARACTER = "n";

    private int row;
    private int column;
    private string character;

    //private Card aggr;

    //default constructor
    public Tile () {
        row = 0;
        column = 0;
        character = "";
        //aggr = new Card();
    }

    //regular constructor
    public Tile (int row, int column) {
        this.row = row;
        this.column = column;
        character = DEFAULT_CHARACTER;
        //aggr = new Card();
    }

    //attribute
    public int Row {
        get { return row; }
    }

    public int Column {
        get { return column; }
    }

    public string Character {
        get { return character; }
        set { character = value; }
    }

    /*public Card Aggr {
        get { return aggr; }
        set { aggr = value; }
    }*/

    //member function
    public void Clear () {
        character = DEFAULT_CHARACTER;
        //aggr.Clear();
    }

}
