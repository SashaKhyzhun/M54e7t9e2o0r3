using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum States { Start, Menu, Shop, Game, GameOver, Intermediate }

public class StateMachine : MonoBehaviour {

	public States state;
	public AnimationController animationContoller;
	public Button catcher;

	private Coroutine currentCoroutine;
	private WaitForSeconds animWFS;

	void Start() {
		animWFS = new WaitForSeconds (animationContoller.animationTime);
		StartToMenu ();
	}

	public void StartToMenu() {
		if (currentCoroutine == null) {
			currentCoroutine = StartCoroutine (StartToMenuCoroutine());
		}
	}

	public void MenuToGame() {
		if (currentCoroutine == null) {
			currentCoroutine = StartCoroutine (MenuToGameCoroutine());
		}
	}

	public void MenuToShop() {
		if (currentCoroutine == null) {
			currentCoroutine = StartCoroutine (MenuToShopCoroutine());
		}
	}

	public void ShopToMenu() {
		if (currentCoroutine == null) {
			currentCoroutine = StartCoroutine (ShopToMenuCoroutine());
		}
	}

	public void ShopToGameOver() {
		if (currentCoroutine == null) {
			currentCoroutine = StartCoroutine (ShopToGameOverCoroutine());
		}
	}

	public void GameToGameOver() {
		if (currentCoroutine == null) {
			currentCoroutine = StartCoroutine (GameToGameOverCoroutine());
		}
	}

	public void GameOverToGame() {
		if (currentCoroutine == null) {
			currentCoroutine = StartCoroutine (GameOverToGameCoroutine());
		}
	}

	public void GameOverToShop() {
		if (currentCoroutine == null) {
			currentCoroutine = StartCoroutine (GameOverToShopCoroutine());
		}
	}

	IEnumerator StartToMenuCoroutine() {
		state = States.Intermediate;
		animationContoller.SplashScreenPlay ();
		yield return new WaitForSeconds(animationContoller.splashScreenDuration);
		animationContoller.MenuToggle (true);
		yield return animWFS;
		state = States.Menu;
		currentCoroutine = null;
	}

	IEnumerator MenuToGameCoroutine() {
		state = States.Intermediate;
		animationContoller.MenuToggle (false);
		yield return animWFS;
		animationContoller.GameToggle (true);
		yield return animWFS;
		catcher.interactable = true;
		state = States.Game;
		currentCoroutine = null;
	}

	IEnumerator MenuToShopCoroutine() {
		state = States.Intermediate;
		animationContoller.MenuToggle (false);
		yield return animWFS;
		animationContoller.ShopToggle (true);
		yield return animWFS;
		state = States.Shop;
		currentCoroutine = null;
	}

	IEnumerator ShopToMenuCoroutine() {
		state = States.Intermediate;
		animationContoller.ShopToggle (false);
		yield return animWFS;
		animationContoller.MenuToggle (true);
		yield return animWFS;
		state = States.Menu;
		currentCoroutine = null;
	}

	IEnumerator ShopToGameOverCoroutine() {
		state = States.Intermediate;
		animationContoller.ShopToggle (false);
		yield return animWFS;
		animationContoller.GameOverToggle (true);
		animationContoller.GameToggle (true);
		yield return animWFS;
		state = States.GameOver;
		currentCoroutine = null;
	}

	IEnumerator GameToGameOverCoroutine() {
		state = States.Intermediate;
		animationContoller.GameOverToggle (true);
		yield return animWFS;
		catcher.interactable = false;
		state = States.GameOver;
		currentCoroutine = null;
	}

	IEnumerator GameOverToGameCoroutine() {
		state = States.Intermediate;
		animationContoller.GameOverToggle (false);
		yield return animWFS;
		animationContoller.GameToggle (true);
		yield return animWFS;
		catcher.interactable = true;
		state = States.Game;
		currentCoroutine = null;
	}

	IEnumerator GameOverToShopCoroutine() {
		state = States.Intermediate;
		animationContoller.GameOverToggle (false);
		animationContoller.GameToggle (false);
		yield return animWFS;
		animationContoller.ShopToggle (true);
		yield return animWFS;
		state = States.Shop;
		currentCoroutine = null;
	}
}
