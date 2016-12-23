using UnityEngine;
using System.Collections;

public class Deck : Entity {

    const int DEFAULT_NUMBER = 10;
    private Card[] deck = new Card[DEFAULT_NUMBER];

    public Deck () {
        for (int i = 0; i < DEFAULT_NUMBER; i++) {
            deck[i] = new Card(111);
        }
    }

    public Deck (int[] card_id) {
        int nCard = card_id.Length;
        this.deck = new Card[nCard];
        for (int i = 0; i < nCard; i++) {
            deck[i] = new Card(card_id[i]);
        }
    }

    public void ShowDeck () {
        foreach (Card card in deck) {
            print(card.Action + " " + card.Type + " " + card.Value.ToString());
        }
    }
}
