using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeGamePlayer : MonoBehaviour
{
    #region Variable
    public int HP = 3; 
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D PlayerCollider;
    public Joystick joystick;
    private Vector2 movement;
    //private Transform PlayerTransform;
    #endregion
    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        PlayerCollider = this.gameObject.GetComponent<BoxCollider2D>();
       // PlayerTransform = this.gameObject.GetComponent<Transform>();
    }
    private void Update()
    {
        #region joystick Move
        if (joystick.Horizontal >= 0.2f || joystick.Horizontal <= -0.2f)
        {
            movement.x = joystick.Horizontal;
        }
        else
        {
            movement.x = 0;
        }
        if (joystick.Vertical >= 0.2f || joystick.Vertical <= -0.2f)
        {
            movement.y = joystick.Vertical;
        }
        else
        {
            movement.y = 0;
        }
        //animation
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        #endregion
        if (HP <= 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //switch case 
        switch (collision.tag)
        {
            case "Bullet":
                GameObject.Destroy(collision.gameObject);
                GameObject.FindObjectOfType<DodgeGameSoundEffect>().HurtSoundEffect();
                HP--;
                if (HP <= 0)
                {
                    GameObject.FindObjectOfType<DodgeGame>().GameOverCall();
                }
                return;
            case "Heart":
                GameObject.Destroy(collision.gameObject);
                if (HP >= 10)
                {
                    GameObject.FindObjectOfType<DodgeGameUIController>().Score += 10;
                    GameObject.FindObjectOfType<DodgeGameSoundEffect>().TooManySoundEffect();
                }
                else
                {
                    GameObject.FindObjectOfType<DodgeGameSoundEffect>().HealSoundEffect();
                    HP++;
                }
                return;
            case "Shoes":
                GameObject.Destroy(collision.gameObject);
                if (moveSpeed > 10)
                {
                    GameObject.FindObjectOfType<DodgeGameUIController>().Score += 10;
                    GameObject.FindObjectOfType<DodgeGameSoundEffect>().TooManySoundEffect();
                }
                else
                {
                    moveSpeed++;
                    GameObject.FindObjectOfType<DodgeGameSoundEffect>().ShoesSoundEffect();
                }
                return;
            case "Big":
                GameObject.Destroy(collision.gameObject);
                if (transform.localScale.x < 9.8f)
                {
                    transform.localScale = new Vector3(transform.localScale.x + 0.5f, transform.localScale.y + 0.5f, transform.localScale.z + 0.5f);
                    GameObject.FindObjectOfType<DodgeGameSoundEffect>().MagicSoundEffect();
                }
                else
                {
                    GameObject.FindObjectOfType<DodgeGameUIController>().Score += 10;
                    GameObject.FindObjectOfType<DodgeGameSoundEffect>().TooManySoundEffect();
                }
                return;
            case "Small":
                GameObject.Destroy(collision.gameObject);
                if (transform.localScale.x > 4.8f)
                {
                    transform.localScale = new Vector3(transform.localScale.x - 0.5f, transform.localScale.y - 0.5f, transform.localScale.z - 0.5f);
                    GameObject.FindObjectOfType<DodgeGameSoundEffect>().MagicSoundEffect();
                }
                else
                {
                    GameObject.FindObjectOfType<DodgeGameUIController>().Score += 10;
                    GameObject.FindObjectOfType<DodgeGameSoundEffect>().TooManySoundEffect();
                }
                return;
            default:
                return;
        }
    }
}
