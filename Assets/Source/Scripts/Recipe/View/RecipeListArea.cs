using System;
using System.Collections.Generic;
using UnityEngine;

public class RecipeListArea : MonoBehaviour
{
    [SerializeField] private Transform _recipeCellsParent;

    private RecipePack _recipePack;
    private List<RecipeUICell> _recipeCellsList;

    public bool IsActive { get; private set; }

    public event Action<RecipeUICell> OnRecipeCellClick;

    private void Awake()
    {
        _recipeCellsList = new List<RecipeUICell>();
    }

    public void SetActive(bool value)
    {
        if (IsActive == value)
            return;

        IsActive = value;

        if (IsActive)
            RefreshCells();
    }

    public void SetRecipePack(RecipePack recipePack)
    {
        _recipePack = recipePack;
        RefreshCells();
    }

    public void RefreshCells()
    {
        if (_recipePack.IsEmpty)
            return;

        int startCellsCount = _recipeCellsList.Count;

        for (int i = 0; i < _recipePack.Recipes.Count - startCellsCount; i++)
        {
            RecipeUICell recipeCell = RecipeViewElementsFactory.Instance.CreateRecipeUICell();
            recipeCell.transform.SetParent(_recipeCellsParent);
            recipeCell.OpenButton.onClick.AddListener(() => OnRecipeCellClick?.Invoke(recipeCell));

            _recipeCellsList.Add(recipeCell);
        }

        for (int i = 0; i < _recipeCellsList.Count; i++)
        {
            _recipeCellsList[i].gameObject.SetActive(i < _recipePack.Recipes.Count);

            if (!_recipeCellsList[i].gameObject.activeSelf)
                continue;

            _recipeCellsList[i].SetRecipe(_recipePack.Recipes[i]);
        }
    }
}


