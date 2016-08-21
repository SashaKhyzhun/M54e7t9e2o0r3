using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{

    [ContextMenuItem("GetCoins", "GetCoins")]
    public StateMachine stateMachine;
    public SaveSystemBidge saveSystemBridge;
    public GPGController gpgController;
    public Spawn coinSpawn, meteorSpawn;
    public Text[] coinTexts;
    public Text meteorText;
    public Text bestText; // ever
    public ShareInfo gameOverShareInfo;

    private int coinScore;
    private int meteorScore;
    private int best;

    void Start()
    {
        //coinScore = 0; //load changes instead
        //if (GPGController.NoGPGMode)
        //{
        LoadStats();
        //}
        meteorScore = 0;
        SetText(meteorText, meteorScore);
    }

    public void LoadStats()
    {
        best = saveSystemBridge.LoadBest();
        SetText(bestText, best);
        coinScore = saveSystemBridge.LoadCoins();
        for (int i = 0; i < coinTexts.Length; i++)
        {
            SetText(coinTexts[i], coinScore);
        }
    }

    public void CoinScoreUp()
    {
        if (coinSpawn.spawnEnabled)
        {
            ++coinScore;
            for (int i = 0; i < coinTexts.Length; i++)
            {
                SetText(coinTexts[i], coinScore);
            }
            //save changes
            saveSystemBridge.SaveCoins(coinScore);
        }
    }

    public void SpendCoins(int amount)
    {
        coinScore -= amount;
        for (int i = 0; i < coinTexts.Length; i++)
        {
            SetText(coinTexts[i], coinScore);
        }
        //save changes
        saveSystemBridge.SaveCoins(coinScore);
    }

    public int GetCoinScore()
    {
        return coinScore;
    }

    public void GetCoins() // for debug purposes
    {
        coinScore += 10;
        for (int i = 0; i < coinTexts.Length; i++)
        {
            SetText(coinTexts[i], coinScore);
        }
        //save changes
        saveSystemBridge.SaveCoins(coinScore);
    }

    public void MeteorScoreUp()
    {
        if (meteorSpawn.spawnEnabled)
        {
            SetText(meteorText, ++meteorScore);
            gameOverShareInfo.text = string.Format(gameOverShareInfo.format, meteorScore);
        }
    }

    public void SetBestScore()
    {
        //if meteor score > best - save best and update ui
        if (best < gpgController.best)
        {
            best = gpgController.best;
            saveSystemBridge.SaveBest(best);
            SetText(bestText, best);
        }

        if (meteorScore > best)
        {
            best = meteorScore;
            saveSystemBridge.SaveBest(best);
            SetText(bestText, best);
        }
    }

    public void Wasted()
    {
        coinSpawn.spawnEnabled = false;
        meteorSpawn.spawnEnabled = false;
        stateMachine.GameToGameOver();
        if (meteorScore < 1)
        {
            gameOverShareInfo.text = gameOverShareInfo.defaultText;
        }
        SetBestScore();
        gpgController.SubmitScore(meteorScore);
    }

    public void ResetScore()
    {
        meteorScore = 0;
        SetText(meteorText, meteorScore);
    }


    void SetText(Text text, int score)
    {
        text.text = score + "";
    }

}
