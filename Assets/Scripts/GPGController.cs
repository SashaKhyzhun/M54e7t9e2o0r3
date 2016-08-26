using UnityEngine;
using System;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.SocialPlatforms;

public enum OpenMode
{
    Save,
    Load,
    None}
;

public class GPGController : MonoBehaviour
{
    public bool pauseOnLogin;
    public static bool NoGPGMode;
    public static float timeOutTime = 25f;

    public int best { get; set; }

    public bool hasBest { get; set; }

    private static Queue<OpenMode> currMode;
    private ILeaderboard lb;

    // Use this for initialization
    void Awake()
    {
        currMode = new Queue<OpenMode>();
        Initialize();
        best = -1;
        if (pauseOnLogin)
            Time.timeScale = 0.01f;
        lb = PlayGamesPlatform.Instance.CreateLeaderboard();
        lb.id = Constants.leaderboard_meteor_dodge_top_players;
        lb.timeScope = TimeScope.AllTime;
    }

    void Start()
    {
        SignIn();
    }

    public void Initialize()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames() // enables saving game progress.
            .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = false;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
    }

    public void SignIn()
    {
        // authenticate user:
        if (pauseOnLogin)
            Time.timeScale = 0.001f;
        Social.localUser.Authenticate((bool success) =>
            {
                // handle success or failure
                if (success)
                {
                    NoGPGMode = false;
                    if (pauseOnLogin)
                        Time.timeScale = 1;
                    GetBestScore(lb);
                    Debug.Log("You've successfuly logged in");
                }
                else
                {
                    NoGPGMode = true;
                    if (pauseOnLogin)
                        Time.timeScale = 1;
                    Debug.Log("Log in failed!");
                }
                Debug.Log("SaveLoad.Load() from Sign In callback function");
                SaveLoad.Load();
            });
    }

    public void SubmitScore(int score)
    {
        // post score 12345 to leaderboard
        if (!NoGPGMode)
        {
            Social.ReportScore(score, Constants.leaderboard_meteor_dodge_top_players, (bool success) =>
                {
                });
        }
    }

    //-----------------------------------------------------------------------------------------------------------
    //

    public static void OpenSavedGame(string filename, OpenMode mode)
    {
        if (!NoGPGMode) // STATIC // Fixed, kinda..
        {
            currMode.Enqueue(mode);
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadNetworkOnly,
                ConflictResolutionStrategy.UseUnmerged, OnSavedGameOpened);
            //Time.timeScale = 0f;
        }
    }

    static void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        Debug.Log("OnSavedGameOpened open mode " + currMode.Peek());
        Debug.Log("Status " + status);
        if (status == SavedGameRequestStatus.Success)
        {
            if (currMode.Peek() == OpenMode.Save)
            {
                Debug.Log("Starting to save");
                byte[] bytes = System.IO.File.ReadAllBytes(Application.persistentDataPath + "/" + SaveLoad.fileName + "." + SaveLoad.fileExtention);

                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                System.IO.FileStream file = System.IO.File.Open(Application.persistentDataPath + "/" + SaveLoad.fileName + "." + SaveLoad.fileExtention, System.IO.FileMode.Open);
                Game savedGame = (Game)bf.Deserialize(file);
                Debug.Log(string.Format(" savedGame.name = {0} \n savedGame.timeStamp = {1} \n savedGame.coins = {2} \n savedGame.best = {3} \n savedGame.currentCharacter = {4}",
                        savedGame.name, savedGame.timeStamp, savedGame.coins, savedGame.best, savedGame.currentCharacter));
                file.Close();

                SaveGame(game, bytes);
            }
            else if (currMode.Peek() == OpenMode.Load)
            {
                Debug.Log("starting to load");
                LoadGameData(game);
            }
            //Time.timeScale = 1f;
        }
        else
        {
            if (currMode.Peek() == OpenMode.Load)
            {
                Debug.Log("Cannot open save to load");
            }
            else if (currMode.Peek() == OpenMode.Save)
            {
                Debug.Log("Cannot open save to save");
            }
            else
            {
                Debug.Log("No OpenMode Provided");
            }
            //Time.timeScale = 1f;
            SaveLoad.loadFinished = true;
        }
        currMode.Dequeue();
    }

    static void SaveGame(ISavedGameMetadata game, byte[] savedData)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
            .WithUpdatedDescription("Saved game at " + DateTime.Now);
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    public static void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Game saved successfuly");
        }
        else
        {
            Debug.Log("Saving failed");
        }
    }

    static void LoadGameData(ISavedGameMetadata game)
    {
        if (!NoGPGMode)
        {
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
        }
    }

    static void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        SaveLoad.LoadCloud(data, status == SavedGameRequestStatus.Success);
    }
    //
    //-----------------------------------------------------------------------------------------------------------

    //DELETE ME
    public void GetBestScore(ILeaderboard lb)
    {
        lb.LoadScores(ok =>
            {
                hasBest = ok;
                if (ok)
                {
                    best = (int)lb.localUserScore.value;
                    if (pauseOnLogin)
                        Time.timeScale = 1;
                    Debug.Log("best is " + best);
                }
                else
                {
                    if (pauseOnLogin)
                        Time.timeScale = 1;
                    Debug.Log("best load failed");
                }
            });
    }


    public void ShowLeaderboardUI()
    {
        // show leaderboard UI
        if (!NoGPGMode)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(Constants.leaderboard_meteor_dodge_top_players);
            Debug.Log("Opening the leaderboard...");
        }
    }

    //-----------------------------------------------------------------------------------------------------

    public void ShowSelectUI()
    {
        uint maxNumToDisplay = 5;
        bool allowCreateNew = false;
        bool allowDelete = true;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ShowSelectSavedGameUI("Select saved game",
            maxNumToDisplay,
            allowCreateNew,
            allowDelete,
            OnSavedGameSelected);
    }

    public void OnSavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
    {
        if (status == SelectUIStatus.SavedGameSelected)
        {
            // handle selected game save
        }
        else
        {
            // handle cancel or error
        }
    }

}
