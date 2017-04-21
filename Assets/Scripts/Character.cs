using UnityEngine;
using System.Collections;

public class Character : Entity {
    public int[] position = new int[2] { 0, 0 };
    public bool isActing = true;
    public bool isTurn;
    public int stamina = 5;
    public int stamina_max = 5;
    public int HP = 0;
    public int recovery = 2;
    public string type;
    public string order;
    public string label;
    public string status;
    public int range = 2;
    public Card pass = new Card();
    public Card[] hand = new Card[3];
    public int[] card_id = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public Deck deck = new Deck();
    public Deck fold = new Deck();

    public void ResetDeck () {
        deck = new Deck(fold);
        deck.Shuffle();
        fold.Clear();
    }

    protected string ActDirection (int[] position, int[] selectPosition) {
        if (selectPosition[0] - position[0] == -1 && selectPosition[1] - position[1] == 0)
            return "up";
        else if (selectPosition[0] - position[0] == 1 && selectPosition[1] - position[1] == 0)
            return "down";
        else if (selectPosition[0] - position[0] == 0 && selectPosition[1] - position[1] == 1)
            return "right";
        else if (selectPosition[0] - position[0] == 0 && selectPosition[1] - position[1] == -1)
            return "left";
        else
            return "n";
    }

}
