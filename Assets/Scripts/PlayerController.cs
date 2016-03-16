using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


    public float speed = 5f;
    bool facingRight = true;

    private Animator anim;

	void Start () {
        anim = GetComponent<Animator>();
	}


    void FixedUpdate () {

        float move = Input.GetAxis("Horizontal");

        GetComponent<Rigidbody2D>().velocity = new Vector2(move * speed, 0);
        if (move > 0 && !facingRight) {
            Flip();
        } else if (move < 0 && facingRight) {
            Flip();
        }
    
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
