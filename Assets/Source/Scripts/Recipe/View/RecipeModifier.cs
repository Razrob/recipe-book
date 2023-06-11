using System.Diagnostics;

public class RecipeModifier : AutoSingletonMono<RecipeModifier>
{
    private RecipesListScreen _recipesListScreen;

    private void Start()
    {
        _recipesListScreen = Screens.GetScreen<RecipesListScreen>();

        _recipesListScreen.RecipeUIWindow.OnModifyButtonClicked += OnModifyButtonClicked;

        _recipesListScreen.RecipeModifyUIWindow.NullifyButton.onClick.AddListener(OnNullify);
        _recipesListScreen.RecipeModifyUIWindow.CancelButton.onClick.AddListener(OnCancel);
        _recipesListScreen.RecipeModifyUIWindow.SaveButton.onClick.AddListener(OnSave);
    }

    private void OnModifyButtonClicked(RecipeUIWindow recipeUIWindow)
    {
        _recipesListScreen.SetRecipeWindowActive(RecipeWindowID.Recipe_Modify_Window, true);
        OnWindowOpen(recipeUIWindow);
    }

    private void OnNullify()
    {
        _recipesListScreen.RecipeModifyUIWindow.Nullify();
    }

    private void OnSave()
    {
        if (!_recipesListScreen.RecipeModifyUIWindow.Validate())
            return;

        Recipe recipe = _recipesListScreen.RecipeModifyUIWindow.FormRecipe(_recipesListScreen.RecipeUIWindow.Recipe.ID);
        GlobalModel.Data.RecipesRepo.ModifyUserRecipe(recipe);

        _recipesListScreen.RecipeUIWindow.SetRecipe(recipe);
        _recipesListScreen.SetRecipeWindowActive(RecipeWindowID.Selected_Recipe_Window, true);
    }

    private void OnCancel()
    {
        _recipesListScreen.SetRecipeWindowActive(RecipeWindowID.Selected_Recipe_Window, true);
    }

    private void OnWindowOpen(RecipeUIWindow recipeUIWindow)
    {
        _recipesListScreen.RecipeModifyUIWindow.SetRecipe(recipeUIWindow.Recipe);
    }
}
