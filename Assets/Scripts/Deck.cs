using UnityEngine;
using System;
using System.Collections;

public class Deck : Entity {

    const int DEFAULT_NUMBER = 10;

    private int nCard = 10;
    private Card tempCard;
    private Card[] deck = new Card[DEFAULT_NUMBER];

    public Deck () {
        for (int i = 0; i < DEFAULT_NUMBER; i++) {
            deck[i] = new Card();
        }
    }

    public Deck (int[] card_id) {
        nCard = card_id.Length;
        deck = new Card[nCard];
        for (int i = 0; i < nCard; i++) {
            deck[i] = new Card(card_id[i]);
        }
    }

    public Deck (Deck deck) {
        nCard = deck.nCard;
        tempCard = deck.tempCard;
        for(int i = 0; i < deck.Length(); i++) {
            this.deck[i] = new Card(deck.Element(i));
        }
    }

    public int Length () {
        return nCard;
    }

    public Card Element (int index) {
        return deck[index];
    }

    public void Clear () {
        foreach (Card card in deck) {
            card.Clear();
        }
    }

    public void Show () {
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
        for (int i = 0; i < nCard; i++) {
            if (deck[i].ID != 0) {
                tempCard = new Card(deck[i]);
                deck[i].Clear();
                return tempCard;
            }
            else {
                if (i == nCard - 1) {
                    return new Card();
                }
                else
                    continue;
            }
        }
        return new Card();
    }

    public void Add (Card card) {
        for (int i = 0; i < nCard; i++) {
            if (deck[i].ID == 0) {
                deck[i] = new Card(card);
                break;
            }
            /*if (deck[i].ID != 0) {
                deck[i - 1] = new Card(card);
                break;
            }
            else {
                if (i == nCard - 1) {
                    deck[nCard - 1] = new Card(card);
                }
                else
                    continue;
            }*/
        }
    }

}
