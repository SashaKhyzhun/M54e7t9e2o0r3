using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{

    [ContextMenuItem("GetCoins", "GetCoins")]
    public StateMachine stateMachine;
    public Spawn coinSpawn, meteorSpawn;
    public Text coinText, meteorText;
    public ShareInfo gameOverShareInfo;

    private int coinScore;
    private int meteorScore;

    void Start()
    {
        coinScore = 0; //load changes instead
        meteorScore = 0;
        SetText(coinText, coinScore);
        SetText(meteorText, meteorScore);
    }

    public void CoinScoreUp()
    {
        if (coinSpawn.spawnEnabled)
        {
            SetText(coinText, ++coinScore);
            //save changes
        }
    }

    public void SpendCoins(int amount)
    {
        coinScore -= amount;
        SetText(coinText, coinScore);
    }

    public int GetCoinScore()
    {
        return coinScore;
    }

    public void GetCoins()
    {
        coinScore += 2;
        SetText(coinText, coinScore);
    }

    public void MeteorScoreUp()
    {
        if (meteorSpawn.spawnEnabled)
        {
            SetText(meteorText, ++meteorScore);
            gameOverShareInfo.text = string.Format(gameOverShareInfo.format, meteorScore);	
        }
    }

    public void Wasted()
    {
        coinSpawn.spawnEnabled = false;
        meteorSpawn.spawnEnabled = false;
        stateMachine.GameToGameOver();
        //if meteor score > best - record it in the history
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
