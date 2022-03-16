using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform Player;
    private Vector3 Target;
    private Vector3 Direction;
    public double Speed;
    private BulletController bulletController;
    private void Start()
    {
        bulletController = GameObject.FindObjectOfType<BulletController>();
        Player = GameObject.FindWithTag("Player").transform;
        Speed = bulletController.BulletSpeed;
        Direction = Player.position - transform.position;
        Direction.Normalize();
    }
    private void Update()
    {
        // Move in direction of target.
        float deltaSpeed = (float)Speed * Time.deltaTime;
        transform.Translate(Direction.x * deltaSpeed, Direction.y * deltaSpeed, 0);
       
    }
}
