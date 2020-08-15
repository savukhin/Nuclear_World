using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : InteractiveObject {
    public DialogueUnit currentDialogUnit;

    public DialogueUnit GetDialogueUnit() {
        return currentDialogUnit;
    }
}
