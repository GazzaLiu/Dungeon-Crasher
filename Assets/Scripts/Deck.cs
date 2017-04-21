using UnityEngine;
using System;
using System.Collections;

public class Deck {

    //variable
    private const int DEFAULT_NUMBER = 10;

    private int nCard = DEFAULT_NUMBER;

    private Card tempCard;

    private Card[] deck = new Card[DEFAULT_NUMBER];

    //default constructor
    public Deck () {
        for (int i = 0; i < nCard; i++) {
            deck[i] = new Card();
        }
    }

    //regular constructor
    public Deck (int[] card_id) {
        nCard = card_id.Length;
        deck = new Card[nCard];
        for (int i = 0; i < nCard; i++) {
            deck[i] = new Card(card_id[i]);
        }
    }

    //copy constructor
    public Deck (Deck deck) {
        nCard = deck.nCard;
        tempCard = deck.tempCard;
        for(int i = 0; i < deck.Length(); i++) {
            this.deck[i] = new Card(deck.Card(i));
        }
    }

    //member function
    public int Length () {
        return nCard;
    }

    public Card Card (int index) {
        return deck[index];
    }

    public void Clear () {
        foreach (Card card in deck) {
            card.Clear();
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
