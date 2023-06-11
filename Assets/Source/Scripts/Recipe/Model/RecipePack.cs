using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct RecipePack
{
    [SerializeField] [JsonProperty] private Dictionary<int, Recipe> _recipes;

    public Dictionary<int, Recipe> Recipes => _recipes;
    public bool IsEmpty => _recipes is null || _recipes.Count is 0;

    public static RecipePack Empty => new RecipePack();

    public RecipePack(IEnumerable<Recipe> recipes)
    {
        _recipes = new Dictionary<int, Recipe>(recipes.ToDictionary(r => r.ID, r => r));
    }
}
