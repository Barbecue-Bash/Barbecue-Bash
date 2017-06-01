//Barbecue Bash Team
//May 24th, 2017
//GameBoard.cs
//Handles the playing board and the tiles on it.
//Designed Based on the Tutorial: "Building a match-3 game (like Candy Crush) in Unity"
//via Dimitris-Ilias Gkanatsios
//URL: https://dgkanatsios.com/2015/02/25/building-a-match-3-game-in-unity-3/
//GameBoard.cs is the same as ShapesManager.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour {

	public FoodsArray foods;
	public readonly Vector2 BottomRight = new Vector2(-5.25f, -3.77f);
	public readonly Vector2 TileSize = new Vector2(1f, 1f);

	public bool isTimed;
	public int startTime;

	public Text ScoreText;
	public Text TimerText;
	private int score;
	private int timeLeft;

	private Vector2[] SpawnPositions;
	public GameObject[] FoodTileOptions;

	void Start () {
		InitializeTypesOnPrefabFoods();
		InitializeFoodsAndSpawnPositions();
	}

	private void InitializeTypesOnPrefabFoods() {
		foreach (var item in FoodTileOptions) {
			item.GetComponent<Food>	().Type = item.name;
		}
	}

	private GameObject getRandomFood() {
		return FoodTileOptions[Random.Range(0, FoodTileOptions.Length)];
	}

	private void InstantiateAndPlaceNewFood(int row, int col, GameObject newFood) {
		GameObject go = Instantiate(newFood,
				BottomRight + new Vector2(col * TileSize.x, row * TileSize.y), Quaternion.identity)
				as GameObject;

				go.GetComponent<Food>().Assign(newFood.GetComponent<Food>().Type, row, col);
				foods[row, col] = go;
	}

	private void SetupSpawnPositions() {
		for (int col = 0; col < 8; col++) {
			SpawnPositions[col] = BottomRight + new Vector2(col * TileSize.x, Constants.Rows * TileSize.y);
		}
	}

	public void InitializeFoodsAndSpawnPositions() {
		InitializeVariables();

		foods = new FoodsArray();
		SpawnPositions = new Vector2[8];
		for (int row = 0; row < Constants.Rows; row++) {
			for (int col = 0; col < Constants.Columns; col++) {
				GameObject newTile = getRandomFood();

				InstantiateAndPlaceNewFood(row, col, newTile);
			}
		}
		SetupSpawnPositions();
	}

	private void InitializeVariables() {
		score = 0;
		timeLeft = startTime;
		ShowScore();
		ShowTime();
		InvokeRepeating("DecrementTime", 1.0f, 1.0f);
	}

	private void IncreaseScore(int amount) {
		score += amount;
		ShowScore();
	}

	private void DecrementTime() {
		timeLeft--;
		ShowTime();
	}

	private void ShowScore() {
		ScoreText.text = "Score: " + score.ToString();
	}

	private void ShowTime() {
		TimerText.text = "Time Remaining: " + (timeLeft / 60).ToString() + ":" + (timeLeft % 60).ToString();
	}

	private void DestroyAllFood() {
		for (int row = 0; row < Constants.Rows; row++) {
			for (int col = 0; col < Constants.Columns; col++) {
				Destroy(foods[row, col]);
			}
		}
	}



	void Update () {

	}

}
