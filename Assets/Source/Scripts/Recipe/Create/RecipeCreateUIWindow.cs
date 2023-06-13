using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RecipeCreateUIWindow : MonoBehaviour, IRecipeWindow
{
    [SerializeField] private RecipeWindowID _recipeWindowID;
    [SerializeField] private TMP_InputField _nameText;
    [SerializeField] private TMP_InputField _descriptionText;
    [SerializeField] private Button _addProductButton;
    [SerializeField] private Transform _productsParent;
    [SerializeField] private TMP_InputField _contentText;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _nullifyButton;

    private List<CreatedProductUICell> _createdProducts = new List<CreatedProductUICell>();

    public RecipeWindowID RecipeWindowID => _recipeWindowID;
    public bool IsActive { get; private set; }
    public Button SaveButton => _saveButton;
    public Button CancelButton => _cancelButton;
    public Button NullifyButton => _nullifyButton;

    public event Action<IRecipeWindow> OnActiveChange;

    private void Awake()
    {
        gameObject.SetActive(IsActive);
        _addProductButton.onClick.AddListener(OnAddProductButtonClick);
    }

    public void SetActive(bool value)
    {
        if (IsActive == value)
            return;

        IsActive = value;
        gameObject.SetActive(value);

        OnActiveChange?.Invoke(this);
    }

    private void OnAddProductButtonClick()
    {
        CreatedProductUICell productCell = RecipeViewElementsFactory.Instance.CreateCreatedProductUICell();
        productCell.transform.SetParent(_productsParent);
        productCell.OnRemoveButtonClick += OnRemoveProductButtonClick;
        _createdProducts.Add(productCell);
    }

    private void OnRemoveProductButtonClick(CreatedProductUICell productUICell)
    {
        productUICell.OnRemoveButtonClick -= OnRemoveProductButtonClick;
        _createdProducts.Remove(productUICell);
        Destroy(productUICell.gameObject);
    }

    public void SetRecipe(Recipe recipe)
    {
        _nameText.text = recipe.Name;
        _descriptionText.text = recipe.Description;
        _descriptionText.text = recipe.Description;
        _contentText.text = recipe.Content;

        int cellsDifference = recipe.Products.Count - _createdProducts.Count;

        if (cellsDifference > 0)
        {
            for (int i = 0; i < cellsDifference; i++)
                OnAddProductButtonClick();
        }
        else if (cellsDifference < 0)
        {
            for (int i = _createdProducts.Count - 1; i >= _createdProducts.Count - Mathf.Abs(cellsDifference); i--)
                OnRemoveProductButtonClick(_createdProducts[i]);
        }

        for (int i = 0; i < _createdProducts.Count; i++)
        {
            _createdProducts[i].gameObject.SetActive(true);
            _createdProducts[i].SetProduct(recipe.Products[i]);
        }
    }

    public void Nullify()
    {
        _nameText.text = string.Empty;
        _descriptionText.text = string.Empty;
        _descriptionText.text = string.Empty;
        _contentText.text = string.Empty;

        for (int i = _createdProducts.Count - 1; i >= 0; i--)
            OnRemoveProductButtonClick(_createdProducts[i]);
    }

    public bool Validate()
    {
        return true;
    }

    public Recipe FormRecipe(int? id = null)
    {
        List<Product> products = new List<Product>();

        foreach (CreatedProductUICell product in _createdProducts)
            products.Add(product.FormProduct());

        Recipe recipe = new Recipe(
            id: id.HasValue ? id.Value : Random.Range(999999, 9999999),
            name: _nameText.text,
            description: _descriptionText.text,
            content: _contentText.text,
            icon: null,
            products: products,
            authorLogin: GlobalModel.Data.UserAuthorizationData.UserName);

        return recipe;
    }
}
