using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource splashSound;
    public AudioSource backgroundSound;
    public AudioSource coinSound;
    public AudioSource[] meteorSounds;
    public AudioSource[] meteorFlybySounds;

    public Sprite onGfx;
    public Sprite offGfx;

    public AudioMixerSnapshot defaultSnapshot;
    public AudioMixerSnapshot musicOffSnapshot;
    public AudioMixerSnapshot gameOverSnapshot;

    public float transitionTime = 0.1f;

    private bool musicState = true;
    private bool isGameOver;

    void Start()
    {
        defaultSnapshot.TransitionTo(0f);
    }

    public void ToggleAudio()
    {
        if (musicState)
        {
            musicOffSnapshot.TransitionTo(transitionTime);
            musicState = false;

        }
        else
        {
            if (isGameOver)
            {
                gameOverSnapshot.TransitionTo(transitionTime);
            }
            else
            {
                defaultSnapshot.TransitionTo(transitionTime);
            }
            musicState = true;
        }
    }

    public void GameOverToggle(bool toggle)
    {
        isGameOver = toggle;
        if (musicState)
        {
            if (toggle)
                gameOverSnapshot.TransitionTo(transitionTime);
            else
                defaultSnapshot.TransitionTo(transitionTime);
        }
    }

    public void ChangeImageColor(ButtonHolder buttons)
    {
        if (!musicState) // if on, then turn off
        {
            foreach (GameObject b in buttons.buttons)
            {
                b.GetComponent<Image>().sprite = offGfx;
            }
        }
        else // else turn on
        {
            foreach (GameObject b in buttons.buttons)
            {
                b.GetComponent<Image>().sprite = onGfx;
            }
        }
    }

    public AudioClip GetRandomMeteorClip()
    {
        int i = Random.Range(0, meteorSounds.Length - 1);
        return meteorSounds[i].clip;
    }

    public AudioClip GetRandomMeteorFlybyClip()
    {
        int i = Random.Range(0, meteorFlybySounds.Length - 1);
        Debug.Log(i);
        return meteorFlybySounds[i].clip;
    }
}
