using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol1 : PrimaryWeapon
{
    public Bullet bulletPrefab;
    public float spread = 3;

    public override bool strike()
    {
        if (!base.strike())
            return false;
        
        Instantiate(strikeVFXPrefab, firePoint.transform);
        var bullet = Instantiate(bulletPrefab, firePoint.transform);
        var randomNumberX = Random.Range(-spread, spread);
        var randomNumberY = Random.Range(-spread, spread);
        bullet.transform.Rotate(randomNumberX, randomNumberY, 0);
        bullet.notWorkAgainstObjects.Add(gameObject);
        return true;
    }
}
