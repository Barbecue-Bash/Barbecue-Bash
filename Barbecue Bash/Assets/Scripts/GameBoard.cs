using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {

	public FoodsArray foods;
	public readonly Vector2 BottomRight = new Vector2(-5.25f, -3.77f);
	public readonly Vector2 TileSize = new Vector2(1f, 1f);

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
			SpawnPositions[col] = BottomRight + new Vector2(col * TileSize.x, 8 * TileSize.y);
		}
	}

	public void InitializeFoodsAndSpawnPositions() {
		foods = new FoodsArray();
		SpawnPositions = new Vector2[8];
		for (int row = 0; row < 8; row++) {
			for (int col = 0; col < 8; col++) {
				GameObject newTile = getRandomFood();

				InstantiateAndPlaceNewFood(row, col, newTile);
			}
		}
		SetupSpawnPositions();
	}

	void Update () {

	}

}
