using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatedProductUICell : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameText;
    [SerializeField] private TMP_InputField _massText;
    [SerializeField] private Button _removeButton;

    public event Action<CreatedProductUICell> OnRemoveButtonClick;

    private void Awake()
    {
        _removeButton.onClick.AddListener(() => OnRemoveButtonClick?.Invoke(this));
    }

    public void SetProduct(Product product)
    {
        _nameText.text = $"{product.Name}";
        _massText.text = $"{product.Mass}";
    }

    public Product FormProduct()
    {
        return new Product(_nameText.text, null, _massText.text);
    }
}
