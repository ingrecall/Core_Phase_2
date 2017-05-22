using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Transform mytarget;
    float myDamage;
    float mySpeed;

    public float MyDamage
    {
        get
        {
            return myDamage;
        }
        set
        {
            myDamage = value;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if (mytarget == null)
            return;
        transform.position += transform.forward * mySpeed * Time.deltaTime;
    }

    public void FireBullet(Transform inputTarget, float inputDamage, float inputSpeed)
    {
        mytarget = inputTarget;
        myDamage = inputDamage;
        mySpeed = inputSpeed;
    }
}
