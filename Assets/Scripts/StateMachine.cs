using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum States
{
    Start,
    Menu,
    Shop,
    Game,
    GameOver,
    Intermediate

}

public class StateMachine : MonoBehaviour
{

    public States state;
    public AnimationController animationContoller;
    public ScoreController scoreController;
    public PlayerController playerController;
    public Button catcher;

    private Coroutine currentCoroutine;
    private WaitForSeconds animWFS;
    private States prevState;

    void Start()
    {
        animWFS = new WaitForSeconds(animationContoller.animationTime);
        StartToMenu();
    }

    public void StartToMenu()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(StartToMenuCoroutine());
        }
    }

    public void MenuToGame()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(MenuToGameCoroutine());
        }
    }

    public void MenuToShop()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(MenuToShopCoroutine());
        }
    }

    public void MenuToTutorial()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(MenuToTutorialCoroutine());
        }
    }


    public void ShopToMenu()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(ShopToMenuCoroutine());
        }
    }

    public void TutorialToMenu()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(TutorialToMenuCoroutine());
        }
    }

    public void ShopToGameOver()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(ShopToGameOverCoroutine());
        }
    }

    public void TutorialToGameOver()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(TutorialToGameOverCoroutine());
        }
    }

    public void GameToGameOver()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(GameToGameOverCoroutine());
        }
    }

    public void GameOverToGame()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(GameOverToGameCoroutine());
        }
    }

    public void GameOverToShop()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(GameOverToShopCoroutine());
        }
    }

    public void GameOverToTutorial()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(GameOverToTutorialCoroutine());
        }
    }

    public void BackFromTutorial()
    {
        switch (state)
        {
            case States.Menu:
                TutorialToMenu();
                break;
            case States.GameOver:
                TutorialToGameOver();
                break;
        }
    }

    public void BackFromShop()
    {
        switch (prevState)
        {
            case States.Menu:
                ShopToMenu();
                break;
            case States.GameOver:
                ShopToGameOver();
                break;
        }
    }

    IEnumerator StartToMenuCoroutine()
    {
        state = States.Intermediate;
        animationContoller.SplashScreenPlay();
        yield return new WaitForSeconds(animationContoller.splashScreenDuration);
        animationContoller.MenuToggle(true);
        yield return animWFS;
        state = States.Menu;
        currentCoroutine = null;
    }

    IEnumerator MenuToGameCoroutine()
    {
        state = States.Intermediate;
        animationContoller.MenuToggle(false);
        yield return animWFS;
        animationContoller.GameToggle(true);
        yield return animWFS;
        catcher.interactable = true;
        state = States.Game;
        currentCoroutine = null;
    }

    IEnumerator MenuToShopCoroutine()
    {
        state = States.Intermediate;
        animationContoller.MenuToggle(false);
        yield return animWFS;
        animationContoller.ShopToggle(true);
        yield return animWFS;
        state = States.Shop;
        prevState = States.Menu;
        currentCoroutine = null;
    }

    IEnumerator MenuToTutorialCoroutine()
    {
        state = States.Intermediate;
        animationContoller.TutorialToggle(true);
        yield return animWFS;
        state = States.Menu;
        currentCoroutine = null;
    }

    IEnumerator ShopToMenuCoroutine()
    {
        state = States.Intermediate;
        animationContoller.ShopToggle(false);
        yield return animWFS;
        animationContoller.MenuToggle(true);
        yield return animWFS;
        state = States.Menu;
        currentCoroutine = null;
    }

    IEnumerator TutorialToMenuCoroutine()
    {
        state = States.Intermediate;
        animationContoller.TutorialToggle(false);
        yield return animWFS;
        state = States.Menu;
        currentCoroutine = null;
    }

    IEnumerator ShopToGameOverCoroutine()
    {
        state = States.Intermediate;
        animationContoller.ShopToggle(false);
        yield return animWFS;
        animationContoller.GameOverToggle(true);
        animationContoller.GameToggle(true);
        yield return animWFS;
        state = States.GameOver;
        currentCoroutine = null;
    }

    IEnumerator TutorialToGameOverCoroutine()
    {
        state = States.Intermediate;
        animationContoller.TutorialToggle(false);
        yield return animWFS;
        state = States.GameOver;
        currentCoroutine = null;
    }

    IEnumerator GameToGameOverCoroutine()
    {
        state = States.Intermediate;
        catcher.interactable = false;
        animationContoller.GameOverToggle(true);
        yield return animWFS;
        state = States.GameOver;
        currentCoroutine = null;
    }

    IEnumerator GameOverToGameCoroutine()
    {
        state = States.Intermediate;
        animationContoller.GameOverToggle(false);
        yield return animWFS;
        /////
        scoreController.ResetScore();
        playerController.Revive();
        /////
        animationContoller.GameToggle(true);
        yield return animWFS;
        catcher.interactable = true;
        state = States.Game;
        currentCoroutine = null;
    }

    IEnumerator GameOverToShopCoroutine()
    {
        state = States.Intermediate;
        animationContoller.GameOverToggle(false);
        animationContoller.GameToggle(false);
        yield return animWFS;
        animationContoller.ShopToggle(true);
        yield return animWFS;
        state = States.Shop;
        prevState = States.GameOver;
        currentCoroutine = null;
    }

    IEnumerator GameOverToTutorialCoroutine()
    {
        state = States.Intermediate;
        animationContoller.TutorialToggle(true);
        yield return animWFS;
        state = States.GameOver;
        currentCoroutine = null;
    }
}
