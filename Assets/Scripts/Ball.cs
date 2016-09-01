using UnityEngine;
using System.Collections;

public class Ball : ProjectileController
{
    public AudioSource impactSound;
    public AudioSource flybySound;
    public float animationTime;
    public float groundLevel;
    public float soundStartTimeOffset;

    private Transform myTransform;
    private Animator anim;
    private Collider2D col;
    private Rigidbody2D rb;
    private WaitForSeconds animationTimewfs;
    private string triggerName = "explode";
    private float soundStartHeight;
    private bool needSound = true;

    void Start()
    {
        myTransform = transform;
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animationTimewfs = new WaitForSeconds(animationTime);
    }

    void Update()
    {
        if (needSound && myTransform.position.y < soundStartHeight)
        {
            flybySound.clip = audioManager.GetRandomMeteorFlybyClip();
            flybySound.Play();
            needSound = false;
        }
    }


    void OnEnable()
    {
        needSound = true;
        soundStartHeight = 0;
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
        impactSound.clip = audioManager.GetRandomMeteorClip();
        impactSound.Play();
        StartCoroutine(KillCoroutine());
    }

    public void GetSoundStartHeight(float speed, float time)
    {
        soundStartHeight = groundLevel + (speed * (time + soundStartTimeOffset));
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

