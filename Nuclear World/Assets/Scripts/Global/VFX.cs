using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    public float duration;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = duration;
    }

    // Update is called once per frame
     void Update() {
          timeLeft -= Time.deltaTime;
          if (timeLeft <= 0.0f) {
              Destroy(this.gameObject);
          }
     }
}
