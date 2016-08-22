using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum States
{
    Start,
    Menu,
    Settings,
    Game,
    GameOver,
    Intermediate

}

public class StateMachine : MonoBehaviour
{

    public States state;
    public AnimationController animationContoller;
    public ScoreController scoreController;
    public GPGController gpgController;
    public PlayerController playerController;
    public SkinChanger skinChanger;
    public Button catcher;

    private Coroutine currentCoroutine;
    private WaitForSeconds animWFS;
    private WaitForSeconds splashWFS;
    private WaitForSeconds splashMinWFS;
    private WaitForEndOfFrame wfeof;
    private States prevState;

    void Awake()
    {
        Input.multiTouchEnabled = false;
    }

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        animWFS = new WaitForSeconds(animationContoller.animationTime);
        splashWFS = new WaitForSeconds(animationContoller.splashScreenTime);
        splashMinWFS = new WaitForSeconds(animationContoller.splashMinTime);
        wfeof = new WaitForEndOfFrame();
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

    public void MenuToSettings()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(MenuToSettingsCoroutine());
        }
    }

    public void MenuToTutorial()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(MenuToTutorialCoroutine());
        }
    }


    public void SettingsToMenu()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(SettingsToMenuCoroutine());
        }
    }

    public void TutorialToMenu()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(TutorialToMenuCoroutine());
        }
    }

    public void SettingsToGameOver()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(SettingsToGameOverCoroutine());
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

    public void GameOverToSettings()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(GameOverToSettingsCoroutine());
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

    public void BackFromSettings()
    {
        switch (prevState)
        {
            case States.Menu:
                SettingsToMenu();
                break;
            case States.GameOver:
                SettingsToGameOver();
                break;
        }
    }

    IEnumerator StartToMenuCoroutine()
    {
        float elapsed = 0;
        bool timeout = false;
        state = States.Intermediate;
        animationContoller.SplashScreenToggle(true);
        yield return splashWFS;
        state = States.Start;
        yield return splashMinWFS;
        while (!(GPGController.NoGPGMode || timeout || SaveLoad.loadFinished))
        {
            yield return wfeof;
            elapsed += Time.deltaTime;
            if (elapsed >= GPGController.timeOutTime)
                timeout = true;
        }
        SaveLoad.ChooseSavedGame();
        gpgController.SubmitScore(Game.current.best);
        scoreController.LoadStats();
        skinChanger.LoadStats();
        animationContoller.SplashScreenToggle(false);
        state = States.Intermediate;
        yield return splashWFS;
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

    IEnumerator MenuToSettingsCoroutine()
    {
        state = States.Intermediate;
        skinChanger.RefreshSettings();
        animationContoller.MenuToggle(false);
        yield return animWFS;
        animationContoller.SettingsToggle(true);
        yield return animWFS;
        state = States.Settings;
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

    IEnumerator SettingsToMenuCoroutine()
    {
        state = States.Intermediate;
        animationContoller.SettingsToggle(false);
        //yield return animWFS; //removed for more fluid transition
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

    IEnumerator SettingsToGameOverCoroutine()
    {
        state = States.Intermediate;
        animationContoller.SettingsToggle(false);
        //yield return animWFS; //removed for more fluid transition
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

    IEnumerator GameOverToSettingsCoroutine()
    {
        state = States.Intermediate;
        skinChanger.RefreshSettings();
        animationContoller.GameOverToggle(false);
        animationContoller.GameToggle(false);
        yield return animWFS;
        animationContoller.SettingsToggle(true);
        yield return animWFS;
        state = States.Settings;
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
