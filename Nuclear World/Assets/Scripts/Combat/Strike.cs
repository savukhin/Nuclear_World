using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{
    public float damage;
    public GameObject hitVFX;

    // Start is called before the first frame update
    void Start()
    {
        RaycastHit info;
        if (Physics.Raycast(transform.position, transform.forward, out info)) {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
