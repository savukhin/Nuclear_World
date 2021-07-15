using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {
    public GameObject firePoint;
    public Strike strikePrefab;
    public float fireRate;
    public GameObject strikeVFXPrefab;
    public float timeToFire = 0;

    public virtual bool strike() {
        if (timeToFire > Time.time)
            return false;
        timeToFire = Time.time + fireRate;
        return true;
    }
}
