using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct RecipePack
{
    [SerializeField] private List<Recipe> _recipes;

    public IReadOnlyList<Recipe> Recipes => _recipes;
    public bool IsEmpty => _recipes is null || _recipes.Count is 0;

    public static RecipePack Empty => new RecipePack();

    public RecipePack(IEnumerable<Recipe> recipes)
    {
        _recipes = recipes.ToList();
    }
}
