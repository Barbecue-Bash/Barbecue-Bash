//Barbecue Bash Team
//May 31st, 2017
//AlteredFoodInfoFood.cs
//Handles tiles that are about to be added to the board.
//Designed Based on the Tutorial: "Building a match-3 game (like Candy Crush) in Unity"
//via Dimitris-Ilias Gkanatsios
//URL: https://dgkanatsios.com/2015/02/25/building-a-match-3-game-in-unity-3/
//AlteredFoodInfo.cs is similar to AlteredCandyInfo.cs in the above code.

using System;
using System.Linq;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlteredFoodInfo {
	private List<GameObject> newFood {get; set;}
	public int MaxDistance {get; set;}

	public IEnumerable<GameObject> AlteredFood {
		get {
			return newFood.Distinct();
		}
	}

	public void AddFood(GameObject go) {
		if (!newFood.Contains(go)) {
			newFood.Add(go);
		}
	}

	public AlteredFoodInfo() {
		newFood = new List<GameObject>();
	}
}
