using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUIWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _contentText;
    [SerializeField] private Transform _productCellsParent;
    [SerializeField] private Button _closeButton;

    private List<ProductUICell> _productCells;

    public Recipe Recipe { get; private set; }
    public bool IsActive { get; private set; }

    public event Action<RecipeUIWindow> OnCloseButtonClicked;

    private void Awake()
    {
        _productCells = new List<ProductUICell>();
        _closeButton.onClick.AddListener(() => OnCloseButtonClicked?.Invoke(this));
    }

    public void SetActive(bool value)
    {
        if (IsActive == value)
            return;

        IsActive = value;

        if (IsActive)
            RefreshView();
    }

    public void SetRecipe(Recipe recipe)
    {
        Recipe = recipe;
        RefreshView();
    }

    public void RefreshView()
    {
        if (Recipe is null)
            return;

        _nameText.text = $"{Recipe.Name}";
        _iconImage.sprite = Recipe.Icon;
        _descriptionText.text = $"{Recipe.Description}";
        RefreshProductList();
    }

    private void RefreshProductList()
    {
        int startCellsCount = _productCells.Count;

        for (int i = 0; i < Recipe.Products.Count - startCellsCount; i++)
        {
            ProductUICell productCell = RecipeViewElementsFactory.Instance.CreateProductUICell();
            productCell.transform.SetParent(_productCellsParent);
            _productCells.Add(productCell);
        }

        for (int i = 0; i < _productCells.Count; i++)
        {
            _productCells[i].gameObject.SetActive(i < Recipe.Products.Count);

            if (!_productCells[i].gameObject.activeSelf)
                continue;

            _productCells[i].SetProduct(Recipe.Products[i]);
        }
    }
}
