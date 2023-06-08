using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeWebLoader : AutoSingletonMono<RecipeWebLoader>
{
    private Coroutine _loadCoroutine;

    private void Awake()
    {
        Upload();
    }

    public void Upload()
    {
        if (_loadCoroutine != null)
            return;

        _loadCoroutine = StartCoroutine(LoadRecipes());
    }

    private IEnumerator LoadRecipes()
    {
        while (!GlobalModel.DataLoaded)
            yield return null;

        ///loading
        GlobalModel.Data.RecipesRepo.SetRecipes(new List<Recipe>());
    }
}
