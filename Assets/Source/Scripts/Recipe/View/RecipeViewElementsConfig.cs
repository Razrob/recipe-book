﻿using UnityEngine;

[CreateAssetMenu(fileName = "RecipeViewElementsConfig", menuName = "Config/RecipeViewElementsConfig")]
public class RecipeViewElementsConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private RecipeUICell _recipeUICellPrefab;
    [SerializeField] private RecipeUIWindow _recipeUIWindowPrefab;
    [SerializeField] private ProductUICell _productUICellPrefab;

    public RecipeUICell RecipeUICellPrefab => _recipeUICellPrefab;
    public RecipeUIWindow RecipeUIWindowPrefab => _recipeUIWindowPrefab;
    public ProductUICell ProductUICellPrefab => _productUICellPrefab;

    private void OnEnable()
    {

    }
}
