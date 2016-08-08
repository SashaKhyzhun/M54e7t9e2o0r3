using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    public PlayerController playerController;
    public ScoreController scoreController;
    public SpriteRenderer background;

    //    public RuntimeAnimatorController[] characterAnimators;
    //    public float[] characterSpeeds;
    //    public int[] characterSkinsCost;
    //    private bool[] characterSkinAvailable;
    public CharacterSkin[] characters;
    private int currentCharacterSkin = 0;

    //    public Sprite[] backgrounds;
    //    public int[] backgroundsCost;
    //    private bool[] backgroundsAvailable;
    public BackgroundSkin[] backgrounds;
    private int currentBackground = 0;

    void Start()
    {
        //characterSkinAvailable = new bool[characterSkinsCost.Length];
        //backgroundsAvailable = new bool[backgroundsCost.Length];

        //load changes
        //get current index and ChangePlayer and ChangeBackground by that index
        ChangePlayer(currentCharacterSkin);
        ChangeBackground(currentBackground);
    }

    //    public void RefreshSettings()
    //    {
    //        for (int i = 0; i < characterSkinsCost.Length; i++)
    //        {
    //            characterSkinAvailable[i] = scoreController.GetCoinScore() >= characterSkinsCost[i];
    //        }
    //        //should be called when game state changes to settings
    //        //change appearence of buttons
    //    }

    //    public void ChangePlayer(int index)
    //    {
    //        if (characterSkinAvailable[index])
    //        {
    //            playerController.gameObject.GetComponent<Animator>().runtimeAnimatorController = characterAnimators[index];
    //            playerController.speed = characterSpeeds[index];
    //            currentCharacterSkin = index;
    //            //save changes
    //        }
    //    }

    //    public void ChangeBackground(int index)
    //    {
    //        if (backgroundsAvailable[index])
    //        {
    //            background.sprite = backgrounds[index];
    //            currentBackground = index;
    //            // save changes
    //        }
    //    }

    public void RefreshSettings()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].available = characters[i].owned || (scoreController.GetCoinScore() >= characters[i].cost);
            characters[i].selected = false;
            characters[currentCharacterSkin].selected = true;
            characters[i].UpdateGfx();
        }
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].available = backgrounds[i].owned || (scoreController.GetCoinScore() >= backgrounds[i].cost);
            backgrounds[i].selected = false;
            backgrounds[currentBackground].selected = true;
            backgrounds[i].UpdateGfx();
        } 
        //should be called when game state changes to settings
        //change appearence of buttons
    }

    public void BuyCharacterSkin(int index)
    {
        if (characters[index].available && !characters[index].selected)
        {
            if (!characters[index].owned)
            {
                scoreController.SpendCoins(characters[index].cost);
            }
            ChangePlayer(index);
        }
        else
        {
            Debug.Log("Can't afford " + characters[index].cost + " coins");
        }
    }

    public void BuyBackgroundSkin(int index)
    {
        if (backgrounds[index].available && !backgrounds[index].selected)
        {
            if (!backgrounds[index].owned)
            {
                scoreController.SpendCoins(backgrounds[index].cost);
            }
            ChangeBackground(index);
        }
        else
        {
            Debug.Log("Can't afford " + backgrounds[index].cost + " coins");
        }
    }

    public void ChangePlayer(int index)
    {
        playerController.gameObject.GetComponent<Animator>().runtimeAnimatorController = characters[index].anim;
        playerController.speed = characters[index].speed;
        currentCharacterSkin = index;
        characters[index].owned = true;
        RefreshSettings();
        //save changes
    }

    public void ChangeBackground(int index)
    {
        background.sprite = backgrounds[index].backgroundSprite;
        currentBackground = index;
        backgrounds[index].owned = true;
        RefreshSettings();
        // save changes
    }
}