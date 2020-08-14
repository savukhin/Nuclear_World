using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPanel : SlotsPanel {

    private void Start() {
        InitializateSlots();
    }

    public void SetChest(Container state) {
        container = state;
    }
}
