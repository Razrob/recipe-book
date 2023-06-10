using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class RecipesRepo
{
    [SerializeField] private RecipePack _userRecipePack = new RecipePack(new Recipe[0]);
    [SerializeField] private RecipePack _globalRecipePack = new RecipePack(new Recipe[0]);

    public RecipePack UserRecipePack => _userRecipePack;
    public RecipePack GlobalRecipePack => _globalRecipePack;

    public event Action OnUserRecipePackUpdate;
    public event Action OnGlobalRecipePackUpdate;

    public event Action<Recipe> OnUserReceptAdd;
    public event Action<Recipe> OnUserReceptRemove;
    public event Action OnUserRecipesStateChange;

    public void SetUserRecipes(IEnumerable<Recipe> recipes)
    {
        _userRecipePack = new RecipePack(recipes);
        OnUserRecipePackUpdate?.Invoke();
    }

    public void SetGlobalRecipes(IEnumerable<Recipe> recipes)
    {
        _globalRecipePack = new RecipePack(recipes);
        OnGlobalRecipePackUpdate?.Invoke();
    }

    public void AddUserRecipe(Recipe recipe)
    {
        if (!_userRecipePack.Recipes.ContainsKey(recipe.ID))
        {
            _userRecipePack.Recipes.Add(recipe.ID, recipe);
            OnUserReceptAdd?.Invoke(recipe);
            OnUserRecipesStateChange?.Invoke();
        }
    }

    public void RemoveUserRecipe(Recipe recipe)
    {
        if (_userRecipePack.Recipes.ContainsKey(recipe.ID))
        {
            _userRecipePack.Recipes.Remove(recipe.ID);
            OnUserReceptRemove?.Invoke(recipe);
            OnUserRecipesStateChange?.Invoke();
        }
    }

    public bool ContainsInUserRecipes(Recipe recipe)
    {
        return _userRecipePack.Recipes.ContainsKey(recipe.ID);
    }
}
