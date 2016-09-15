using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour
{
    public Animator splashScreenLayout;
    public Animator tutorialLayout;
    public Animator SettingsLayout;
    public Animator menuLayout;
    public Animator gameLayout;
    public Animator gameOverLayout;
    public Animator tooltip;

    public float animationTime = 0.5f;
    public float splashScreenTime = 4.5f;
    public float splashMinTime = 1f;
    public string animBoolName = "enable";

    public void SplashScreenToggle(bool toggle)
    {
        splashScreenLayout.SetBool(animBoolName, toggle);
    }

    public void SettingsToggle(bool toggle)
    {
        SettingsLayout.SetBool(animBoolName, toggle);
    }

    public void MenuToggle(bool toggle)
    {
        menuLayout.SetBool(animBoolName, toggle);
    }

    public void GameToggle(bool toggle)
    {
        gameLayout.SetBool(animBoolName, toggle);
    }

    public void GameOverToggle(bool toggle)
    {
        gameOverLayout.SetBool(animBoolName, toggle);
    }

    public void TutorialToggle(bool toggle)
    {
        tutorialLayout.SetBool(animBoolName, toggle);
    }

    public void TooltipToggle(bool toggle)
    {
        tooltip.SetBool(animBoolName, toggle);
    }
}

