using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoresManager : MonoBehaviour {
	public Text ScoreDisplay;
	// Use this for initialization
	void Start () {
		ScoreDisplay.text = getScores();
	}

	private string getScores () {
		string rtr = "";
		for (int i = 0; i < 5; i++) {
			rtr = rtr + (i+1).ToString() + ". " + Constants.topScores[i].ToString() + "\n";
		}
		return rtr;
	}

	public void LeaveToMenu() {
		SceneManager.LoadScene("main-menu");
	}

}
