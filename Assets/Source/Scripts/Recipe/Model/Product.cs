using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class Product
{
    [SerializeField][JsonProperty] private string _name;
    /* [SerializeField] */
    [JsonIgnore] private Sprite _icon;
    [SerializeField][JsonProperty] private string _mass;

    public Product(string name, Sprite icon, string mass)
    {
        _name = name;
        _icon = icon;
        _mass = mass;
    }

    [JsonIgnore] public string Name => _name;
    [JsonIgnore] public Sprite Icon => _icon;
    [JsonIgnore] public string Mass => _mass;
}
