using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : SlotsPanel {

    private void Start() {
        InitializateSlots();
    }

    public void SetInventory(Inventory state) {
        container = state;
        Draw();
    }

    private void Draw() {
        for (int i = 0; i < container.items.Length; i++) {
            if (container.items[i] == null)
                continue;
            AddCell(i, container.items[i]);
        }
    }
}
