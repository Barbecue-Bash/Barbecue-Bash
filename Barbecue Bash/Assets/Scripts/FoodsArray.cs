//Barbecue Bash Team
//May 24th, 2017
//FoodsArray.cs
//Handles the 2d array of tiles.
//Designed Based on the Tutorial: "Building a match-3 game (like Candy Crush) in Unity"
//via Dimitris-Ilias Gkanatsios
//URL: https://dgkanatsios.com/2015/02/25/building-a-match-3-game-in-unity-3/
//FoodsArray.cs is similar to ShapesArray.cs in the code above.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FoodsArray {

	private GameObject backupGo1;
	private GameObject backupGo2;

	private GameObject[,] foods = new GameObject[8, 8];

	public GameObject this[int row, int col] {
		get {
			try {
				return foods[row, col];
			} catch (Exception e) {
				throw;
			}
		}

		set {
			foods[row, col] = value;
		}
	}

	public void Swap(GameObject go1, GameObject go2) {
		backupGo1 = go1;
		backupGo2 = go2;

		var go1Food = go1.GetComponent<Food>();
		var go2Food = go2.GetComponent<Food>();

		int go1Row = go1Food.Row;
		int go1Col = go1Food.Column;
		int go2Row = go2Food.Row;
		int go2Col = go2Food.Column;

		var temp = foods[go1Row, go1Col];
		foods[go1Row, go1Col] = foods[go2Row, go2Col];
		foods[go2Row, go2Col] = temp;

		Food.SwapColumnRow(go1Food, go2Food);
	}

	public void Unswap() {
		if (backupGo1 == null || backupGo2 == null) {
			throw new Exception("Backup is now");
		}
		Swap(backupGo1, backupGo2);
	}

	private IEnumerable<GameObject> GetMatchesHorizontally(GameObject go) {
		List<GameObject> matches = new List<GameObject>();
		matches.Add(go);
		var food = go.GetComponent<Food>();

		if (food.Column != 0) {
			for (int col = food.Column - 1; col >= 0; col--) {
				if (foods[food.Row, col].GetComponent<Food>().IsSameType(food)) {
					matches.Add(foods[food.Row, col]);
				} else {
					break;
				}
			}
		}

		if (food.Column != 7) {
			for (int col = food.Column + 1; col < Constants.Columns; col++) {
				if (foods[food.Row, col].GetComponent<Food>().IsSameType(food)) {
					matches.Add(foods[food.Row, col]);
				} else {
					break;
				}
			}
		}

		if (matches.Count < Constants.MinimumMatches) {
			matches.Clear();
		}

		return matches.Distinct();
	}

	private IEnumerable<GameObject> GetMatchesVertically(GameObject go) {
		List<GameObject> matches = new List<GameObject>();
		matches.Add(go);
		var food = go.GetComponent<Food>();

		if (food.Row != 0) {
			for (int row = food.Row - 1; row >= 0; row--) {
				if (foods[row, food.Column].GetComponent<Food>().IsSameType(food)) {
					matches.Add(foods[row, food.Column]);
				} else {
					break;
				}
			}
		}

		if (food.Row != 7) {
			for (int row = food.Row + 1; row < Constants.Rows; row++) {
				if (foods[row, food.Column].GetComponent<Food>().IsSameType(food)) {
					matches.Add(foods[row, food.Column]);
				} else {
					break;
				}
			}
		}

		if (matches.Count < Constants.MinimumMatches) {
			matches.Clear();
		}

		return matches.Distinct();
	}

	public MatchesInfo GetMatches(GameObject go) {
		MatchesInfo matchesInfo = new MatchesInfo();

		var horizontalMatches = GetMatchesHorizontally(go);
		matchesInfo.AddObjectRange(horizontalMatches);

		var verticalMatches = GetMatchesVertically(go);
		matchesInfo.AddObjectRange(verticalMatches);

		return matchesInfo;
	}

	public IEnumerable<GameObject> GetMatches(IEnumerable<GameObject> gos) {
		List<GameObject> matches = new List<GameObject>();
		foreach (var go in gos) {
			matches.AddRange(GetMatches(go).MatchedFood);
		}
		return matches.Distinct();
	}

	public void Remove(GameObject item) {
		foods[item.GetComponent<Food>().Row, item.GetComponent<Food>().Column] = null;
	}

	public AlteredFoodInfo Collapse(IEnumerable<int> cols) {
		AlteredFoodInfo collapseInfo = new AlteredFoodInfo();
		foreach (var col in cols) {
			for (int row = 0; row < Constants.Rows - 1; row++) {
				if (foods[row, col] == null) {
					for (int row2 = row + 1; row2 < Constants.Rows; row2++) {
						collapseInfo.MaxDistance = row2 - row;

						foods[row, col].GetComponent<Food>().Row = row;
						foods[row, col].GetComponent<Food>().Column = col;

						collapseInfo.AddFood(foods[row, col]);
						break;
					}
				}
			}
		}
		return collapseInfo;
	}

	public IEnumerable<FoodInfo> GetEmptyItemsOnColumn(int col) {
		List<FoodInfo> emptyItems = new List<FoodInfo>();
		for (int row = 0; row < Constants.Rows; row++) {
			if (foods[row, col] == null) {
				emptyItems.Add(new FoodInfo() {Row = row, Column = col});
			}
		}
		return emptyItems;
	}
}
