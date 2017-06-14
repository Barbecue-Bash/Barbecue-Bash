using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public Text scoreDisplay;

	// Use this for initialization
	void Start () {
		loadScores();
		scoreDisplay.text = getHighestScore().ToString();
	}

	private void loadScores() {
		StreamReader reader;
		try {
			reader = new StreamReader("scores.txt");
		} catch {
			StreamWriter writer = new StreamWriter("scores.txt");
			for (int i = 0; i < 5; i++) {
				writer.WriteLine("0");
			}
			writer.Close();
			reader = new StreamReader("scores.txt");
		}
		for (int i = 0; i < 5; i++) {
			Constants.topScores[i] = int.Parse(reader.ReadLine());
		}
		reader.Close();
	}

	private int getHighestScore() {
		return Constants.topScores[0];
	}

	// Update is called once per frame
	void Update () {

	}

	public void OpenZen() {
		SceneManager.LoadScene("iphone");
		ShapesManager.timed = false;
	}

	public void OpenTimed() {
		SceneManager.LoadScene("iphone");
		ShapesManager.timed = true;
	}

	public void OpenScores() {
		SceneManager.LoadScene("scores");
	}

}
