using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public double BulletSpeed;
    private float time = 0;
    private float PeriodTime = 10;
    private void Start()
    {
        BulletSpeed = 3;
    }

    /*
    void Update()
    {
        time += Time.deltaTime;

        if (time >= PeriodTime)
        {
            time = 0;
            BulletSpeed += 0.1;
        }
    }
    */
    
}
