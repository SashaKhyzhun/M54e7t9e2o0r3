using UnityEngine;
using System.Collections;

public class Coin : ProjectileController {

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
            //Debug.Log("MONETKA TOCHED");
            audioManager.coinSound.Play();
			scoreController.CoinScoreUp();
			Kill();
		}
	}

	protected override void Kill() {
		//Debug.Log ("I have beed picked up. I'm " + gameObject.name);
		gameObject.SetActive(false);
	}

}

