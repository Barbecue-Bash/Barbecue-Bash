using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities {
	public static bool AreVericalOrHorizontalNeighbors(Food f1, Food f2) {
		return ((f1.Column == f2.Column)
				|| (f1.Row == f2.Row))
				&& Mathf.Abs(f1.Column - f2.Column) <= 1
				&& Mathf.Abs(f1.Row - f2.Row) <= 1;
	}
}
