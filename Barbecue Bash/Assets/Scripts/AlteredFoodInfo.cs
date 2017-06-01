using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class AlteredFoodInfo
{
    private List<GameObject> newFood { get; set; }
    public int MaxDistance { get; set; }

    public IEnumerable<GameObject> AlteredFood
    {
        get
        {
            return newFood.Distinct();
        }
    }

    public void AddFood(GameObject go)
    {
        if (!newFood.Contains(go))
            newFood.Add(go);
    }

    public AlteredFoodInfo()
    {
        newFood = new List<GameObject>();
    }
}
