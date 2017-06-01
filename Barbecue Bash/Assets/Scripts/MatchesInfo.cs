//Barbecue Bash Team
//May 31st, 2017
//MatchesInfo.cs
//Handles matched tiles.
//Designed Based on the Tutorial: "Building a match-3 game (like Candy Crush) in Unity"
//via Dimitris-Ilias Gkanatsios
//URL: https://dgkanatsios.com/2015/02/25/building-a-match-3-game-in-unity-3/
//MatchesInfo.cs is similar to MatchesInfo.cs in the above code.

using System;
using System.Linq;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchesInfo {
  private List<GameObject> matchedFoods;

	public IEnumerable<GameObject> MatchedFood {
		get {
			return matchedFoods.Distinct();
		}
	}

	public void AddObject(GameObject go) {
		if (!matchedFoods.Contains(go)) {
			matchedFoods.Add(go);
		}
	}

	public void AddObjectRange(IEnumerable<GameObject> gos) {
		foreach (var item in gos) {
			AddObject(item);
		}
	}

	public MatchesInfo() {
		matchedFoods = new List<GameObject>();
	}
}
