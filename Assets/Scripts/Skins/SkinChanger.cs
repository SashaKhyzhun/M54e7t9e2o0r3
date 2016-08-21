using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    public PlayerController playerController;
    public SaveSystemBidge saveSystemBridge;
    public ScoreController scoreController;
    public SpriteRenderer background;
    
    public CharacterSkin[] characters;
    private int currentCharacterSkin = 0;
    
    public BackgroundSkin[] backgrounds;
    private int currentBackground = 0;

    void Start()
    {
        //if (GPGController.NoGPGMode)
        //{
            LoadStats();
        //}
        //load changes
        //get current indices and ChangePlayer and ChangeBackground by that index
    }

    public void LoadStats()
    {
        saveSystemBridge.LoadCharacters(ref characters, out currentCharacterSkin);
        saveSystemBridge.LoadBackgrounds(ref backgrounds, out currentBackground);
        ChangePlayer(currentCharacterSkin);
        ChangeBackground(currentBackground);
    }

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
        //should be called when gamestate changes to settings
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
        saveSystemBridge.SaveCharacters(characters, currentCharacterSkin);
    }

    public void ChangeBackground(int index)
    {
        background.sprite = backgrounds[index].backgroundSprite;
        currentBackground = index;
        backgrounds[index].owned = true;
        RefreshSettings();
        // save changes
        saveSystemBridge.SaveBackrounds(backgrounds, currentBackground);
    }
}