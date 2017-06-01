using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class MatchesInfo
{
    private List<GameObject> matchedFoods;

    /// <summary>
    /// Returns distinct list of matched candy
    /// </summary>
    public IEnumerable<GameObject> MatchedFood
    {
        get
        {
            return matchedFoods.Distinct();
        }
    }

    public void AddObject(GameObject go)
    {
        if (!matchedFoods.Contains(go))
            matchedFoods.Add(go);
    }

    public void AddObjectRange(IEnumerable<GameObject> gos)
    {
        foreach (var item in gos)
        {
            AddObject(item);
        }
    }

    public MatchesInfo()
    {
        matchedFoods = new List<GameObject>();
        BonusesContained = BonusType.None;
    }

    public BonusType BonusesContained { get; set; }
}
