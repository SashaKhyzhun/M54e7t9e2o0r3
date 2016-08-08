using UnityEngine;
using System.Collections;

public class Ball : ProjectileController
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            scoreController.Wasted();
            collision.gameObject.GetComponent<PlayerController>().Die();
        }
        else
        {
            scoreController.MeteorScoreUp();
        }

        Kill();

    }

    protected override void Kill()
    {
        //Debug.Log ("I have died. I'm " + gameObject.name);
        gameObject.SetActive(false);
    }


}

