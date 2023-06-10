using System;
using System.Collections.Generic;
using UnityEngine;

public class RecipeListArea : MonoBehaviour, IRecipeWindow
{
    [SerializeField] private Transform _recipeCellsParent;
    [SerializeField] private RecipeWindowID _recipeWindowID;

    private RecipePack _recipePack;
    private List<RecipeUICell> _recipeCellsList = new List<RecipeUICell>();

    public RecipeWindowID RecipeWindowID => _recipeWindowID;
    public bool IsActive { get; private set; }

    public event Action<RecipeUICell> OnRecipeCellClick;
    public event Action<RecipeUICell> OnChangeUserRecipesButtonClick;
    public event Action<IRecipeWindow> OnActiveChange;

    private void Awake()
    {
        gameObject.SetActive(IsActive);
    }

    public void SetActive(bool value)
    {
        if (IsActive == value)
            return;

        IsActive = value;
        gameObject.SetActive(value);

        if (IsActive)
            RefreshCells();

        OnActiveChange?.Invoke(this);
    }

    public void SetRecipePack(RecipePack recipePack)
    {
        _recipePack = recipePack;
        RefreshCells();
    }

    public void RefreshCells()
    {
        int startCellsCount = _recipeCellsList.Count;

        for (int i = 0; i < _recipePack.Recipes.Count - startCellsCount; i++)
        {
            RecipeUICell recipeCell = RecipeViewElementsFactory.Instance.CreateRecipeUICell();
            recipeCell.transform.SetParent(_recipeCellsParent);

            recipeCell.OpenButton.onClick.AddListener(() => OnRecipeCellClick?.Invoke(recipeCell));
            recipeCell.ChangeUserReceptsButton.onClick.AddListener(() => OnChangeUserRecipesButtonClick?.Invoke(recipeCell));

            _recipeCellsList.Add(recipeCell);
        }

        foreach (RecipeUICell recipeCell in _recipeCellsList)
            recipeCell.gameObject.SetActive(false);

        int index = 0;

        foreach (long id in _recipePack.Recipes.Keys)
        {
            _recipeCellsList[index].gameObject.SetActive(index < _recipePack.Recipes.Count);

            if (!_recipeCellsList[index].gameObject.activeSelf)
                break;

            _recipeCellsList[index].SetRecipe(_recipePack.Recipes[id]);
            _recipeCellsList[index].SetContainsInUserRecipes(GlobalModel.Data.RecipesRepo.ContainsInUserRecipes(_recipePack.Recipes[id]));

            index++;
        }
    }
}


