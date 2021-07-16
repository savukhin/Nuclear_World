using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMob : Mob {
    public ComboMeleeAttack combo;

    protected override void Start() {
        base.Start();
        combo.againstTags = new List<string>() {"Player"};
    }

    protected void Attack() {
        combo.Activate();
    }
}
