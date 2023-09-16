using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Gun
{
    public bool isDefault = true;

    public int ammo;

    public float projectileSpeed = 2f;
    //public float projectileCooldown = 1f;
    public float fireRate = 1f;
    public int bulletsPerShot = 1, bulletsLeft;
    public float spread;

}
