using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MeleeMob {
    public override void Activate() {
        base.Activate();
        StartCoroutine("PersueTheTarget");
    }

    public override void Deactivate() {
        base.Deactivate();
        StopCoroutine("PersueTheTarget");
        animator.SetBool("Attack", true);
    }

    IEnumerator PersueTheTarget() {
        for (;;) {
            target = activationZone.target.transform.position;
            target = new Vector3(target.x, 0, target.z);
            if (MoveToTarget()) {
                animator.SetTrigger("Attack");
                Attack();
                yield return new WaitForSeconds(combo.GetNextAttackDuration());
            } else {
            }
            yield return null;
        }
    }

    protected override void Update() {
        base.Update();
        
    }
}
