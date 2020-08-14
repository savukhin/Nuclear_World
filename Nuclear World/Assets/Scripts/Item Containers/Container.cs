using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Container : MonoBehaviour {
    public Item[] items;

    [NonSerialized]
    public Character subject;

    public Container(int itemCount) {
        items = new Item[itemCount];
    }

    public virtual void AddItem(Item state) {
        for (int i = 0; i < items.Length; i++) {
            if (items[i] == null) {
                items[i] = state;
                AdditionalAction(i);
                return;
            }
        }
    }

    public virtual void AddItem(int number, Item state) {
        items[number] = state;
        AdditionalAction(number);
    }

    public virtual void ChangeItems(int number1, int number2) {
        Item t = items[number1];
        items[number1] = items[number2];
        items[number2] = t;
        AdditionalAction(number1);
        AdditionalAction(number2);
    }

    public virtual void DeleteItem(int number) {
        items[number] = null;
        AdditionalAction(number);
    }

    public virtual void ChangeItem(int number, Item item) {
        items[number] = item;
        AdditionalAction(number);
    }

    public virtual void AdditionalAction(int number) {}
}
