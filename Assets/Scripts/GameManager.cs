using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public void Restart() { 
		SceneManager.LoadScene(0);
	}
}

