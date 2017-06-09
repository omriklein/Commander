using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    soldierBullet,
    TankBomb,
    AirplainBomb,
}

public class Bullet : MonoBehaviour
{
    public BulletType type; // get the prefub from Resorses
    public float damage;

    /// <summary>
    /// play the bullet hitting place animation and destroy the bullet gameobject
    /// </summary>
    public void destroyBullet()
    {
        //play animation
        GameObject.Destroy(this.gameObject);
    }
}
