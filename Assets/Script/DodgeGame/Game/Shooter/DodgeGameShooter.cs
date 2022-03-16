using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeGameShooter : MonoBehaviour
{
    public float time = 2;
    public float PeriodTime;
    [SerializeField]
    private GameObject Bullet;
    public GameObject BulletPrefab;
    private void Start()
    {
        PeriodTime = Random.Range(4, 5);
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= PeriodTime)
        {
            time = 0;
            PeriodTime = Random.Range(1, 5);
            Bullet = GameObject.Instantiate(BulletPrefab, transform.position, Quaternion.identity) as GameObject;
            Bullet.transform.SetParent(GameObject.Find("BulletContainer").transform);
        }
    }

}
