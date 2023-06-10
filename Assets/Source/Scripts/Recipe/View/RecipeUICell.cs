using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RecipeUICell : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _changeUserReceptsButton;
    [SerializeField] private GameObject _addReceptIcon;

    public Recipe Recipe { get; private set; }
    public Button OpenButton => _openButton;
    public Button ChangeUserReceptsButton => _changeUserReceptsButton;
    public bool ContainsInUserRecepts { get; private set; }

    public void SetRecipe(Recipe recipe)
    {
        Recipe = recipe;
        RefreshView();
    }

    public void SetContainsInUserRecipes(bool contains)
    {
        ContainsInUserRecepts = contains;

        float rotateZ = contains ? 135f : 0f;

        _changeUserReceptsButton.enabled = false;
        _addReceptIcon.transform.DORotateQuaternion(Quaternion.Euler(0f, 0f, rotateZ), 0.4f)
            .OnComplete(() => _changeUserReceptsButton.enabled = true);
    }

    private void RefreshView()
    {
        _nameText.text = $"{Recipe.Name}";
        _iconImage.sprite = Recipe.Icon;

        string description = Recipe.Description.Length > 70 ? Recipe.Description.Substring(0, 70) : Recipe.Description;

        if (Recipe.Description.Length > 70)
            description += "...";

        _descriptionText.text = $"{description}";
    }
}
