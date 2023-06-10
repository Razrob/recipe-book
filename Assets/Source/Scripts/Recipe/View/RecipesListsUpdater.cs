using UnityEngine;

public class RecipesListsUpdater : AutoSingletonMono<RecipesListsUpdater>
{
    private RecipesListScreen _recipesListScreen;

    private void Start()
    {
        _recipesListScreen = Screens.GetScreen<RecipesListScreen>();

        _recipesListScreen.UserRecipeArea.OnRecipeCellClick += OnRecipeCellClick;
        _recipesListScreen.GlobalRecipesArea.OnRecipeCellClick += OnRecipeCellClick;

        _recipesListScreen.UserRecipeArea.OnChangeUserRecipesButtonClick += OnUserListChangeRecipeButtonClick;
        _recipesListScreen.GlobalRecipesArea.OnChangeUserRecipesButtonClick += OnUserListChangeRecipeButtonClick;

        _recipesListScreen.RecipeUIWindow.OnCloseButtonClicked += OnCloseWindowButtonClick;

        RefreshUserRecipeList();
        RefreshGlobalRecipeList();
        GlobalModel.Data.RecipesRepo.OnUserRecipePackUpdate += RefreshUserRecipeList;
        GlobalModel.Data.RecipesRepo.OnUserRecipesStateChange += RefreshUserRecipeList;
        GlobalModel.Data.RecipesRepo.OnUserRecipesStateChange += RefreshGlobalRecipeList;
        GlobalModel.Data.RecipesRepo.OnGlobalRecipePackUpdate += RefreshGlobalRecipeList;
    }

    private void RefreshUserRecipeList()
    {
        _recipesListScreen.UserRecipeArea.SetRecipePack(GlobalModel.Data.RecipesRepo.UserRecipePack);
    }

    private void RefreshGlobalRecipeList()
    {
        _recipesListScreen.GlobalRecipesArea.SetRecipePack(GlobalModel.Data.RecipesRepo.GlobalRecipePack);
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

    private void OnUserListChangeRecipeButtonClick(RecipeUICell recipeUICell)
    {
        if (recipeUICell.ContainsInUserRecepts)
            GlobalModel.Data.RecipesRepo.RemoveUserRecipe(recipeUICell.Recipe);
        else
            GlobalModel.Data.RecipesRepo.AddUserRecipe(recipeUICell.Recipe);
    }
}