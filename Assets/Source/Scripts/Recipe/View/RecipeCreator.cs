using System;

public class RecipeCreator : AutoSingletonMono<RecipeCreator>
{
    private RecipesListScreen _recipesListScreen;

    private void Start()
    {
        _recipesListScreen = Screens.GetScreen<RecipesListScreen>();
        _recipesListScreen.RecipeCreateUIWindow.OnActiveChange += OnRecipeCreateWindowActiveChange;

        _recipesListScreen.RecipeCreateUIWindow.NullifyButton.onClick.AddListener(OnNullify);
        _recipesListScreen.RecipeCreateUIWindow.CancelButton.onClick.AddListener(OnCancel);
        _recipesListScreen.RecipeCreateUIWindow.SaveButton.onClick.AddListener(OnSave);
    }

    private void OnNullify()
    {
        _recipesListScreen.RecipeCreateUIWindow.Nullify();
    }

    private void OnSave()
    {
        if (!_recipesListScreen.RecipeCreateUIWindow.Validate())
            return;

        Recipe recipe = _recipesListScreen.RecipeCreateUIWindow.FormRecipe();
        GlobalModel.Data.RecipesRepo.AddUserRecipe(recipe);

        _recipesListScreen.SetRecipeWindowActive(RecipeWindowID.User_Recipe_Area, true);
    }

    private void OnCancel()
    {
        _recipesListScreen.SetRecipeWindowActive(RecipeWindowID.User_Recipe_Area, true);
    }

    private void OnRecipeCreateWindowActiveChange(IRecipeWindow recipeWindow)
    {
        if (recipeWindow.IsActive)
            OnWindowOpen();
    }

    private void OnWindowOpen()
    {
        _recipesListScreen.RecipeCreateUIWindow.Nullify();
    }
}
