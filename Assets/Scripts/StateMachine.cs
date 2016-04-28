using UnityEngine;
using System.Collections;

public enum States { Start, Menu, Shop, Game, GameOver, Intermediate }

public class StateMachine : MonoBehaviour {

	public States state;
	public Animator splash;
	public Animator menuLayout;
	public Animator shopLayout;
	public Animator gameLayout;
	public Animator gameOverLayout;
	public float animationTime;

	private Coroutine currentCoroutine;
	private WaitForSeconds animWFS;
	private string animBool = "enable";

	void Start() {
		animWFS = new WaitForSeconds (animationTime);
	}

	public void StartToMenu() {
		if (currentCoroutine == null) {
			currentCoroutine = StartCoroutine (StartToMenuCoroutine());
		}
	}

	public void MenuToGame() {

	}

	public void MenuToShop() {

	}

	public void ShopToMenu() {

	}

	public void ShopToGameOver() {

	}

	public void GameToGameOver() {

	}

	public void GameOverToGame() {

	}

	public void GameOverToShop() {
	
	}

	IEnumerator StartToMenuCoroutine() {
		state = States.Intermediate;

		yield return animWFS;
		state = States.Menu;
	}


}
