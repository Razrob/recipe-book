using UnityEngine;

public class RecipesListScreen : UIScreen
{
    [SerializeField] private RecipeListArea _recipeArea;
    [SerializeField] private RecipeUIWindow _recipeUIWindow;

    public RecipeListArea RecipeArea => _recipeArea;
    public RecipeUIWindow RecipeUIWindow => _recipeUIWindow;
}
