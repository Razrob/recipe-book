using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Recipe
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private string _content;
    [SerializeField] private Sprite _icon;
    [SerializeField] private List<Product> _products;

    public string Name => _name;
    public string Description => _description;
    public string Content => _content;
    public IReadOnlyList<Product> Products => _products;
    public Sprite Icon => _icon;
}
