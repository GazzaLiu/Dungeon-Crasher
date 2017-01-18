using UnityEngine;
using System.Collections;

public class Enemy : Entity {

    public bool isTurn = false;
    public bool isAttacked = false;
    public int e_tag = 0;
    public int HP = 0;
    public int range = 2;

    public int[] position = new int[2] { 0, 0 };
    public int[] card_id = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public GameObject manager;
    public Stage1_Manager m;
    public Card pass = new Card();

    public Card[] hand = new Card[3];

    private int randRow = 0;
    private int randColumn = 0;

    private int[] startPosition = new int[2];
    private int[] endPosition = new int[2];

    private SpriteRenderer sr;

    private Deck deck = new Deck();
    private Deck fold = new Deck();

    void Start () {
        m = manager.GetComponent<Stage1_Manager>();
        sr = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        pass = new Card();
        deck = new Deck(card_id);
        deck.Shuffle();
        hand[0] = deck.Draw();
        hand[1] = deck.Draw();
        hand[2] = deck.Draw();
    }

    void Update () {

        //display position
        this.transform.position = new Vector3(Horizontalposition(position[1]), Verticallposition(position[0]), 0);
        sr.sortingOrder = position[0];

        //death event
        if (HP <= 0) {
            m.DeathEvent(position, "e" + e_tag.ToString());
            Destroy(this.gameObject);
        }

        //enemy's turn
        if (isTurn && m.enemyTurn % 4 == (e_tag * 2 - 2)) {

            //recycle passive card
            if (m.cardBoard[position[0], position[1], 1].ID != 0) {
                fold.Add(m.cardBoard[position[0], position[1], 1]);
                m.cardBoard[position[0], position[1], 1].Clear();
                pass.Clear();
            }

            //move enemy

            //attack
            //GetMaxCard(hand, "attack");
            /*if (GetMaxCard(hand, "attack").ID != 0) {
                attack here
            }*/
            m.CheckAggr();

            //defence
            //GetMaxCard(hand, "defence");
            /*if (GetMaxCard(hand, "defence").ID != 0) {
                defence here
            }*/

            //end turn
            m.EndTurn("e" + e_tag.ToString());

        }
    }

    public int GetHandNumber () {
        int number = 0;
        foreach (Card card in hand) {
            if (card.ID != 0) {
                number++;
            }
        }
        return number;
    }

    private Card GetMaxCard (Card[] card, string action) {
        Card tempMaxCard = new Card();
        for (int i = 0; i < card.Length; i++) {
            if (card[i].Action == action && card[i].Value >= tempMaxCard.Value) {
                tempMaxCard = new Card(card[i]);
            }
        }
        return tempMaxCard;
    }

}
