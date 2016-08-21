using UnityEngine;
using System;
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

    public int best { get; set; }

    public bool hasBest { get; set; }

    private static OpenMode currMode = OpenMode.None;
    private ILeaderboard lb;

    // Use this for initialization
    void Awake()
    {
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

    // DELETE ME
    void Update()
    {
        if (!NoGPGMode)
        {
            if (best < 0)
            {
                if (!lb.loading)
                    GetBestScore(lb);
            }
        }
    }


    public void Initialize()
    {
        //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        //    .EnableSavedGames() // enables saving game progress.
        //    .Build();

        //PlayGamesPlatform.InitializeInstance(config);
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
        //Time.timeScale = 0f;
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
                    //Time.timeScale = 1f;
                }
                else
                {
                    NoGPGMode = true;
                    if (pauseOnLogin)
                        Time.timeScale = 1;
                    Debug.Log("Log in failed!");
                    //Time.timeScale = 1f;
                }
            });
    }

    public void SubmitScore(int score)
    {
        // post score 12345 to leaderboard
        if (!NoGPGMode)
        {
            Social.ReportScore(score, Constants.leaderboard_meteor_dodge_top_players, (bool success) =>
                {
                    // handle success or failure
                });
        }
    }

    //-----------------------------------------------------------------------------------------------------------
    //

    public static void OpenSavedGame(string filename, OpenMode mode)
    {
        if (!NoGPGMode) // ;STATIC // Fixed, kinda..
        {
            currMode = mode;
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
            //Time.timeScale = 0f;
        }
    }

    static void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (currMode == OpenMode.Save)
            {
                byte[] bytes = System.IO.File.ReadAllBytes(Application.persistentDataPath + "/" + SaveLoad.fileName + "." + SaveLoad.fileExtention);
                SaveGame(game, bytes);
            }
            else if (currMode == OpenMode.Load)
            {
                LoadGameData(game);
            }
            currMode = OpenMode.None;
            //Time.timeScale = 1f;
        }
        else
        {
            currMode = OpenMode.None;
            Debug.Log("Cannot open save");
            //Time.timeScale = 1f;
        }
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
                Debug.Log("Loading was successful, the best is " + best);
            }
            else
            {
                if (pauseOnLogin)
                    Time.timeScale = 1;
                Debug.Log("Loading failed");
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

}
