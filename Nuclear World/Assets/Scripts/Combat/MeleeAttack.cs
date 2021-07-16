using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MeleeAttack : MonoBehaviour {
    public UnityEngine.Events.UnityEvent AttackEnded;
    [System.NonSerialized]
    public List<string> againstTags;
    public float attackDelay = 0.1f;
    public float duration = 1f;
    private BoxCollider m_collider;
    public float damageMin = 0;
    public float damageMax = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<BoxCollider>();
    }

    public void Activate() {
        StartCoroutine("Hit");
        this.Invoke(()=>AttackEnded.Invoke(), duration);
    }

    IEnumerator Hit() {
        float damage = Random.Range(damageMin, damageMax);
        yield return new WaitForSeconds(attackDelay);
        Collider[] hitColliders = Physics.OverlapBox(m_collider.transform.position, m_collider.size, m_collider.transform.rotation);
        
        foreach (var collider in hitColliders) {
            if (againstTags.Contains(collider.tag) && collider.GetComponent<Character>())
                collider.GetComponent<Character>().TakeDamage(damage);
        }
    }
}
