using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Recipe
{
    [SerializeField] [JsonProperty] private int _id;
    [SerializeField] [JsonProperty] private string _name;
    [SerializeField] [JsonProperty] private string _description;
    [SerializeField] [JsonProperty] private string _content;
    /*[SerializeField] */
    private Sprite _icon;
    [SerializeField] [JsonProperty] private List<Product> _products;

    public int ID => _id;
    public string Name => _name;
    public string Description => _description;
    public string Content => _content;
    public IReadOnlyList<Product> Products => _products;
    public Sprite Icon => _icon;

    public Recipe(int id, string name, string description, string content, Sprite icon, List<Product> products)
    {
        _id = id;
        _name = name;
        _description = description;
        _content = content;
        _icon = icon;
        _products = products;
    }

    public Recipe(RecipeServer recipeServer)
    {
        _id = recipeServer.id;
        _name = recipeServer.name;
        _description = recipeServer.description;
        _content = recipeServer.content;
    }
}
