using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	public StateMachine stateMachine;
	public Spawn coinSpawn, meteorSpawn;
    public Text coinText, meteorText;

	private int coinScore;
	private int meteorScore;
    
    void Start() {
        coinScore = 0;
		meteorScore = 0;
		SetText(coinText, coinScore);
		SetText (meteorText, meteorScore);
    }
    
	public void CoinScoreUp() {
		if (coinSpawn.spawnEnabled)
			SetText(coinText, ++coinScore);
	}

	public void MeteorScoreUp() {
		if (meteorSpawn.spawnEnabled)
			SetText (meteorText, ++meteorScore);
	}

	public void Wasted() {
		coinSpawn.spawnEnabled = false;
		meteorSpawn.spawnEnabled = false;
		meteorScore = 0;
		stateMachine.GameToGameOver ();
		SetText (meteorText, meteorScore);
	}

	void SetText(Text text, int score) {
		text.text = score + "";
	}
 
}
