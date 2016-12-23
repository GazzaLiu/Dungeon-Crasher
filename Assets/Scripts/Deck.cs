using UnityEngine;
using System;
using System.Collections;

public class Deck : Entity {

    const int DEFAULT_NUMBER = 10;

    private int nCard = 10;
    private int pointer = -1;
    private Card[] deck = new Card[DEFAULT_NUMBER];

    public Deck () {
        for (int i = 0; i < DEFAULT_NUMBER; i++) {
            deck[i] = new Card(111);
        }
    }

    public Deck (int[] card_id) {
        nCard = card_id.Length;
        deck = new Card[nCard];
        for (int i = 0; i < nCard; i++) {
            deck[i] = new Card(card_id[i]);
        }
    }

    public void ShowDeck () {
        foreach (Card card in deck) {
            print("(" + card.Action + "," + card.Type + "," + card.Value.ToString() + ")");
        }
    }

    public void Shuffle () {
        int[] randomArray = new int[nCard];
        for (int i = 0; i < nCard; i++) {
            randomArray[i] = UnityEngine.Random.Range(1, 100);
        }
        Array.Sort(randomArray, deck);
    }

    public Card Draw () {
        if(pointer == nCard - 1)
            Reset();
        pointer++;
        return deck[pointer];
    }

    private void Reset () {
        Shuffle();
        pointer = -1;
    }

}
