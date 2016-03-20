using UnityEngine;
using System.Collections;

public class MeteorController : MonoBehaviour {

    //public ScoreController scoreController;

	public float lifeSpan = 5;
    public int lives = 1;

	private WaitForSeconds lifeSpanWFS;
	private int lifeCounter;

	void Awake()
	{
		lifeCounter = lives;
		lifeSpanWFS = new WaitForSeconds (lifeSpan);
	}

    void OnCollisionEnter2D(Collision2D collision)
	{
        //if (collision.gameObject.CompareTag("Player")) {
            //scoreController.ScoreUp ();
            //тут буде убивати ігрока
        //}
        Kill();
    }

	void OnEnable() 
	{
		lifeCounter = lives; // reset lives and ready to go
		StartCoroutine (Live ()); // if enabled - begin to live;
	}

	void OnDisable() 
	{
		StopAllCoroutines (); // if disabled - stop counting seconds to death
	}

	IEnumerator Live()
	{
		yield return lifeSpanWFS; // you have only this much
		Kill (); // the only purpose of life is death
	}

    void CheckLives() {
        if (lifeCounter <= 0) {
            //scoreController.Wasted ();
            Kill(); // you ran out of lives! you have to die;
        }
    }

	void Kill()
	{
		gameObject.SetActive (false); // not very interesting death
	}

}