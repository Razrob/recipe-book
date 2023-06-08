using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductUICell : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _massText;

    private Product _product;

    public void SetProduct(Product product)
    {
        _product = product;
        RefreshView();
    }

    public void RefreshView()
    {
        _nameText.text = $"{_product.Name}";
        _iconImage.sprite = _product.Icon;
        _massText.text = $"{_product.Mass}";
    }
}