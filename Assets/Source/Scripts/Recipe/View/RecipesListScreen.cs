using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipesListScreen : UIScreen
{
    [SerializeField] private RecipeListArea _userRecipeArea;
    [SerializeField] private RecipeListArea _globalRecipesArea;
    [SerializeField] private RecipeUIWindow _recipeUIWindow;
    [SerializeField] private RecipeCreateUIWindow _recipeCreateUIWindow;
    [SerializeField] private RecipeCreateUIWindow _recipeModifyUIWindow;
    [SerializeField] private NavigationPanel _navigationPanel;
    [SerializeField] private LoginUIWindow _loginUIWindow;
    [SerializeField] private RegisterUIWindow _registerUIWindow;

    private List<IRecipeWindow> _recipeWindows;

    public RecipeListArea UserRecipeArea => _userRecipeArea;
    public RecipeListArea GlobalRecipesArea => _globalRecipesArea;
    public RecipeUIWindow RecipeUIWindow => _recipeUIWindow;
    public RecipeCreateUIWindow RecipeCreateUIWindow => _recipeCreateUIWindow;
    public RecipeCreateUIWindow RecipeModifyUIWindow => _recipeModifyUIWindow;
    public NavigationPanel NavigationPanel => _navigationPanel;
    public LoginUIWindow LoginUIWindow => _loginUIWindow;
    public RegisterUIWindow RegisterUIWindow => _registerUIWindow;

    private void Awake()
    {
        _recipeWindows = GetComponentsInChildren<IRecipeWindow>(true).ToList();
    }

    public void SetRecipeWindowActive(RecipeWindowID recipeWindowID, bool value) 
    {
        foreach (IRecipeWindow recipeWindow in _recipeWindows)
            recipeWindow.SetActive(recipeWindow.RecipeWindowID == recipeWindowID && value);
    }
}
