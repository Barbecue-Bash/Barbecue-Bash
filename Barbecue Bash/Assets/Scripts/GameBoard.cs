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
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour {

	public FoodsArray foods;
	public readonly Vector2 BottomRight = new Vector2(-5.25f, -3.77f);
	public readonly Vector2 TileSize = new Vector2(1f, 1f);

	public bool isTimed;
	public int startTime;

	private GameState state = GameState.None;
	private GameObject hitGo = null;

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

	private GameObject GetRandomFood() {
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
				GameObject newTile = GetRandomFood();

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

	private void MoveAndAnimate(IEnumerable<GameObject> movedGameObjects, int distance) {
		foreach (var item in movedGameObjects) {
			item.transform.positionTo(Constants.MoveAnimationMinDuration * distance, BottomRight + new Vector2(item.GetComponent<Food>().Column * TileSize.x, item.GetComponent<Food>().Row * TileSize.y));
		}
	}

	private AlteredFoodInfo CreateNewCandyInSpecificLocations(IEnumerable<int> colsWithMissingFood) {
		AlteredFoodInfo newFoodInfo = new AlteredFoodInfo();
		foreach (int col in colsWithMissingFood) {
			var emptyItems = foods.GetEmptyItemsOnColumn(col);
			foreach (var item in emptyItems) {
				var go = GetRandomFood();
				GameObject newFood = Instantiate(go, SpawnPositions[col], Quaternion.identity) as GameObject;
				newFood.GetComponent<Food>().Assign(go.GetComponent<Food>().Type, item.Row, item.Column);
				if (Constants.Rows - item.Row > newFoodInfo.MaxDistance) {
					newFoodInfo.MaxDistance = Constants.Rows - item.Row;
				}
				foods[item.Row, item.Column] = newFood;
				newFoodInfo.AddFood(newFood);
			}
		}
		return newFoodInfo;
	}

	void Update () {
		if (state == GameState.None) {
			if (Input.GetMouseButtonDown(0)) {
				var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				if (hit.collider != null) {
					hitGo = hit.collider.gameObject;
					state = GameState.SelectionStarted;
				}
			}
		} else if (state == GameState.SelectionStarted) {
			if (Input.GetMouseButton(0)) {
				var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				if (hit.collider != null && hitGo != hit.collider.gameObject) {
					if (!Utilities.AreVericalOrHorizontalNeighbors(hitGo.GetComponent<Food>(), hit.collider.gameObject.GetComponent<Food>())) {
						state = GameState.None;
					} else {
						state = GameState.Animating;
						FindMatchesAndCollapse(hit);
					}
				}
			}
		}
	}

	private IEnumerator FindMatchesAndCollapse(RaycastHit2D hit2) {
		var hitGo2 = hit2.collider.gameObject;
		foods.Swap(hitGo, hitGo2);
		hitGo.transform.positionTo(Constants.AnimationDuration, hitGo2.transform.position);
		hitGo2.transform.positionTo(Constants.AnimationDuration, hitGo.transform.position);
		yield return new WaitForSeconds(Constants.AnimationDuration);
		var hitGoMatchesInfo = foods.GetMatches(hitGo);
		var hitGo2MatchesInfo = foods.GetMatches(hitGo2);
		var totalMatches = hitGoMatchesInfo.MatchedFood.Union(hitGo2MatchesInfo.MatchedFood).Distinct();
		if (totalMatches.Count() < Constants.MinimumMatches) {
			hitGo.transform.positionTo(Constants.AnimationDuration, hitGo.transform.position);
			hitGo2.transform.positionTo(Constants.AnimationDuration, hitGo2.transform.position);
			yield return new WaitForSeconds(Constants.AnimationDuration);
			foods.Unswap();
		}

		//TODO: Bonus for swaps of 4+

		state = GameState.None;
	}


}
