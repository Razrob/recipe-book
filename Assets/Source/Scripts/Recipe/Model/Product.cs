using System;
using UnityEngine;

[Serializable]
public class Product
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _mass;

    public Product(string name, Sprite icon, string mass)
    {
        _name = name;
        _icon = icon;
        _mass = mass;
    }

    public string Name => _name;
    public Sprite Icon => _icon;
    public string Mass => _mass;
}
