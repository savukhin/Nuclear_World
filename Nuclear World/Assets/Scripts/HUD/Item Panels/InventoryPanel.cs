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
    }

}
