//Barbecue Bash Team
//May 31st, 2017
//FoodInfo.cs
//Used by FoodArray.cs to help handle empty tiles.
//Designed Based on the Tutorial: "Building a match-3 game (like Candy Crush) in Unity"
//via Dimitris-Ilias Gkanatsios
//URL: https://dgkanatsios.com/2015/02/25/building-a-match-3-game-in-unity-3/
//FoodInfo.cs is similar to ShapeInfo.cs in the above code.

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInfo {
	public int Column {get; set;}
	public int Row    {get; set;}
}
