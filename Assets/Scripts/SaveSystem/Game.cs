using System;

[System.Serializable]
public class Game
{
    public static Game current;
    public static Game local;
    public static Game cloud;
    public static int characterCount;
    public static int backgroundCount;

    public string name;
    public DateTime timeStamp;

    public SerializableSkin[] characters;
    public SerializableSkin[] backgrounds;

    public int currentCharacter;
    public int currentBackground;

    public int coins;
    public int best;

    public Game()
    {
        characters = new SerializableSkin[characterCount];               // new skin array
        for (int i = 0; i < characters.Length; i++)         // create each skin
        {
            characters[i] = new SerializableSkin();
        }
        characters[0].owned = true;                         // set the first as current, selected and owned
        characters[0].selected = true;
        currentCharacter = 0;

        backgrounds = new SerializableSkin[backgroundCount];              // same as above
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i] = new SerializableSkin();
        }
        backgrounds[0].owned = true;
        backgrounds[0].selected = true;
        currentBackground = 0;

        coins = 0;                                          // set default values
        best = 0;

        timeStamp = DateTime.Now;
        name = "save";                      // and name
    }
}
