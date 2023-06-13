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
    [SerializeField] [JsonProperty] private string _authorLogin;
    /*[SerializeField] */
    [JsonIgnore] private Sprite _icon;
    [SerializeField] [JsonProperty] private List<Product> _products;

    [JsonIgnore] public int ID => _id;
    [JsonIgnore] public string Name => _name;
    [JsonIgnore] public string Description => _description;
    [JsonIgnore] public string Content => _content;
    [JsonIgnore] public IReadOnlyList<Product> Products => _products;
    [JsonIgnore] public Sprite Icon => _icon;
    [JsonIgnore] public string AuthorLogin => _authorLogin;

    public Recipe(int id, string name, string description, string content, Sprite icon, List<Product> products, string authorLogin)
    {
        _id = id;
        _name = name;
        _description = description;
        _content = content;
        _icon = icon;
        _products = products;
        _authorLogin = authorLogin;
    }

    public Recipe(RecipeServer recipeServer)
    {
        _id = recipeServer.id;
        _name = recipeServer.name ?? "";
        _description = recipeServer.description ?? "";
        _content = recipeServer.content ?? "";
        Debug.Log(recipeServer.products);
        _products = JsonConvert.DeserializeObject<List<Product>>(recipeServer.products ?? "") ?? new List<Product>();
        _authorLogin = recipeServer.owner ?? "";
    }
}
