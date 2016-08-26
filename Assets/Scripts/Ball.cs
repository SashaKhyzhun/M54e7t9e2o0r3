using UnityEngine;
using System.Collections;

public class Ball : ProjectileController
{
    public float animationTime;

    private Animator anim;
    private Collider2D col;
    private Rigidbody2D rb;
    private WaitForSeconds animationTimewfs;
    private string triggerName = "explode";

    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animationTimewfs = new WaitForSeconds(animationTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        triggerName = "explode";
        if (collision.gameObject.CompareTag("Player"))
        {
            triggerName = "explodeMidAir";
            scoreController.Wasted();
            collision.gameObject.GetComponent<PlayerController>().Die();
        }
        else
        {
            scoreController.MeteorScoreUp();
        }
        audioManager.PlayRandomMeteorSound();
        StartCoroutine(KillCoroutine());
    }

    protected override void Kill()
    {
        //Debug.Log ("I have died. I'm " + gameObject.name);
        gameObject.SetActive(false);
    }

    IEnumerator KillCoroutine()
    {
        anim.SetTrigger(triggerName);
        col.enabled = false;
        rb.isKinematic = true;
        yield return animationTimewfs;
        col.enabled = true;
        rb.isKinematic = false;
        Kill();
    }

}

