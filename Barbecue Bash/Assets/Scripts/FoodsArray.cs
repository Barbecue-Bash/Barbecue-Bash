//Barbecue Bash Team
//May 24th, 2017
//FoodsArray.cs
//Handles the 2d array of tiles.
//Designed Based on the Tutorial: "Building a match-3 game (like Candy Crush) in Unity"
//via Dimitris-Ilias Gkanatsios
//URL: https://dgkanatsios.com/2015/02/25/building-a-match-3-game-in-unity-3/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FoodsArray {

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
}
