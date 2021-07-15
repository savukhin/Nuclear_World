using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public int damage;
    public float bulletSpeed = 10;
    public float maxDist = 1000;
    public GameObject hitVFX;
    public List<GameObject> notWorkAgainstObjects;
    public List<string> notWorkAgainstTags;
    private Rigidbody m_rigidbody;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start() {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
        startPos = transform.position;
    }

    void Update() {
        if ((transform.position - startPos).magnitude > maxDist)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collider) {
        if (notWorkAgainstObjects.Contains(collider.gameObject) || notWorkAgainstTags.Contains(collider.tag))
            return;
        Instantiate(hitVFX, transform.position - transform.forward * 3, Quaternion.identity);
        Destroy(gameObject);
    }
}
