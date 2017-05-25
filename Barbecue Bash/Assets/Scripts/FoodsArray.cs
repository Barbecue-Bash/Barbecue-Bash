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
