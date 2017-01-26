using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    protected void DisplayBoard (string[,,] board, int floor) {
        string display = "";
        for (int i = 0; i <= board.GetUpperBound(0); i++) {
            for (int j = 0; j <= board.GetUpperBound(1); j++) {
                display += board[i, j, floor] + " ";
            }
            display += "\n";
        }
        print(display);
    }

    protected void DisplayCardBoard (Card[,,] board, int floor) {
        string display = "";
        for (int i = 0; i <= board.GetUpperBound(0); i++) {
            for (int j = 0; j <= board.GetUpperBound(1); j++) {
                display += board[i, j, floor].ID.ToString() + " ";
            }
            display += "\n";
        }
        print(display);
    }

    protected float Horizontalposition (int i) {
        float x = -58f / 30f + (2f * i) * (203f / 420f);
        return x;
    }

    protected float Verticallposition (int j) {
        float y = 106.5f / 30f - (2f * j) * (116f / 240f);
        return y;
    }

}
