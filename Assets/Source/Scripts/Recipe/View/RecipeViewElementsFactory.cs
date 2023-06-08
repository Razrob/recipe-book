using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeViewElementsFactory
{
    private RecipeViewElementsConfig _config;

    private static RecipeViewElementsFactory _instance;
    public static RecipeViewElementsFactory Instance
    {
        get
        {
            if (_instance is null)
                _instance = new RecipeViewElementsFactory();

            return _instance;
        }
    }

    private RecipeViewElementsFactory()
    {
        _config = ConfigsRepository.FindConfig<RecipeViewElementsConfig>();
        _instance = this;
    }

    public RecipeUICell CreateRecipeUICell()
    {
        RecipeUICell cell = GameObject.Instantiate(_config.RecipeUICellPrefab);
        return cell;
    }

    public RecipeUIWindow CreateRecipeUIWindow()
    {
        RecipeUIWindow window = GameObject.Instantiate(_config.RecipeUIWindowPrefab);
        return window;
    }

    public ProductUICell CreateProductUICell()
    {
        ProductUICell cell = GameObject.Instantiate(_config.ProductUICellPrefab);
        return cell;
    }
}
