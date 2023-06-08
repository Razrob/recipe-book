using System;
using UnityEngine;

[Serializable]
public class Product
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _mass;

    public string Name => _name;
    public Sprite Icon => _icon;
    public int Mass => _mass;
}
