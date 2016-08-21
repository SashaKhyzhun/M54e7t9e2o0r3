using UnityEngine;

public class SaveSystemBidge : MonoBehaviour
{
    public string saveFileName;
    public string saveFileExtention;

    public int characterSkinCount;
    public int backgroundSkinCount;

    void Start()
    {
        SaveLoad.fileName = saveFileName;
        SaveLoad.fileExtention = saveFileExtention;

        Game.characterCount = characterSkinCount;          // in case new game needed
        Game.backgroundCount = backgroundSkinCount;

        if (!SaveLoad.Load())           // if can't load - create new game and save it to file
        {
            Game.current = new Game();
            //SaveLoad.Load();
            SaveLoad.Save();
        }
    }

    #region Save_Functions
    public void SaveCharacters(CharacterSkin[] characters, int currentCharacter)
    {
        for (int i = 0; i < characterSkinCount; i++)                        // record all relevant changes in curr game
        {
            Game.current.characters[i].owned = characters[i].owned;
            Game.current.characters[i].selected = characters[i].selected;
        }
        Game.current.currentCharacter = currentCharacter;
        SaveLoad.Save();                                                    // then save to file
    }

    public void SaveBackrounds(BackgroundSkin[] backgrounds, int currentBackground) // same as above
    {
        for (int i = 0; i < backgroundSkinCount; i++)
        {
            Game.current.backgrounds[i].owned = backgrounds[i].owned;
            Game.current.backgrounds[i].selected = backgrounds[i].selected;
        }
        Game.current.currentBackground = currentBackground;
        SaveLoad.Save();
    }

    public void SaveBest(int best)                                                  // so is below
    {
        Game.current.best = best;
        SaveLoad.Save();
    }

    public void SaveCoins(int coins)
    {
        Game.current.coins = coins;
        SaveLoad.Save();
    }

    public void SaveAll(CharacterSkin[] characters, int currentCharacter,           // do it all at once
        BackgroundSkin[] backgrounds, int currentBackground, int best, int coins)
    {
        SaveCharacters(characters, currentCharacter);
        SaveBackrounds(backgrounds, currentBackground);
        SaveBest(best);
        SaveCoins(coins);
        SaveLoad.Save();
    }
    #endregion

    #region Load_Functions
    public void LoadCharacters(ref CharacterSkin[] characters, out int currCharacter)       // if the load was successful - get the characters reference and update it
    {
        if (SaveLoad.Load())
        {
            currCharacter = Game.current.currentCharacter;                                      // and also change current character
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].selected = Game.current.characters[i].selected;
                characters[i].owned = Game.current.characters[i].owned;
            }
        }
        else
        {
            currCharacter = 0;
        }
    }

    public void LoadBackgrounds(ref BackgroundSkin[] backgrounds, out int currBackground)   // same thing
    {
        if (SaveLoad.Load())
        {
            currBackground = Game.current.currentBackground;
            for (int i = 0; i < backgrounds.Length; i++)
            {
                backgrounds[i].selected = Game.current.backgrounds[i].selected;
                backgrounds[i].owned = Game.current.backgrounds[i].owned;
            }
        }
        else
        {
            currBackground = 0;
        }
    }

    public int LoadBest()                                                                   // just return the value if the load was successful
    {
        int best = 0;
        if (SaveLoad.Load())
        {
            best = Game.current.best;
        }
        return best;
    }

    public int LoadCoins()
    {
        int coins = 0;
        if (SaveLoad.Load())
        {
            coins = Game.current.coins;
        }
        return coins;
    }
    #endregion
}
