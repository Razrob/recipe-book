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

        string description = Recipe.Description.Length > 70 ? Recipe.Description.Substring(0, 70) : Recipe.Description;

        if (Recipe.Description.Length > 70)
            description += "...";

        _descriptionText.text = $"{description}";
    }
}
