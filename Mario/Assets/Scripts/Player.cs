using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float speedBonus;
    public float jumpForce;
    public int score;
    public Text scoreText;

    private bool isGrounded;
    private Rigidbody2D rigidbody2d;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float speedStart;

    public float timerSpeed;
    public float timerSpeedMax;

    public float timerScale;
    public float timerScaleMax;



    private void Start()
    {
        speedStart = speed;
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Jump();
        }        

        Vector3 position = transform.position;

        position.x += Input.GetAxis("Horizontal") * speed;

        transform.position = position;

        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                spriteRenderer.flipX = false;
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                spriteRenderer.flipX = true;
            }
            animator.SetInteger("State", 1);        }
        else
        {
            animator.SetInteger("State", 0);
        }
        BonusCheck();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }       
    }

    private void Jump()
    {
        isGrounded = false;
        rigidbody2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void BonusCheck()
    {
        if (timerSpeed > 0)
        {
            speed = speedBonus;
            timerSpeed--;
        }
        else
        {
            speed = speedStart;
        }

        if (timerScale > 0)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1); 
            timerScale--;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


    }

    public void SpeedBonus()
    {
        timerSpeed = timerSpeedMax;
    }

    public void ScaleBonus()
    {
        timerScale = timerScaleMax;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    public void AddCoin(int count)
    {
        score += count;
        scoreText.text = score.ToString();
    } 
}
