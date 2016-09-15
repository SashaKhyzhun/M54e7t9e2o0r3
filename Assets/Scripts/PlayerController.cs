using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public ScoreController scoreController;
    public float speed = 5f;

    private InputHeandler inputHeandler;
    private Rigidbody2D rb;
    private Animator anim;
    private Touch currTouch;
    private bool dead = false;
    private bool canStart = true;
    private bool canFlip = true;
    private float direction = 0;

    void Start()
    {
        if (scoreController == null)
        {
            scoreController = GetComponent<ScoreController>();
        }
        inputHeandler = GetComponent<InputHeandler>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (canStart)
        {
            if (inputHeandler.started)
            {
                direction = 1f;
                anim.SetBool("started", true);
                canStart = false;
            }
        }
        if (inputHeandler.touched)
        {
            direction *= -1;
        }
    }

    void FixedUpdate()
    {
        if (!dead)
        {
            rb.MovePosition(transform.position + Vector3.right * speed * direction * Time.deltaTime);
        }
        if (canFlip)
        {
            Vector3 theScale = transform.localScale;
            if (direction != 0)
            {
                theScale.x = direction;
            }
            transform.localScale = theScale;
        }
    }

    public void Die()
    {
        if (!dead)
        {
            dead = true;
            anim.SetBool("dead", true);
            anim.SetBool("started", false);
            canFlip = false;
            inputHeandler.started = false;
        }
    }

    public void Revive()
    {
        if (dead)
        {
            dead = false;
            anim.SetBool("dead", false);
            canFlip = true;
            canStart = true;
            direction = 0f;
        }
    }

    public void GoToCenter()
    {
        transform.position += new Vector3(-transform.position.x, 0, 0);
    }

    public void UpdateAnimator()
    {
        anim = GetComponent<Animator>();
    }
}
