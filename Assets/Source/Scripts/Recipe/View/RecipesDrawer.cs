using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipesDrawer : AutoSingletonMono<RecipesDrawer>
{
    private RecipesListScreen _recipesListScreen;

    private void Start()
    {
        _recipesListScreen = Screens.GetScreen<RecipesListScreen>();
        _recipesListScreen.RecipeArea.OnRecipeCellClick += OnRecipeCellClick;
        _recipesListScreen.RecipeUIWindow.OnCloseButtonClicked += OnCloseWindowButtonClick;

        RefreshScreen();
        GlobalModel.Data.RecipesRepo.OnRecipePackUpdate += RefreshScreen;
    }

    private void RefreshScreen()
    {
        _recipesListScreen.RecipeArea.SetActive(true);
        _recipesListScreen.RecipeArea.SetRecipePack(GlobalModel.Data.RecipesRepo.RecipePack);
    }

    private void OnRecipeCellClick(RecipeUICell cell)
    {
        _recipesListScreen.RecipeUIWindow.SetActive(true);
        _recipesListScreen.RecipeUIWindow.SetRecipe(cell.Recipe);
    }

    private void OnCloseWindowButtonClick(RecipeUIWindow window)
    {
        window.SetActive(false);
    }
}
