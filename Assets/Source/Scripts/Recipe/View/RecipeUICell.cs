using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUICell : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Button _openButton;

    public Recipe Recipe { get; private set; }
    public Button OpenButton => _openButton;

    public void SetRecipe(Recipe recipe)
    {
        Recipe = recipe;
        RefreshView();
    }

    public void RefreshView()
    {
        _nameText.text = $"{Recipe.Name}";
        _iconImage.sprite = Recipe.Icon;
        _descriptionText.text = $"{Recipe.Description}";
    }
}
