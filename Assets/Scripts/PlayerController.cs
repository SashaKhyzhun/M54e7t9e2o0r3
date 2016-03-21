using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 5f;

    private InputHeandler inputHeandler;
    private PlayerController playerController;
    private Rigidbody2D rb;
    private Animator anim;
    private Touch currTouch;
    private bool canStart = true;
    private bool canFlip = true;
    private bool dead = false;
    private float direction = 0;

    void Start () {
        inputHeandler = GetComponent<InputHeandler>();
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Dangerous")) {
            if (dead == false) {
                dead = true;
                anim.SetBool("dead", true);
            }
        }
    }

    void Update() {
        if (canStart) {
            if (inputHeandler.started) {
                direction = -0.7f;
                anim.SetBool("started", true);
                canStart = false;
            }
        }
            if (inputHeandler.touched) {
                direction *= -1;
          }
    }

    void FixedUpdate () {
        rb.MovePosition(transform.position + Vector3.right * speed * direction * Time.deltaTime);
        Vector3 theScale = transform.localScale;
        if (direction != 0) {
            theScale.x = direction;
        }
        transform.localScale = theScale;
    }

  
}
