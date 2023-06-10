public class RecipesPanelsSwitcher : AutoSingletonMono<RecipesPanelsSwitcher>
{
    private RecipesListScreen _recipesListScreen;

    private void Start()
    {
        _recipesListScreen = Screens.GetScreen<RecipesListScreen>();

        foreach (NavigationPanel.NavigationButton button in _recipesListScreen.NavigationPanel.NavigationButtons)
            button.Button.onClick.AddListener(() => OnNavigationButtonClick(button));

        _recipesListScreen.SetRecipeWindowActive(RecipeWindowID.User_Recipe_Area, true);
    }

    private void OnNavigationButtonClick(NavigationPanel.NavigationButton navigationButton)
    {
        _recipesListScreen.SetRecipeWindowActive(navigationButton.WindowID, true);
    }
}
