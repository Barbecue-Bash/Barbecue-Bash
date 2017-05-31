//Barbecue Bash Team
//May 24th, 2017
//Food.cs
//Handles an individual tile.
//Designed Based on the Tutorial: "Building a match-3 game (like Candy Crush) in Unity"
//via Dimitris-Ilias Gkanatsios
//URL: https://dgkanatsios.com/2015/02/25/building-a-match-3-game-in-unity-3/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

	public int Column {get; set;}
	public int Row {get; set;}
	public string Type {get; set;}

	public void Assign(string type, int row, int col) {
		if (string.IsNullOrEmpty(type)) {
			throw new ArgumentException("type");
		}

		Column = col;
		Row = row;
		Type = type;
	}
}
