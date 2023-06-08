using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class RecipesRepo
{
    [SerializeField] private RecipePack _recipePack;

    public RecipePack RecipePack => _recipePack;

    public event Action OnRecipePackUpdate;

    public void SetRecipes(IEnumerable<Recipe> recipes)
    {
        _recipePack = new RecipePack(recipes);
        OnRecipePackUpdate?.Invoke();
    }
}
