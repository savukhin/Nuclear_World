using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Enemy {
    public Vector3 target;
    public float attackRange;
    public float attackRate = 1;
    public ActivationZone activationZone;
    protected bool attacking = false;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        StartCoroutine("WanderAround");
    }

    // Returns true if target in attack range
    protected bool MoveToTarget() {
        var q = Quaternion.LookRotation(target - new Vector3(transform.position.x, 0, transform.position.z));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 90 * Time.deltaTime);

        if ((target - transform.position).magnitude > attackRange && !attacking) {
            animator.SetBool("Move", true);
            Vector3 direction = (target - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
            return false;
        } else {
            animator.SetBool("Move", false);
            return true;
        }
    }

    IEnumerator WanderAround() {
        for(;;) {
            yield return new WaitForSeconds(5);
            Vector3 direction = new Vector3(Random.Range(0, 10f), 0, Random.Range(0, 10)).normalized;
            target = transform.position + direction * 10;
            target = new Vector3(target.x, 0, target.z);
            for (;;) {
                if (MoveToTarget())
                    break;
                yield return null;
            }
        }
    }

    public virtual void Activate() {
        target = activationZone.target.transform.position;
        StopCoroutine("WanderAround");
    }

    public virtual void Deactivate() {
        //target = new Vector3();
        StartCoroutine("WanderAround");
    }
}
