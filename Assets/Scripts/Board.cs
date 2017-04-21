using UnityEngine;
using System.Collections;

public class Board {

    //variable
    private const int DEFAULT_ROW = 1;
    private const int DEFAULT_COLUMN = 1;

    private int row = DEFAULT_ROW;
    private int column = DEFAULT_COLUMN;

    private Tile[,] board = new Tile[DEFAULT_ROW, DEFAULT_COLUMN];

    //default constructor
    public Board () {
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < column; j++) {
                board[i, j] = new Tile();
            }
        }
    }

    //regular constructor
    public Board (int row, int column) {
        this.row = row;
        this.column = column;
        board = new Tile[row, column];
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < column; j++) {
                board[i, j] = new Tile(i, j);
            }
        }
    }

    public int Row {
        get { return row; }
    }

    public int Column {
        get { return column; }
    }

    //member function
    public Tile GetTile (int rowIndex, int columnIndex) {
        return board[rowIndex, columnIndex];
    }

    public string GetCharacterLabel (int rowIndex, int columnIndex) {
        return board[rowIndex, columnIndex].Character;
    }

    public void SetCharacterLabel (int rowIndex, int columnIndex, string character) {
        board[rowIndex, columnIndex].Character = character;
    }

    /*public Card GetAggr (int rowIndex, int columnIndex) {
        return board[rowIndex, columnIndex].Aggr;
    }*/

    /*public void SetAggr (int rowIndex, int columnIndex, Card aggr) {
        board[rowIndex, columnIndex].Aggr = new Card(aggr);
    }*/

    /*public int[] FindAggr () {
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < column; j++) {
                if (board[i, j].Aggr.ID != 0 && board[i, j].Aggr.TargetColumn != -1 && board[i, j].Aggr.TargetRow != -1) {
                    return new int[2] { i, j };
                }
            }
        }
        return new int[2] { -1, -1 };
    }*/

}
