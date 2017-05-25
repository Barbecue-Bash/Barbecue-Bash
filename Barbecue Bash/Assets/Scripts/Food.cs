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
