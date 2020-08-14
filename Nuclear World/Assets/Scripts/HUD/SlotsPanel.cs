using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class SlotsPanel : MonoBehaviour {
    public GameObject[] emptySlots;
    public GameObject Slot_prefab;
    public Container container;

    protected void InitializateSlots() {
        for (int i = 0; i < emptySlots.Length; i++) {
            emptySlots[i].GetComponent<BoxCollider>().size =
                new Vector3(emptySlots[i].GetComponent<RectTransform>().rect.size.x,
                    emptySlots[i].GetComponent<RectTransform>().rect.size.y, 1f);
            emptySlots[i].GetComponent<EmptySlot>().owner = this;
            emptySlots[i].GetComponent<EmptySlot>().number = i;
        }
    }

    public virtual void AddCell(int number, Item state) {
        GameObject newCell = Instantiate(Slot_prefab, emptySlots[number].transform.position, Quaternion.identity, transform.parent);
        emptySlots[number].GetComponent<EmptySlot>().cell = newCell.GetComponent<Cell>();
        newCell.GetComponent<Cell>().position = number;
        newCell.GetComponent<Cell>().currentSlot = emptySlots[number];
        newCell.GetComponent<Cell>().SetItem(state);
    }

    public virtual void AddItem(Item state) {
        container.AddItem(state);
    }

    public virtual void AddItem(int number, Item state) {
        container.AddItem(number, state);
    }

    public virtual void DeleteItem(int number) {
        container.DeleteItem(number);
    }

    public virtual void ChangeItems(int number1, int number2) {
        container.ChangeItems(number1, number2);
    }

    public virtual void ChangeItem(int number, Item state) {
        container.ChangeItem(number, state);
    }
}
